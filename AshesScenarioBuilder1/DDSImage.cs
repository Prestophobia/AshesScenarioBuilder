using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

using DirectXTexNet;


//using Stardock.Nitrous;


/* Code taken from here: https://gist.github.com/soeminnminn/e9c4c99867743a717f5b */

/* Other resources: 
 * https://github.com/DentonW/DevIL/blob/master/DevIL/src-IL/include/il_dds.h
 * https://github.com/Microsoft/DirectXTex/tree/master/DirectXTex
 * https://github.com/midspace/SEToolbox/blob/master/Main/SEToolbox/SEToolbox.Image.Library/ImageTextureUtil.cs
 */

namespace Stardock.NitrousTool.Utilities
{
    #region DdsImage Class
    public class DdsImage : IDisposable
    {
        #region Variables
        private string m_fileName = null;
        private bool m_isValid = false;
        private Bitmap m_bitmap = null;
        private byte[] m_rawData = null;
        private bool m_isDXT10 = false;
        private bool m_precisionWarned = false;
        #endregion

        #region Constructor/Destructor
        public DdsImage(byte[] ddsImage)
        {
            if (ddsImage == null) throw new ArgumentNullException(nameof(ddsImage));
            if (ddsImage.Length == 0) return;

            using (MemoryStream stream = new MemoryStream(ddsImage.Length))
            {
                stream.Write(ddsImage, 0, ddsImage.Length);
                stream.Seek(0, SeekOrigin.Begin);

                using (BinaryReader reader = new BinaryReader(stream))
                {
                    this.Parse(reader);
                }
            }
        }

        public DdsImage(Stream ddsImage)
        {
            if (ddsImage == null) throw new ArgumentNullException(nameof(ddsImage));
            if (!ddsImage.CanRead) throw new InvalidOperationException("The DDS image System.Stream is not readable.");

            using (BinaryReader reader = new BinaryReader(ddsImage))
            {
                this.Parse(reader);
            }
        }


        public DdsImage(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException("The DDS image file could not be found at the provided path.", fileName);

            m_fileName = fileName;

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    this.Parse(reader);
                }
            }
        }


        private DdsImage(System.Drawing.Bitmap bitmap)
        {
            this.m_bitmap = bitmap;
        }
        #endregion

        #region Override Methods
        #endregion

        #region Private Methods
        private void Parse(BinaryReader reader)
        {
            DDSStruct header = new DDSStruct();
            PixelFormat pixelFormat = PixelFormat.UNKNOWN;
            DxgiFormat dxgiFormat = DxgiFormat.DXGI_FORMAT_UNKNOWN;

            byte[] data = null;

            if (this.ReadHeader(reader, ref header))
            {
                this.m_isValid = true;
                // patches for stuff
                if (header.depth == 0) header.depth = 1;
                uint blocksize = 0;


                // Determine the PixelFormat.
                pixelFormat = this.GetFormat(header, ref blocksize);

                /*
                if (pixelFormat == PixelFormat.UNKNOWN)
                {
                    throw new DdsImageException($"Unsupported pixel format (FOURCC = {header.pixelformat.fourcc}).");
                }*/


                // Determine the DXGI format.
                if (m_isDXT10)
                {
                    dxgiFormat = (DxgiFormat)header.dxt10_dxgiFormat;
                }
                else
                {
                    dxgiFormat = DetectDxgiFormat(pixelFormat, header);
                }

                /*
                if (dxgiFormat == DxgiFormat.DXGI_FORMAT_UNKNOWN)
                {
                    throw new DdsImageException($"Unsupported DXGI format (FOURCC = {header.pixelformat.fourcc}).");
                }*/


                // Read in the image data.
                switch (dxgiFormat)
                {
                    case DxgiFormat.DXGI_FORMAT_BC6H_UF16:
                    case DxgiFormat.DXGI_FORMAT_BC4_UNORM:
                    case DxgiFormat.DXGI_FORMAT_BC7_UNORM:
                    case DxgiFormat.DXGI_FORMAT_UNKNOWN:

                        if (!String.IsNullOrWhiteSpace(m_fileName))
                        {
                            using (IScratchImage ddsImage = DirectXTex.LoadFromDDSFile(m_fileName))
                            {
                                data = ddsImage.GetRawBytes(0, 0);
                                dxgiFormat = (DxgiFormat)((int)ddsImage.MetaData.format);
                            }
                        }

                        break;

                    default:

                        data = this.ReadData(reader, header);
                        break;
                }


                if (data != null)
                {
                    m_rawData = this.DecompressData(header, data, pixelFormat, dxgiFormat);

                    switch (dxgiFormat)
                    {
                        case DxgiFormat.DXGI_FORMAT_R8_UNORM:

                            m_bitmap = this.CreateGreyscaleBitmap(header, m_rawData);
                            break;

                        default:

                            m_bitmap = this.Create32BppBitmap(header, m_rawData, dxgiFormat);
                            break;
                    }
                }
                else
                {
                    // TODO: Throw an exception.
                }
            }
        }


        private DxgiFormat DetectDxgiFormat(PixelFormat pixelFormat, DDSStruct header)
        {
            switch (pixelFormat)
            {
                case PixelFormat.LUMINANCE:

                    if (header.pixelformat.rgbbitcount == 8 && header.pixelformat.rbitmask == 255 && header.pixelformat.gbitmask == 0 && header.pixelformat.bbitmask == 0 && header.pixelformat.alphabitmask == 0)
                    {
                        return DxgiFormat.DXGI_FORMAT_R8_UNORM;
                    }

                    break;

                case PixelFormat.BC4S:

                    return DxgiFormat.DXGI_FORMAT_BC4_SNORM;

                case PixelFormat.BC4U:

                    return DxgiFormat.DXGI_FORMAT_BC4_UNORM;
            }


            return DxgiFormat.DXGI_FORMAT_UNKNOWN;
        }


        private byte[] ReadData(BinaryReader reader, DDSStruct header)
        {
            byte[] compdata = null;
            uint compsize = 0;

            if ((header.flags & DDSD_LINEARSIZE) > 1)
            {
                compdata = reader.ReadBytes((int)header.sizeorpitch);
                compsize = (uint)compdata.Length;
            }
            else
            {
                uint bps;

                if (m_isDXT10)
                {
                    bps = header.sizeorpitch;
                }
                else
                {
                    bps = header.width * header.pixelformat.rgbbitcount / 8;
                }
                
                compsize = bps * header.height * header.depth;
                compdata = new byte[compsize];

                MemoryStream mem = new MemoryStream((int)compsize);

                byte[] temp;
                for (int z = 0; z < header.depth; z++)
                {
                    for (int y = 0; y < header.height; y++)
                    {
                        temp = reader.ReadBytes((int)bps);
                        mem.Write(temp, 0, temp.Length);
                    }
                }
                mem.Seek(0, SeekOrigin.Begin);

                mem.Read(compdata, 0, compdata.Length);
                mem.Close();
            }

            return compdata;
        }


        private Bitmap CreateGreyscaleBitmap(DDSStruct header, byte[] rawData)
        {
            System.Drawing.Imaging.PixelFormat pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;

            Bitmap bitmap = new Bitmap((int)header.width, (int)header.height, pixelFormat);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, pixelFormat);
            IntPtr scan = data.Scan0;

            int size = bitmap.Width * bitmap.Height * 4;


            unsafe
            {
                byte* p = (byte*)scan;

                for (int i = 0; i < size; i += 4)
                {
                    p[i] = rawData[i]; // red
                    p[i + 1] = rawData[i + 1]; // green
                    p[i + 2] = rawData[i + 2]; // blue
                    p[i + 3] = 255;
                }
            }


            bitmap.UnlockBits(data);

            return bitmap;
        }


        private Bitmap Create32BppBitmap(DDSStruct header, byte[] rawData, DxgiFormat dxgiFormat)
        {
            System.Drawing.Imaging.PixelFormat pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;

            Bitmap bitmap = new Bitmap((int)header.width, (int)header.height, pixelFormat);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, pixelFormat);
            IntPtr scan = data.Scan0;

            int size = bitmap.Width * bitmap.Height * 4;


            unsafe
            {
                byte* p = (byte*)scan;

                for (int i = 0; i < size; i += 4)
                {
                    switch (dxgiFormat)
                    {
                        case DxgiFormat.DXGI_FORMAT_R10G10B10A2_UNORM:

                            ConvertPixel_R10G10B10A2_To_R8G8B8A8(p, i, ref rawData);
                            break;

                        case DxgiFormat.DXGI_FORMAT_BC4_UNORM:
                        case DxgiFormat.DXGI_FORMAT_BC4_SNORM:
                        case DxgiFormat.DXGI_FORMAT_BC4_TYPELESS:
                        case DxgiFormat.DXGI_FORMAT_BC5_UNORM:
                        case DxgiFormat.DXGI_FORMAT_BC5_SNORM:
                        case DxgiFormat.DXGI_FORMAT_BC5_TYPELESS:
                        case DxgiFormat.DXGI_FORMAT_BC7_UNORM:
                        case DxgiFormat.DXGI_FORMAT_BC7_UNORM_SRGB:
                        case DxgiFormat.DXGI_FORMAT_BC7_TYPELESS:
                        default:

                            ConvertPixel_B8G8R8A8_To_R8G8B8A8(p, i, ref rawData);
                            break;
                    }
                }
            }


            bitmap.UnlockBits(data);

            return bitmap;
        }


        private unsafe void ConvertPixel_B8G8R8A8_To_R8G8B8A8(byte* result, int pixelIndex, ref byte[] rawData)
        {
            // Bitmap stores it's data in RGBA order.
            // DDS stores it's data in BGRA order.
            if (rawData.Length >= 4)
            {
                result[pixelIndex] = rawData[pixelIndex + 2]; // red
                result[pixelIndex + 1] = rawData[pixelIndex + 1]; // green
                result[pixelIndex + 2] = rawData[pixelIndex]; // blue
                result[pixelIndex + 3] = rawData[pixelIndex + 3]; // alpha
            }        
        }



        private static readonly int R10G10B10A2_RedMask = ((int)Math.Pow(2, 10) - 1) << 0;
        private static readonly int R10G10B10A2_GreenMask = ((int)Math.Pow(2, 10) - 1) << 10;
        private static readonly int R10G10B10A2_BlueMask = ((int)Math.Pow(2, 10) - 1) << 20;
        private static readonly int R10G10B10A2_AlphaMask = ((int)Math.Pow(2, 2) - 1) << 30;
        private static readonly int R10G10B10A2_RGBMax = ((int)Math.Pow(2, 10) - 1);
        private static readonly int R10G10B10A2_AMax = ((int)Math.Pow(2, 2) - 1);


        private unsafe void ConvertPixel_R10G10B10A2_To_R8G8B8A8(byte* result, int pixelIndex, ref byte[] rawData)
        {
            int pixelValue = BitConverter.ToInt32(rawData, pixelIndex);

            int red10 = (int)((((pixelValue & R10G10B10A2_RedMask) >> 0) / (Math.Pow(2, 10) - 1)) * (Math.Pow(2, 8) - 1));
            int green10 = (int)((((pixelValue & R10G10B10A2_GreenMask) >> 10) / (Math.Pow(2, 10) - 1)) * (Math.Pow(2, 8) - 1));
            int blue10 = (int)((((pixelValue & R10G10B10A2_BlueMask) >> 20) / (Math.Pow(2, 10) - 1)) * (Math.Pow(2, 8) - 1));
            int alpha2 = (int)((((pixelValue & R10G10B10A2_AlphaMask) >> 30) / (Math.Pow(2, 2) - 1)) * (Math.Pow(2, 8) - 1));

            if (!m_precisionWarned && 
                (red10 > R10G10B10A2_RGBMax || green10 > R10G10B10A2_RGBMax || blue10 > R10G10B10A2_RGBMax // <-- very possible, but not likely
                    || alpha2 > R10G10B10A2_AMax)) // <-- shouldn't be possible
            {
                //Log.Warning($"DdsImage->Parse->ConvertPixel_R10G10B10A2_To_R8G8B8A8: Loss of precision detected. {red10},{green10},{blue10},{alpha2} (RGBA) was truncated.");
                m_precisionWarned = true;
            }


            result[pixelIndex] = (byte)blue10; // blue
            result[pixelIndex + 1] = (byte)green10; // green
            result[pixelIndex + 2] = (byte)red10; // red
            result[pixelIndex + 3] = (byte)alpha2; // alpha
        }




        private bool ReadHeader(BinaryReader reader, ref DDSStruct header)
        {
            byte[] signature = reader.ReadBytes(4);
            if (!(signature[0] == 'D' && signature[1] == 'D' && signature[2] == 'S' && signature[3] == ' '))
                return false;

            header.size = reader.ReadUInt32();
            if (header.size != 124)
                return false;

            //convert the data
            header.flags = reader.ReadUInt32();
            header.height = reader.ReadUInt32();
            header.width = reader.ReadUInt32();
            header.sizeorpitch = reader.ReadUInt32();
            header.depth = reader.ReadUInt32();
            header.mipmapcount = reader.ReadUInt32();
            header.alphabitdepth = reader.ReadUInt32();

            header.reserved = new uint[10];
            for (int i = 0; i < 10; i++)
            {
                header.reserved[i] = reader.ReadUInt32();
            }

            //pixelfromat
            header.pixelformat.size = reader.ReadUInt32();
            header.pixelformat.flags = reader.ReadUInt32();
            header.pixelformat.fourcc = reader.ReadUInt32();
            header.pixelformat.rgbbitcount = reader.ReadUInt32();
            header.pixelformat.rbitmask = reader.ReadUInt32();
            header.pixelformat.gbitmask = reader.ReadUInt32();
            header.pixelformat.bbitmask = reader.ReadUInt32();
            header.pixelformat.alphabitmask = reader.ReadUInt32();

            //caps
            header.ddscaps.caps1 = reader.ReadUInt32();
            header.ddscaps.caps2 = reader.ReadUInt32();
            header.ddscaps.caps3 = reader.ReadUInt32();
            header.ddscaps.caps4 = reader.ReadUInt32();
            header.texturestage = reader.ReadUInt32();

            // check if DXT10
            if (header.pixelformat.fourcc == FOURCC_DXT10)
            {
                m_isDXT10 = true;

                header.dxt10_dxgiFormat = reader.ReadUInt32();
                header.dxt10_resourceDimension = reader.ReadUInt32();
                header.dxt10_miscFlag = reader.ReadUInt32();
                header.dxt10_arraySize = reader.ReadUInt32();
                header.dxt10_miscFlags2 = reader.ReadUInt32();
            }

            return true;
        }

        private PixelFormat GetFormat(DDSStruct header, ref uint blocksize)
        {
            PixelFormat format = PixelFormat.UNKNOWN;

            if ((header.pixelformat.flags & DDPF_FOURCC) == DDPF_FOURCC)
            {
                blocksize = ((header.width + 3) / 4) * ((header.height + 3) / 4) * header.depth;

                switch (header.pixelformat.fourcc)
                {
                    case FOURCC_DXT1:
                        format = PixelFormat.DXT1;
                        blocksize *= 8;
                        break;

                    case FOURCC_DXT2:
                        format = PixelFormat.DXT2;
                        blocksize *= 16;
                        break;

                    case FOURCC_DXT3:
                        format = PixelFormat.DXT3;
                        blocksize *= 16;
                        break;

                    case FOURCC_DXT4:
                        format = PixelFormat.DXT4;
                        blocksize *= 16;
                        break;

                    case FOURCC_DXT5:
                        format = PixelFormat.DXT5;
                        blocksize *= 16;
                        break;

                    case FOURCC_DXT10:
                        format = PixelFormat.DXT10;
                        blocksize *= 16;
                        break;

                    case FOURCC_BC4U:
                        format = PixelFormat.BC4U;
                        blocksize *= 8;
                        break;

                    case FOURCC_BC4S:
                        format = PixelFormat.BC4S;
                        blocksize *= 8;
                        break;

                    case FOURCC_ATI1:
                        format = PixelFormat.ATI1N;
                        blocksize *= 8;
                        break;

                    case FOURCC_ATI2:
                        format = PixelFormat.THREEDC;
                        blocksize *= 16;
                        break;

                    case FOURCC_RXGB:
                        format = PixelFormat.RXGB;
                        blocksize *= 16;
                        break;

                    case FOURCC_DOLLARNULL:
                        format = PixelFormat.A16B16G16R16;
                        blocksize = header.width * header.height * header.depth * 8;
                        break;

                    case FOURCC_oNULL:
                        format = PixelFormat.R16F;
                        blocksize = header.width * header.height * header.depth * 2;
                        break;

                    case FOURCC_pNULL:
                        format = PixelFormat.G16R16F;
                        blocksize = header.width * header.height * header.depth * 4;
                        break;

                    case FOURCC_qNULL:
                        format = PixelFormat.A16B16G16R16F;
                        blocksize = header.width * header.height * header.depth * 8;
                        break;

                    case FOURCC_rNULL:
                        format = PixelFormat.R32F;
                        blocksize = header.width * header.height * header.depth * 4;
                        break;

                    case FOURCC_sNULL:
                        format = PixelFormat.G32R32F;
                        blocksize = header.width * header.height * header.depth * 8;
                        break;

                    case FOURCC_tNULL:
                        format = PixelFormat.A32B32G32R32F;
                        blocksize = header.width * header.height * header.depth * 16;
                        break;

                    default:
                        format = PixelFormat.UNKNOWN;
                        blocksize *= 16;
                        break;
                } // switch
            }
            else
            {
                // uncompressed image
                if ((header.pixelformat.flags & DDPF_LUMINANCE) == DDPF_LUMINANCE)
                {
                    if ((header.pixelformat.flags & DDPF_ALPHAPIXELS) == DDPF_ALPHAPIXELS)
                    {
                        format = PixelFormat.LUMINANCE_ALPHA;
                    }
                    else
                    {
                        format = PixelFormat.LUMINANCE;
                    }
                }
                else
                {
                    if ((header.pixelformat.flags & DDPF_ALPHAPIXELS) == DDPF_ALPHAPIXELS)
                    {
                        format = PixelFormat.RGBA;
                    }
                    else
                    {
                        format = PixelFormat.RGB;
                    }
                }

                blocksize = (header.width * header.height * header.depth * (header.pixelformat.rgbbitcount >> 3));
            }

            return format;
        }

        #region Helper Methods
        // iCompFormatToBpp
        private uint PixelFormatToBpp(PixelFormat pf, uint rgbbitcount)
        {
            switch (pf)
            {
                case PixelFormat.LUMINANCE:
                case PixelFormat.LUMINANCE_ALPHA:
                case PixelFormat.RGBA:
                case PixelFormat.RGB:
                    return rgbbitcount / 8;

                case PixelFormat.THREEDC:
                case PixelFormat.RXGB:
                    return 3;

                case PixelFormat.ATI1N:
                    return 1;

                case PixelFormat.R16F:
                    return 2;

                case PixelFormat.A16B16G16R16:
                case PixelFormat.A16B16G16R16F:
                case PixelFormat.G32R32F:
                    return 8;

                case PixelFormat.A32B32G32R32F:
                    return 16;

                default:
                    return 4;
            }
        }

        // iCompFormatToBpc
        private uint PixelFormatToBpc(PixelFormat pf)
        {
            switch (pf)
            {
                case PixelFormat.R16F:
                case PixelFormat.G16R16F:
                case PixelFormat.A16B16G16R16F:
                    return 4;

                case PixelFormat.R32F:
                case PixelFormat.G32R32F:
                case PixelFormat.A32B32G32R32F:
                    return 4;

                case PixelFormat.A16B16G16R16:
                    return 2;

                default:
                    return 1;
            }
        }

        private bool Check16BitComponents(DDSStruct header)
        {
            if (header.pixelformat.rgbbitcount != 32)
                return false;
            // a2b10g10r10 format
            if (header.pixelformat.rbitmask == 0x3FF00000 && header.pixelformat.gbitmask == 0x000FFC00 && header.pixelformat.bbitmask == 0x000003FF
                && header.pixelformat.alphabitmask == 0xC0000000)
                return true;
            // a2r10g10b10 format
            else if (header.pixelformat.rbitmask == 0x000003FF && header.pixelformat.gbitmask == 0x000FFC00 && header.pixelformat.bbitmask == 0x3FF00000
                && header.pixelformat.alphabitmask == 0xC0000000)
                return true;

            return false;
        }

        private void CorrectPremult(uint pixnum, ref byte[] buffer)
        {
            for (uint i = 0; i < pixnum; i++)
            {
                byte alpha = buffer[i + 3];
                if (alpha == 0) continue;
                int red = (buffer[i] << 8) / alpha;
                int green = (buffer[i + 1] << 8) / alpha;
                int blue = (buffer[i + 2] << 8) / alpha;

                buffer[i] = (byte)red;
                buffer[i + 1] = (byte)green;
                buffer[i + 2] = (byte)blue;
            }
        }

        private void ComputeMaskParams(uint mask, ref int shift1, ref int mul, ref int shift2)
        {
            shift1 = 0; mul = 1; shift2 = 0;
            while ((mask & 1) == 0)
            {
                mask >>= 1;
                shift1++;
            }
            uint bc = 0;
            while ((mask & (1 << (int)bc)) != 0) bc++;
            while ((mask * mul) < 255)
                mul = (mul << (int)bc) + 1;
            mask *= (uint)mul;

            while ((mask & ~0xff) != 0)
            {
                mask >>= 1;
                shift2++;
            }
        }

        private unsafe void DxtcReadColors(byte* data, ref Colour8888[] op)
        {
            byte r0, g0, b0, r1, g1, b1;

            b0 = (byte)(data[0] & 0x1F);
            g0 = (byte)(((data[0] & 0xE0) >> 5) | ((data[1] & 0x7) << 3));
            r0 = (byte)((data[1] & 0xF8) >> 3);

            b1 = (byte)(data[2] & 0x1F);
            g1 = (byte)(((data[2] & 0xE0) >> 5) | ((data[3] & 0x7) << 3));
            r1 = (byte)((data[3] & 0xF8) >> 3);

            op[0].red = (byte)(r0 << 3 | r0 >> 2);
            op[0].green = (byte)(g0 << 2 | g0 >> 3);
            op[0].blue = (byte)(b0 << 3 | b0 >> 2);

            op[1].red = (byte)(r1 << 3 | r1 >> 2);
            op[1].green = (byte)(g1 << 2 | g1 >> 3);
            op[1].blue = (byte)(b1 << 3 | b1 >> 2);
        }

        private void DxtcReadColor(ushort data, ref Colour8888 op)
        {
            byte r, g, b;

            b = (byte)(data & 0x1f);
            g = (byte)((data & 0x7E0) >> 5);
            r = (byte)((data & 0xF800) >> 11);

            op.red = (byte)(r << 3 | r >> 2);
            op.green = (byte)(g << 2 | g >> 3);
            op.blue = (byte)(b << 3 | r >> 2);
        }

        private unsafe void DxtcReadColors(byte* data, ref Colour565 color_0, ref Colour565 color_1)
        {
            color_0.blue = (byte)(data[0] & 0x1F);
            color_0.green = (byte)(((data[0] & 0xE0) >> 5) | ((data[1] & 0x7) << 3));
            color_0.red = (byte)((data[1] & 0xF8) >> 3);

            color_0.blue = (byte)(data[2] & 0x1F);
            color_0.green = (byte)(((data[2] & 0xE0) >> 5) | ((data[3] & 0x7) << 3));
            color_0.red = (byte)((data[3] & 0xF8) >> 3);
        }

        private void GetBitsFromMask(uint mask, ref uint shiftLeft, ref uint shiftRight)
        {
            uint temp, i;

            if (mask == 0)
            {
                shiftLeft = shiftRight = 0;
                return;
            }

            temp = mask;
            for (i = 0; i < 32; i++, temp >>= 1)
            {
                if ((temp & 1) != 0)
                    break;
            }
            shiftRight = i;

            // Temp is preserved, so use it again:
            for (i = 0; i < 8; i++, temp >>= 1)
            {
                if ((temp & 1) == 0)
                    break;
            }
            shiftLeft = 8 - i;
        }

        // This function simply counts how many contiguous bits are in the mask.
        private uint CountBitsFromMask(uint mask)
        {
            uint i, testBit = 0x01, count = 0;
            bool foundBit = false;

            for (i = 0; i < 32; i++, testBit <<= 1)
            {
                if ((mask & testBit) != 0)
                {
                    if (!foundBit)
                        foundBit = true;
                    count++;
                }
                else if (foundBit)
                    return count;
            }

            return count;
        }

        private uint HalfToFloat(ushort y)
        {
            int s = (y >> 15) & 0x00000001;
            int e = (y >> 10) & 0x0000001f;
            int m = y & 0x000003ff;

            if (e == 0)
            {
                if (m == 0)
                {
                    //
                    // Plus or minus zero
                    //
                    return (uint)(s << 31);
                }
                else
                {
                    //
                    // Denormalized number -- renormalize it
                    //
                    while ((m & 0x00000400) == 0)
                    {
                        m <<= 1;
                        e -= 1;
                    }

                    e += 1;
                    m &= ~0x00000400;
                }
            }
            else if (e == 31)
            {
                if (m == 0)
                {
                    //
                    // Positive or negative infinity
                    //
                    return (uint)((s << 31) | 0x7f800000);
                }
                else
                {
                    //
                    // Nan -- preserve sign and significand bits
                    //
                    return (uint)((s << 31) | 0x7f800000 | (m << 13));
                }
            }

            //
            // Normalized number
            //
            e = e + (127 - 15);
            m = m << 13;

            //
            // Assemble s, e and m.
            //
            return (uint)((s << 31) | (e << 23) | m);
        }

        private unsafe void ConvFloat16ToFloat32(uint* dest, ushort* src, uint size)
        {
            uint i;
            for (i = 0; i < size; ++i, ++dest, ++src)
            {
                //float: 1 sign bit, 8 exponent bits, 23 mantissa bits
                //half: 1 sign bit, 5 exponent bits, 10 mantissa bits
                *dest = HalfToFloat(*src);
            }
        }

        private unsafe void ConvG16R16ToFloat32(uint* dest, ushort* src, uint size)
        {
            uint i;
            for (i = 0; i < size; i += 3)
            {
                //float: 1 sign bit, 8 exponent bits, 23 mantissa bits
                //half: 1 sign bit, 5 exponent bits, 10 mantissa bits
                *dest++ = HalfToFloat(*src++);
                *dest++ = HalfToFloat(*src++);
                *((float*)dest++) = 1.0f;
            }
        }

        private unsafe void ConvR16ToFloat32(uint* dest, ushort* src, uint size)
        {
            uint i;
            for (i = 0; i < size; i += 3)
            {
                //float: 1 sign bit, 8 exponent bits, 23 mantissa bits
                //half: 1 sign bit, 5 exponent bits, 10 mantissa bits
                *dest++ = HalfToFloat(*src++);
                *((float*)dest++) = 1.0f;
                *((float*)dest++) = 1.0f;
            }
        }
        #endregion

        #region Decompress Methods
        private byte[] DecompressData(DDSStruct header, byte[] data, PixelFormat pixelFormat, DxgiFormat dxgiFormat)
        {
            System.Diagnostics.Debug.WriteLine(pixelFormat);
            // allocate bitmap
            byte[] rawData = null;

            switch (pixelFormat)
            {
                case PixelFormat.RGBA:
                    rawData = this.DecompressRGBA(header, data, pixelFormat);
                    break;

                case PixelFormat.RGB:
                    rawData = this.DecompressRGB(header, data, pixelFormat);
                    break;

                case PixelFormat.LUMINANCE:
                case PixelFormat.LUMINANCE_ALPHA:
                    rawData = this.DecompressLum(header, data, pixelFormat);
                    break;

                case PixelFormat.DXT1:
                    rawData = this.DecompressDXT1(header, data, pixelFormat);
                    break;

                case PixelFormat.DXT2:
                    rawData = this.DecompressDXT2(header, data, pixelFormat);
                    break;

                case PixelFormat.DXT3:
                    rawData = this.DecompressDXT3(header, data, pixelFormat);
                    break;

                case PixelFormat.DXT4:
                    rawData = this.DecompressDXT4(header, data, pixelFormat);
                    break;

                case PixelFormat.DXT5:
                    rawData = this.DecompressDXT5(header, data, pixelFormat);
                    break;

                case PixelFormat.BC4U:
                case PixelFormat.BC4S:
                case PixelFormat.DXT10:
                    rawData = data;
                    break;

                case PixelFormat.THREEDC:
                    rawData = this.Decompress3Dc(header, data, pixelFormat);
                    break;

                case PixelFormat.ATI1N:
                    rawData = this.DecompressAti1n(header, data, pixelFormat);
                    break;

                case PixelFormat.RXGB:
                    rawData = this.DecompressRXGB(header, data, pixelFormat);
                    break;

                case PixelFormat.R16F:
                case PixelFormat.G16R16F:
                case PixelFormat.A16B16G16R16F:
                case PixelFormat.R32F:
                case PixelFormat.G32R32F:
                case PixelFormat.A32B32G32R32F:
                    rawData = this.DecompressFloat(header, data, pixelFormat);
                    break;

                default:
                    rawData = data;
                    break;
            }

            return rawData;
        }




        internal struct HDRColorA
        {
            //------// Properties \\--------------------------------------------\\

            //------\\ Properties //--------------------------------------------//



            //------// Fields \\------------------------------------------------\\
            public float r, g, b, a;
            //------\\ Fields //------------------------------------------------//



            //------// Constructors \\------------------------------------------\\
            public HDRColorA(float r, float g, float b, float a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }


            public HDRColorA(HDRColorA colorToClone)
            {
                this.r = colorToClone.r;
                this.g = colorToClone.g;
                this.b = colorToClone.b;
                this.a = colorToClone.a;
            }


            public HDRColorA(LDRColorA colorToClone)
            {
                this.r = colorToClone.r * (1.0f / 255.0f);
                this.g = colorToClone.g * (1.0f / 255.0f);
                this.b = colorToClone.b * (1.0f / 255.0f);
                this.a = colorToClone.a * (1.0f / 255.0f);
            }
            //------\\ Constructors //------------------------------------------//



            //------// Methods \\-----------------------------------------------\\
            public static HDRColorA operator +(HDRColorA c1, HDRColorA c2)
            {
                return new HDRColorA((c1.r + c2.r), (c1.g + c2.g), (c1.b + c2.b), (c1.a + c2.a));
            }


            public static HDRColorA operator -(HDRColorA c1, HDRColorA c2)
            {
                return new HDRColorA((c1.r - c2.r), (c1.g - c2.g), (c1.b - c2.b), (c1.a - c2.a));
            }


            public static HDRColorA operator *(HDRColorA c, float f)
            {
                return new HDRColorA((c.r * f), (c.g * f), (c.b * f), (c.a * f));
            }


            public static HDRColorA operator /(HDRColorA c, float f)
            {
                float fInv = 1.0f / f;
                return new HDRColorA((c.r * f), (c.g * f), (c.b * f), (c.a * f));
            }


            public static float operator *(HDRColorA c1, HDRColorA c2)
            {
                return ((c1.r * c2.r) + (c1.g * c2.g) + (c1.b * c2.b) + (c1.a * c2.a));
            }


            public static implicit operator HDRColorA(LDRColorA c)
            {
                return new HDRColorA((float)c.r, (float)c.g, (float)c.b, (float)c.a);
            }


            public void Clamp(float fMin, float fMax)
            {
                r = Math.Min(fMax, Math.Max(fMin, r));
                g = Math.Min(fMax, Math.Max(fMin, g));
                b = Math.Min(fMax, Math.Max(fMin, b));
                a = Math.Min(fMax, Math.Max(fMin, a));
            }


            public LDRColorA ToLDRColorA()
            {
                return new LDRColorA((byte)(r + 0.01f), (byte)(g + 0.01f), (byte)(b + 0.01f), (byte)(a + 0.01f));
            }
            //------\\ Methods //-----------------------------------------------//    
        }


        private static readonly int NUM_PIXELS_PER_BLOCK = 16;

        private static readonly byte BC7_MAX_REGIONS = 3;
        private static readonly byte BC7_NUM_CHANNELS = 4;
        private static readonly byte BC7_MAX_SHAPES = 64;

        private static readonly int BC67_WEIGHT_MAX = 64;
        private static readonly int BC67_WEIGHT_SHIFT = 6;
        private static readonly int BC67_WEIGHT_ROUND = 32;

        private static readonly byte[,,] g_aPartitionTable = new byte[3,64,16]
        {
            {   // 1 Region case has no subsets (all 0)
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },

            {   // BC6H/BC7 Partition Set for 2 Subsets
                { 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1 }, // Shape 0
                { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 }, // Shape 1
                { 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1 }, // Shape 2
                { 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1 }, // Shape 3
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1 }, // Shape 4
                { 0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1 }, // Shape 5
                { 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1 }, // Shape 6
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1 }, // Shape 7
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1 }, // Shape 8
                { 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // Shape 9
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1 }, // Shape 10
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1 }, // Shape 11
                { 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // Shape 12
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }, // Shape 13
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // Shape 14
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 }, // Shape 15
                { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1 }, // Shape 16
                { 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }, // Shape 17
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0 }, // Shape 18
                { 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0 }, // Shape 19
                { 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }, // Shape 20
                { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0 }, // Shape 21
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0 }, // Shape 22
                { 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1 }, // Shape 23
                { 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 }, // Shape 24
                { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0 }, // Shape 25
                { 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0 }, // Shape 26
                { 0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0 }, // Shape 27
                { 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0 }, // Shape 28
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 }, // Shape 29
                { 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0 }, // Shape 30
                { 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0 }, // Shape 31

                                                                    // BC7 Partition Set for 2 Subsets (second-half)
                { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 }, // Shape 32
                { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 }, // Shape 33
                { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0 }, // Shape 34
                { 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0 }, // Shape 35
                { 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 }, // Shape 36
                { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0 }, // Shape 37
                { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1 }, // Shape 38
                { 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1 }, // Shape 39
                { 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0 }, // Shape 40
                { 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0 }, // Shape 41
                { 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0 }, // Shape 42
                { 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0 }, // Shape 43
                { 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0 }, // Shape 44
                { 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1 }, // Shape 45
                { 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1 }, // Shape 46
                { 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0 }, // Shape 47
                { 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 }, // Shape 48
                { 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0 }, // Shape 49
                { 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0 }, // Shape 50
                { 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0 }, // Shape 51
                { 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1 }, // Shape 52
                { 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1 }, // Shape 53
                { 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0 }, // Shape 54
                { 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0 }, // Shape 55
                { 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1 }, // Shape 56
                { 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1 }, // Shape 57
                { 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, // Shape 58
                { 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1 }, // Shape 59
                { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1 }, // Shape 60
                { 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 }, // Shape 61
                { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0 }, // Shape 62
                { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1 }  // Shape 63
            },

            {   // BC7 Partition Set for 3 Subsets
                { 0, 0, 1, 1, 0, 0, 1, 1, 0, 2, 2, 1, 2, 2, 2, 2 }, // Shape 0
                { 0, 0, 0, 1, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2, 2, 1 }, // Shape 1
                { 0, 0, 0, 0, 2, 0, 0, 1, 2, 2, 1, 1, 2, 2, 1, 1 }, // Shape 2
                { 0, 2, 2, 2, 0, 0, 2, 2, 0, 0, 1, 1, 0, 1, 1, 1 }, // Shape 3
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2 }, // Shape 4
                { 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 2, 2, 0, 0, 2, 2 }, // Shape 5
                { 0, 0, 2, 2, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1 }, // Shape 6
                { 0, 0, 1, 1, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1 }, // Shape 7
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 }, // Shape 8
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2 }, // Shape 9
                { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2 }, // Shape 10
                { 0, 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2 }, // Shape 11
                { 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2 }, // Shape 12
                { 0, 1, 2, 2, 0, 1, 2, 2, 0, 1, 2, 2, 0, 1, 2, 2 }, // Shape 13
                { 0, 0, 1, 1, 0, 1, 1, 2, 1, 1, 2, 2, 1, 2, 2, 2 }, // Shape 14
                { 0, 0, 1, 1, 2, 0, 0, 1, 2, 2, 0, 0, 2, 2, 2, 0 }, // Shape 15
                { 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 2, 1, 1, 2, 2 }, // Shape 16
                { 0, 1, 1, 1, 0, 0, 1, 1, 2, 0, 0, 1, 2, 2, 0, 0 }, // Shape 17
                { 0, 0, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2 }, // Shape 18
                { 0, 0, 2, 2, 0, 0, 2, 2, 0, 0, 2, 2, 1, 1, 1, 1 }, // Shape 19
                { 0, 1, 1, 1, 0, 1, 1, 1, 0, 2, 2, 2, 0, 2, 2, 2 }, // Shape 20
                { 0, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 1, 2, 2, 2, 1 }, // Shape 21
                { 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 2, 0, 1, 2, 2 }, // Shape 22
                { 0, 0, 0, 0, 1, 1, 0, 0, 2, 2, 1, 0, 2, 2, 1, 0 }, // Shape 23
                { 0, 1, 2, 2, 0, 1, 2, 2, 0, 0, 1, 1, 0, 0, 0, 0 }, // Shape 24
                { 0, 0, 1, 2, 0, 0, 1, 2, 1, 1, 2, 2, 2, 2, 2, 2 }, // Shape 25
                { 0, 1, 1, 0, 1, 2, 2, 1, 1, 2, 2, 1, 0, 1, 1, 0 }, // Shape 26
                { 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 2, 1, 1, 2, 2, 1 }, // Shape 27
                { 0, 0, 2, 2, 1, 1, 0, 2, 1, 1, 0, 2, 0, 0, 2, 2 }, // Shape 28
                { 0, 1, 1, 0, 0, 1, 1, 0, 2, 0, 0, 2, 2, 2, 2, 2 }, // Shape 29
                { 0, 0, 1, 1, 0, 1, 2, 2, 0, 1, 2, 2, 0, 0, 1, 1 }, // Shape 30
                { 0, 0, 0, 0, 2, 0, 0, 0, 2, 2, 1, 1, 2, 2, 2, 1 }, // Shape 31
                { 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2, 2, 1, 2, 2, 2 }, // Shape 32
                { 0, 2, 2, 2, 0, 0, 2, 2, 0, 0, 1, 2, 0, 0, 1, 1 }, // Shape 33
                { 0, 0, 1, 1, 0, 0, 1, 2, 0, 0, 2, 2, 0, 2, 2, 2 }, // Shape 34
                { 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0 }, // Shape 35
                { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 0, 0, 0, 0 }, // Shape 36
                { 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0 }, // Shape 37
                { 0, 1, 2, 0, 2, 0, 1, 2, 1, 2, 0, 1, 0, 1, 2, 0 }, // Shape 38
                { 0, 0, 1, 1, 2, 2, 0, 0, 1, 1, 2, 2, 0, 0, 1, 1 }, // Shape 39
                { 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 0, 0, 0, 0, 1, 1 }, // Shape 40
                { 0, 1, 0, 1, 0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2 }, // Shape 41
                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 1, 2, 1, 2, 1 }, // Shape 42
                { 0, 0, 2, 2, 1, 1, 2, 2, 0, 0, 2, 2, 1, 1, 2, 2 }, // Shape 43
                { 0, 0, 2, 2, 0, 0, 1, 1, 0, 0, 2, 2, 0, 0, 1, 1 }, // Shape 44
                { 0, 2, 2, 0, 1, 2, 2, 1, 0, 2, 2, 0, 1, 2, 2, 1 }, // Shape 45
                { 0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 0, 1 }, // Shape 46
                { 0, 0, 0, 0, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 }, // Shape 47
                { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2, 2, 2, 2 }, // Shape 48
                { 0, 2, 2, 2, 0, 1, 1, 1, 0, 2, 2, 2, 0, 1, 1, 1 }, // Shape 49
                { 0, 0, 0, 2, 1, 1, 1, 2, 0, 0, 0, 2, 1, 1, 1, 2 }, // Shape 50
                { 0, 0, 0, 0, 2, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2 }, // Shape 51
                { 0, 2, 2, 2, 0, 1, 1, 1, 0, 1, 1, 1, 0, 2, 2, 2 }, // Shape 52
                { 0, 0, 0, 2, 1, 1, 1, 2, 1, 1, 1, 2, 0, 0, 0, 2 }, // Shape 53
                { 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 2, 2, 2, 2 }, // Shape 54
                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2, 2, 1, 1, 2 }, // Shape 55
                { 0, 1, 1, 0, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2 }, // Shape 56
                { 0, 0, 2, 2, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 2, 2 }, // Shape 57
                { 0, 0, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2, 0, 0, 2, 2 }, // Shape 58
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2 }, // Shape 59
                { 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1 }, // Shape 60
                { 0, 2, 2, 2, 1, 2, 2, 2, 0, 2, 2, 2, 1, 2, 2, 2 }, // Shape 61
                { 0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Shape 62
                { 0, 1, 1, 1, 2, 0, 1, 1, 2, 2, 0, 1, 2, 2, 2, 0 }  // Shape 63
            }
        };

        private static readonly byte[,,] g_aFixUp = new byte[3,64,3]
        {
            {   // No fix-ups for 1st subset for BC6H or BC7
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },
                { 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 },{ 0, 0, 0 }
            },

            {   // BC6H/BC7 Partition Set Fixups for 2 Subsets
                { 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },
                { 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },
                { 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },
                { 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },
                { 0,15, 0 },{ 0, 2, 0 },{ 0, 8, 0 },{ 0, 2, 0 },
                { 0, 2, 0 },{ 0, 8, 0 },{ 0, 8, 0 },{ 0,15, 0 },
                { 0, 2, 0 },{ 0, 8, 0 },{ 0, 2, 0 },{ 0, 2, 0 },
                { 0, 8, 0 },{ 0, 8, 0 },{ 0, 2, 0 },{ 0, 2, 0 },

                // BC7 Partition Set Fixups for 2 Subsets (second-half)
                { 0,15, 0 },{ 0,15, 0 },{ 0, 6, 0 },{ 0, 8, 0 },
                { 0, 2, 0 },{ 0, 8, 0 },{ 0,15, 0 },{ 0,15, 0 },
                { 0, 2, 0 },{ 0, 8, 0 },{ 0, 2, 0 },{ 0, 2, 0 },
                { 0, 2, 0 },{ 0,15, 0 },{ 0,15, 0 },{ 0, 6, 0 },
                { 0, 6, 0 },{ 0, 2, 0 },{ 0, 6, 0 },{ 0, 8, 0 },
                { 0,15, 0 },{ 0,15, 0 },{ 0, 2, 0 },{ 0, 2, 0 },
                { 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },{ 0,15, 0 },
                { 0,15, 0 },{ 0, 2, 0 },{ 0, 2, 0 },{ 0,15, 0 }
            },

            {   // BC7 Partition Set Fixups for 3 Subsets
                { 0, 3,15 },{ 0, 3, 8 },{ 0,15, 8 },{ 0,15, 3 },
                { 0, 8,15 },{ 0, 3,15 },{ 0,15, 3 },{ 0,15, 8 },
                { 0, 8,15 },{ 0, 8,15 },{ 0, 6,15 },{ 0, 6,15 },
                { 0, 6,15 },{ 0, 5,15 },{ 0, 3,15 },{ 0, 3, 8 },
                { 0, 3,15 },{ 0, 3, 8 },{ 0, 8,15 },{ 0,15, 3 },
                { 0, 3,15 },{ 0, 3, 8 },{ 0, 6,15 },{ 0,10, 8 },
                { 0, 5, 3 },{ 0, 8,15 },{ 0, 8, 6 },{ 0, 6,10 },
                { 0, 8,15 },{ 0, 5,15 },{ 0,15,10 },{ 0,15, 8 },
                { 0, 8,15 },{ 0,15, 3 },{ 0, 3,15 },{ 0, 5,10 },
                { 0, 6,10 },{ 0,10, 8 },{ 0, 8, 9 },{ 0,15,10 },
                { 0,15, 6 },{ 0, 3,15 },{ 0,15, 8 },{ 0, 5,15 },
                { 0,15, 3 },{ 0,15, 6 },{ 0,15, 6 },{ 0,15, 8 },
                { 0, 3,15 },{ 0,15, 3 },{ 0, 5,15 },{ 0, 5,15 },
                { 0, 5,15 },{ 0, 8,15 },{ 0, 5,15 },{ 0,10,15 },
                { 0, 5,15 },{ 0,10,15 },{ 0, 8,15 },{ 0,13,15 },
                { 0,15, 3 },{ 0,12,15 },{ 0, 3,15 },{ 0, 3, 8 }
            }
        };

        private static readonly int[] g_aWeights2 = new int[] { 0, 21, 43, 64 };
        private static readonly int[] g_aWeights3 = new int[] { 0, 9, 18, 27, 37, 46, 55, 64 };
        private static readonly int[] g_aWeights4 = new int[] { 0, 4, 9, 13, 17, 21, 26, 30, 34, 38, 43, 47, 51, 55, 60, 64 };


        internal struct LDRColorA
        {
            //------// Properties \\--------------------------------------------\\
            public byte this[int i]
            {
                get
                {
                    switch (i)
                    {
                        case 0: return r;
                        case 1: return g;
                        case 2: return b;
                        case 3: return a;
                        default: return r;
                    }
                }
                set
                {
                    switch (i)
                    {
                        case 0: r = value; break;
                        case 1: g = value; break;
                        case 2: b = value; break;
                        case 3: a = value; break;
                    }
                }
            }
            //------\\ Properties //--------------------------------------------//



            //------// Fields \\------------------------------------------------\\
            public byte r, g, b, a;
            //------\\ Fields //------------------------------------------------//



            //------// Constructors \\------------------------------------------\\
            public LDRColorA(byte r, byte g, byte b, byte a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }
            //------\\ Constructors //------------------------------------------//



            //------// Methods \\-----------------------------------------------\\
            public static implicit operator LDRColorA(HDRColorA hdr)
            {
                HDRColorA temp = new HDRColorA(hdr);
                temp.Clamp(0.0f, 1.0f);
                temp *= 255.0f;

                return new LDRColorA((byte)(temp.r + 0.001f), (byte)(temp.g + 0.001f), (byte)(temp.b + 0.001f), (byte)(temp.a + 0.001f));
            }


            public static void InterpolateRGB(LDRColorA c1, LDRColorA c2, int wc, int wcprec, ref LDRColorA result)
            {
                int[] aWeights;

                switch (wcprec)
                {
                    case 2: aWeights = g_aWeights2; break;
                    case 3: aWeights = g_aWeights3; break;
                    case 4: aWeights = g_aWeights4; break;
                    default: result.r = result.g = result.b = 0; return;
                }

                result.r = (byte)(((c1.r * (BC67_WEIGHT_MAX - aWeights[wc])) + (c2.r * aWeights[wc]) + BC67_WEIGHT_ROUND) >> BC67_WEIGHT_SHIFT);
                result.g = (byte)(((c1.g * (BC67_WEIGHT_MAX - aWeights[wc])) + (c2.g * aWeights[wc]) + BC67_WEIGHT_ROUND) >> BC67_WEIGHT_SHIFT);
                result.b = (byte)(((c1.b * (BC67_WEIGHT_MAX - aWeights[wc])) + (c2.b * aWeights[wc]) + BC67_WEIGHT_ROUND) >> BC67_WEIGHT_SHIFT);
            }


            public static void InterpolateA(LDRColorA c1, LDRColorA c2, int wa, int waprec, ref LDRColorA result)
            {
                int[] aWeights;

                switch (waprec)
                {
                    case 2: aWeights = g_aWeights2; break;
                    case 3: aWeights = g_aWeights3; break;
                    case 4: aWeights = g_aWeights4; break;
                    default: result.a = 0; return;
                }

                result.a = (byte)(((c1.a * (BC67_WEIGHT_MAX - aWeights[wa])) + (c2.a * aWeights[wa]) + BC67_WEIGHT_ROUND) >> BC67_WEIGHT_SHIFT);
            }


            public static void Interpolate(LDRColorA c1, LDRColorA c2, int wc, int wa, int wcprec, int waprec, ref LDRColorA result)
            {
                InterpolateRGB(c1, c2, wc, wcprec, ref result);
                InterpolateA(c1, c2, wa, waprec, ref result);
            }
            //------\\ Methods //-----------------------------------------------//
        }



        internal class LDREndPntPair
        {
            //------// Properties \\--------------------------------------------\\

            //------\\ Properties //--------------------------------------------//



            //------// Fields \\------------------------------------------------\\
            public LDRColorA A;
            public LDRColorA B;
            //------\\ Fields //------------------------------------------------//



            //------// Constructors \\------------------------------------------\\
            public LDREndPntPair()
            { }
            //------\\ Constructors //------------------------------------------//



            //------// Methods \\-----------------------------------------------\\
            
            //------\\ Methods //-----------------------------------------------//
        }



        internal class ModeInfoBC7
        {
            //------// Properties \\--------------------------------------------\\

            //------\\ Properties //--------------------------------------------//



            //------// Fields \\------------------------------------------------\\
            public byte uPartitions;
            public byte uPartitionBits;
            public byte uPBits;
            public byte uRotationBits;
            public byte uIndexModeBits;
            public byte uIndexPrec;
            public byte uIndexPrec2;
            public LDRColorA RGBAPrec;
            public LDRColorA RGBAPrecWithP;
            //------\\ Fields //------------------------------------------------//



            //------// Constructors \\------------------------------------------\\
            public ModeInfoBC7()
            { }


            public ModeInfoBC7(
                byte uPartitions,
                byte uPartitionBits,
                byte uPBits,
                byte uRotationBits,
                byte uIndexModeBits,
                byte uIndexPrec,
                byte uIndexPrec2, 
                LDRColorA RGBAPrec, 
                LDRColorA RGBAPrecWithP)
            {
                this.uPartitions = uPartitions;
                this.uPartitionBits = uPartitionBits;
                this.uPBits = uPBits;
                this.uRotationBits = uRotationBits;
                this.uIndexModeBits = uIndexModeBits;
                this.uIndexPrec = uIndexPrec;
                this.uIndexPrec2 = uIndexPrec2;
                this.RGBAPrec = RGBAPrec;
                this.RGBAPrecWithP = RGBAPrecWithP;
            }
            //------\\ Constructors //------------------------------------------//



            //------// Methods \\-----------------------------------------------\\

            //------\\ Methods //-----------------------------------------------//
        }



        private static readonly ModeInfoBC7[] aInfo_BC7 = new ModeInfoBC7[]
        {
            new ModeInfoBC7(2, 4, 6, 0, 0, 3, 0, new LDRColorA(4, 4, 4, 0), new LDRColorA(5, 5, 5, 0)), // Mode 0: Color only, 3 Subsets, RGBP 4441 (unique P-bit), 3-bit indecies, 16 partitions
            new ModeInfoBC7(1, 6, 2, 0, 0, 3, 0, new LDRColorA(6, 6, 6, 0), new LDRColorA(7, 7, 7, 0)), // Mode 1: Color only, 2 Subsets, RGBP 6661 (shared P-bit), 3-bit indecies, 64 partitions
            new ModeInfoBC7(2, 6, 0, 0, 0, 2, 0, new LDRColorA(5, 5, 5, 0), new LDRColorA(5, 5, 5, 0)), // Mode 2: Color only, 3 Subsets, RGB 555, 2-bit indecies, 64 partitions
            new ModeInfoBC7(1, 6, 4, 0, 0, 2, 0, new LDRColorA(7, 7, 7, 0), new LDRColorA(8, 8, 8, 0)), // Mode 3: Color only, 2 Subsets, RGBP 7771 (unique P-bit), 2-bits indecies, 64 partitions
            new ModeInfoBC7(0, 0, 0, 2, 1, 2, 3, new LDRColorA(5, 5, 5, 6), new LDRColorA(5, 5, 5, 6)), // Mode 4: Color w/ Separate Alpha, 1 Subset, RGB 555, A6, 16x2/16x3-bit indices, 2-bit rotation, 1-bit index selector
            new ModeInfoBC7(0, 0, 0, 2, 0, 2, 2, new LDRColorA(7, 7, 7, 8), new LDRColorA(7, 7, 7, 8)), // Mode 5: Color w/ Separate Alpha, 1 Subset, RGB 777, A8, 16x2/16x2-bit indices, 2-bit rotation
            new ModeInfoBC7(0, 0, 2, 0, 0, 4, 0, new LDRColorA(7, 7, 7, 7), new LDRColorA(8, 8, 8, 8)), // Mode 6: Color+Alpha, 1 Subset, RGBAP 77771 (unique P-bit), 16x4-bit indecies
            new ModeInfoBC7(1, 6, 4, 0, 0, 2, 0, new LDRColorA(5, 5, 5, 5), new LDRColorA(6, 6, 6, 6)) // Mode 7: Color+Alpha, 2 Subsets, RGBAP 55551 (unique P-bit), 2-bit indices, 64 partitions
        };



        private unsafe byte[] DecompressBC7(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            byte[] outData = new byte[header.width * header.height * 4];
            int outDataIndex = 0;

            for (int index = 0; index < 100000; index++) //(outData.Length / NUM_PIXELS_PER_BLOCK); index++)
            {
                int uFirst = 0;
                while (uFirst < 128 && GetBit(data, ref uFirst) == 0) { }
                int uMode = uFirst - 1;

                if (uMode < 8)
                {
                    byte uPartitions = aInfo_BC7[uMode].uPartitions;

                    byte uNumEndPts = (byte)((uPartitions + 1) << 1);
                    byte uIndexPrec = aInfo_BC7[uMode].uIndexPrec;
                    byte uIndexPrec2 = aInfo_BC7[uMode].uIndexPrec2;

                    int i;
                    int uStartBit = uMode + 1;
                    byte[] p = new byte[6];

                    byte uShape = GetBits(data, ref uStartBit, aInfo_BC7[uMode].uPartitionBits);

                    byte uRotation = GetBits(data, ref uStartBit, aInfo_BC7[uMode].uRotationBits);
                    byte uIndexMode = GetBits(data, ref uStartBit, aInfo_BC7[uMode].uIndexModeBits);

                    LDRColorA[] c = new LDRColorA[BC7_MAX_REGIONS << 1];
                    LDRColorA RGBAPrec = aInfo_BC7[uMode].RGBAPrec;
                    LDRColorA RGBAPrecWithP = aInfo_BC7[uMode].RGBAPrecWithP;


                    // Red channel
                    for (i = 0; i < uNumEndPts; i++)
                    {
                        if (uStartBit + RGBAPrec.r > 128)
                        {
                            return outData;
                        }

                        c[i].r = GetBits(data, ref uStartBit, RGBAPrec.r);
                    }


                    // Green channel
                    for (i = 0; i < uNumEndPts; i++)
                    {
                        if (uStartBit + RGBAPrec.g > 128)
                        {
                            return outData;
                        }

                        c[i].g = GetBits(data, ref uStartBit, RGBAPrec.g);
                    }


                    // Blue channel
                    for (i = 0; i < uNumEndPts; i++)
                    {
                        if (uStartBit + RGBAPrec.b > 128)
                        {
                            return outData;
                        }

                        c[i].b = GetBits(data, ref uStartBit, RGBAPrec.b);
                    }


                    // Alpha channel
                    for (i = 0; i < uNumEndPts; i++)
                    {
                        if (uStartBit + RGBAPrec.a > 128)
                        {
                            return outData;
                        }

                        c[i].a = (RGBAPrec.a == 1 ? GetBits(data, ref uStartBit, RGBAPrec.a) : (byte)255);
                    }


                    // P-bits
                    for (i = 0; i < aInfo_BC7[uMode].uPBits; i++)
                    {
                        p[i] = GetBit(data, ref uStartBit);
                    }


                    if (aInfo_BC7[uMode].uPBits == 1)
                    {
                        for (i = 0; i < uNumEndPts; i++)
                        {
                            int pi = i * aInfo_BC7[uMode].uPBits / uNumEndPts;

                            for (byte ch = 0; ch < BC7_NUM_CHANNELS; ch++)
                            {
                                if (RGBAPrec[ch] != RGBAPrecWithP[ch])
                                {
                                    c[i][ch] = (byte)((c[i][ch] << 1) | p[pi]);
                                }
                            }
                        }
                    }


                    for (i = 0; i < uNumEndPts; i++)
                    {
                        c[i] = Unquantize_BC7(c[i], RGBAPrecWithP);
                    }


                    byte[] w1 = new byte[NUM_PIXELS_PER_BLOCK], w2 = new byte[NUM_PIXELS_PER_BLOCK];


                    // read color indices
                    for (i = 0; i < NUM_PIXELS_PER_BLOCK; i++)
                    {
                        int uNumBits = IsFixUpOffset(aInfo_BC7[uMode].uPartitions, uShape, i) ? uIndexPrec - 1 : uIndexPrec;

                        w1[i] = GetBits(data, ref uStartBit, uNumBits);
                    }


                    if (uIndexPrec2 == 1)
                    {
                        for (i = 0; i < NUM_PIXELS_PER_BLOCK; i++)
                        {
                            int uNumBits = (i == 1 ? uIndexPrec2 : (uIndexPrec2 - 1));

                            w2[i] = GetBits(data, ref uStartBit, uNumBits);
                        }
                    }


                    for (i = 0; i < NUM_PIXELS_PER_BLOCK; ++i)
                    {
                        byte uRegion = g_aPartitionTable[uPartitions, uShape, i];
                        LDRColorA outPixel = new LDRColorA(0, 0, 0, 0);

                        if (uIndexPrec2 == 0)
                        {
                            LDRColorA.Interpolate(c[uRegion << 1], c[(uRegion << 1) + 1], w1[i], w1[i], uIndexPrec, uIndexPrec, ref outPixel);
                        }
                        else
                        {
                            if (uIndexMode == 0)
                            {
                                LDRColorA.Interpolate(c[uRegion << 1], c[(uRegion << 1) + 1], w1[i], w2[i], uIndexPrec, uIndexPrec2, ref outPixel);
                            }
                            else
                            {
                                LDRColorA.Interpolate(c[uRegion << 1], c[(uRegion << 1) + 1], w2[i], w1[i], uIndexPrec2, uIndexPrec, ref outPixel);
                            }
                        }


                        switch (uRotation)
                        {
                            case 1:

                                byte temp = outPixel.a;
                                outPixel.a = outPixel.r;
                                outPixel.r = temp;

                                break;

                            case 2:

                                temp = outPixel.a;
                                outPixel.a = outPixel.g;
                                outPixel.g = temp;

                                break;

                            case 3:

                                temp = outPixel.a;
                                outPixel.a = outPixel.b;
                                outPixel.b = temp;

                                break;
                        }


                        outData[outDataIndex] = outPixel.r;
                        outData[outDataIndex + 1] = outPixel.g;
                        outData[outDataIndex + 2] = outPixel.b;
                        outData[outDataIndex + 3] = outPixel.a;

                        outDataIndex += 4;
                    }
                }
            }


            return outData;
        }


        private byte GetBit(byte[] data, ref int uStartBit)
        {
            int uIndex = uStartBit >> 3;
            byte ret = (byte)((data[uIndex] >> (uStartBit - (uIndex << 3))) & 0x01);

            uStartBit++;

            return ret;
        }


        private byte GetBits(byte[] data, ref int uStartBit, int uNumBits)
        {
            if (uNumBits == 0)
            {
                return 0;
            }


            byte ret;
            int uIndex = uStartBit >> 3;
            int uBase = uStartBit - (uIndex << 3);

            if (uBase + uNumBits > 8)
            {
                int uFirstIndexBits = 8 - uBase;
                int uNextIndexBits = uNumBits - uFirstIndexBits;

                ret = (byte)((data[uIndex] >> uBase) | ((data[uIndex + 1] & ((1 << uNextIndexBits) - 1)) << uFirstIndexBits));
            }
            else
            {
                ret = (byte)((data[uIndex] >> uBase) & ((1 << uNumBits) - 1));
            }
            
            uStartBit += uNumBits;

            return ret;
        }


        private byte Unquantize_BC7(byte comp, int uPrec)
        {
            comp = (byte)(comp << (8 - uPrec));
            return (byte)(comp | (comp >> uPrec));
        }


        private LDRColorA Unquantize_BC7(LDRColorA c, LDRColorA RGBAPrec)
        {
            LDRColorA q;
            q.r = Unquantize_BC7(c.r, RGBAPrec.r);
            q.g = Unquantize_BC7(c.g, RGBAPrec.g);
            q.b = Unquantize_BC7(c.b, RGBAPrec.b);
            q.a = (RGBAPrec.a > 0 ? Unquantize_BC7(c.a, RGBAPrec.a) : (byte)255);

            return q;
        }


        private bool IsFixUpOffset(int uPartitions, int uShape, int uOffset)
        {
            for (int p = 0; p <= uPartitions; p++)
            {
                if (uOffset == g_aFixUp[uPartitions, uShape, p])
                {
                    return true;
                }
            }

            return false;
        }





        private unsafe byte[] DecompressDXT1(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            // DXT1 decompressor
            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];

            Colour8888[] colours = new Colour8888[4];
            colours[0].alpha = 0xFF;
            colours[1].alpha = 0xFF;
            colours[2].alpha = 0xFF;

            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        for (int x = 0; x < width; x += 4)
                        {
                            ushort colour0 = *((ushort*)temp);
                            ushort colour1 = *((ushort*)(temp + 2));
                            DxtcReadColor(colour0, ref colours[0]);
                            DxtcReadColor(colour1, ref colours[1]);

                            uint bitmask = ((uint*)temp)[1];
                            temp += 8;

                            if (colour0 > colour1)
                            {
                                // Four-color block: derive the other two colors.
                                // 00 = color_0, 01 = color_1, 10 = color_2, 11 = color_3
                                // These 2-bit codes correspond to the 2-bit fields
                                // stored in the 64-bit block.
                                colours[2].blue = (byte)((2 * colours[0].blue + colours[1].blue + 1) / 3);
                                colours[2].green = (byte)((2 * colours[0].green + colours[1].green + 1) / 3);
                                colours[2].red = (byte)((2 * colours[0].red + colours[1].red + 1) / 3);
                                //colours[2].alpha = 0xFF;

                                colours[3].blue = (byte)((colours[0].blue + 2 * colours[1].blue + 1) / 3);
                                colours[3].green = (byte)((colours[0].green + 2 * colours[1].green + 1) / 3);
                                colours[3].red = (byte)((colours[0].red + 2 * colours[1].red + 1) / 3);
                                colours[3].alpha = 0xFF;
                            }
                            else
                            {
                                // Three-color block: derive the other color.
                                // 00 = color_0,  01 = color_1,  10 = color_2,
                                // 11 = transparent.
                                // These 2-bit codes correspond to the 2-bit fields 
                                // stored in the 64-bit block. 
                                colours[2].blue = (byte)((colours[0].blue + colours[1].blue) / 2);
                                colours[2].green = (byte)((colours[0].green + colours[1].green) / 2);
                                colours[2].red = (byte)((colours[0].red + colours[1].red) / 2);
                                //colours[2].alpha = 0xFF;

                                colours[3].blue = (byte)((colours[0].blue + 2 * colours[1].blue + 1) / 3);
                                colours[3].green = (byte)((colours[0].green + 2 * colours[1].green + 1) / 3);
                                colours[3].red = (byte)((colours[0].red + 2 * colours[1].red + 1) / 3);
                                colours[3].alpha = 0x00;
                            }

                            for (int j = 0, k = 0; j < 4; j++)
                            {
                                for (int i = 0; i < 4; i++, k++)
                                {
                                    int select = (int)((bitmask & (0x03 << k * 2)) >> k * 2);
                                    Colour8888 col = colours[select];
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp);
                                        rawData[offset + 0] = (byte)col.red;
                                        rawData[offset + 1] = (byte)col.green;
                                        rawData[offset + 2] = (byte)col.blue;
                                        rawData[offset + 3] = (byte)col.alpha;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return rawData;
        }

        private byte[] DecompressDXT2(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            // Can do color & alpha same as dxt3, but color is pre-multiplied
            // so the result will be wrong unless corrected.
            byte[] rawData = DecompressDXT3(header, data, pixelFormat);
            CorrectPremult((uint)(width * height * depth), ref rawData);

            return rawData;
        }

        private unsafe byte[] DecompressDXT3(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            // DXT3 decompressor
            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];
            Colour8888[] colours = new Colour8888[4];

            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        for (int x = 0; x < width; x += 4)
                        {
                            byte* alpha = temp;
                            temp += 8;

                            DxtcReadColors(temp, ref colours);
                            temp += 4;

                            uint bitmask = ((uint*)temp)[1];
                            temp += 4;

                            // Four-color block: derive the other two colors.
                            // 00 = color_0, 01 = color_1, 10 = color_2, 11	= color_3
                            // These 2-bit codes correspond to the 2-bit fields
                            // stored in the 64-bit block.
                            colours[2].blue = (byte)((2 * colours[0].blue + colours[1].blue + 1) / 3);
                            colours[2].green = (byte)((2 * colours[0].green + colours[1].green + 1) / 3);
                            colours[2].red = (byte)((2 * colours[0].red + colours[1].red + 1) / 3);
                            //colours[2].alpha = 0xFF;

                            colours[3].blue = (byte)((colours[0].blue + 2 * colours[1].blue + 1) / 3);
                            colours[3].green = (byte)((colours[0].green + 2 * colours[1].green + 1) / 3);
                            colours[3].red = (byte)((colours[0].red + 2 * colours[1].red + 1) / 3);
                            //colours[3].alpha = 0xFF;

                            for (int j = 0, k = 0; j < 4; j++)
                            {
                                for (int i = 0; i < 4; k++, i++)
                                {
                                    int select = (int)((bitmask & (0x03 << k * 2)) >> k * 2);

                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp);
                                        rawData[offset + 0] = (byte)colours[select].red;
                                        rawData[offset + 1] = (byte)colours[select].green;
                                        rawData[offset + 2] = (byte)colours[select].blue;
                                    }
                                }
                            }

                            for (int j = 0; j < 4; j++)
                            {
                                //ushort word = (ushort)(alpha[2 * j] + 256 * alpha[2 * j + 1]);
                                ushort word = (ushort)(alpha[2 * j] | (alpha[2 * j + 1] << 8));
                                for (int i = 0; i < 4; i++)
                                {
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp + 3);
                                        rawData[offset] = (byte)(word & 0x0F);
                                        rawData[offset] = (byte)(rawData[offset] | (rawData[offset] << 4));
                                    }
                                    word >>= 4;
                                }
                            }
                        }
                    }
                }
            }
            return rawData;
        }

        private byte[] DecompressDXT4(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            // Can do color & alpha same as dxt5, but color is pre-multiplied
            // so the result will be wrong unless corrected.
            byte[] rawData = DecompressDXT5(header, data, pixelFormat);
            CorrectPremult((uint)(width * height * depth), ref rawData);

            return rawData;
        }

        private unsafe byte[] DecompressDXT5(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];
            Colour8888[] colours = new Colour8888[4];
            ushort[] alphas = new ushort[8];

            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        for (int x = 0; x < width; x += 4)
                        {
                            if (y >= height || x >= width)
                                break;

                            alphas[0] = temp[0];
                            alphas[1] = temp[1];
                            byte* alphamask = (temp + 2);
                            temp += 8;

                            DxtcReadColors(temp, ref colours);
                            uint bitmask = ((uint*)temp)[1];
                            temp += 8;

                            // Four-color block: derive the other two colors.
                            // 00 = color_0, 01 = color_1, 10 = color_2, 11	= color_3
                            // These 2-bit codes correspond to the 2-bit fields
                            // stored in the 64-bit block.
                            colours[2].blue = (byte)((2 * colours[0].blue + colours[1].blue + 1) / 3);
                            colours[2].green = (byte)((2 * colours[0].green + colours[1].green + 1) / 3);
                            colours[2].red = (byte)((2 * colours[0].red + colours[1].red + 1) / 3);
                            //colours[2].alpha = 0xFF;

                            colours[3].blue = (byte)((colours[0].blue + 2 * colours[1].blue + 1) / 3);
                            colours[3].green = (byte)((colours[0].green + 2 * colours[1].green + 1) / 3);
                            colours[3].red = (byte)((colours[0].red + 2 * colours[1].red + 1) / 3);
                            //colours[3].alpha = 0xFF;

                            int k = 0;
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < 4; k++, i++)
                                {
                                    int select = (int)((bitmask & (0x03 << k * 2)) >> k * 2);
                                    Colour8888 col = colours[select];
                                    // only put pixels out < width or height
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp);
                                        rawData[offset] = (byte)col.red;
                                        rawData[offset + 1] = (byte)col.green;
                                        rawData[offset + 2] = (byte)col.blue;
                                    }
                                }
                            }

                            // 8-alpha or 6-alpha block?
                            if (alphas[0] > alphas[1])
                            {
                                // 8-alpha block:  derive the other six alphas.
                                // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                                alphas[2] = (ushort)((6 * alphas[0] + 1 * alphas[1] + 3) / 7); // bit code 010
                                alphas[3] = (ushort)((5 * alphas[0] + 2 * alphas[1] + 3) / 7); // bit code 011
                                alphas[4] = (ushort)((4 * alphas[0] + 3 * alphas[1] + 3) / 7); // bit code 100
                                alphas[5] = (ushort)((3 * alphas[0] + 4 * alphas[1] + 3) / 7); // bit code 101
                                alphas[6] = (ushort)((2 * alphas[0] + 5 * alphas[1] + 3) / 7); // bit code 110
                                alphas[7] = (ushort)((1 * alphas[0] + 6 * alphas[1] + 3) / 7); // bit code 111
                            }
                            else
                            {
                                // 6-alpha block.
                                // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                                alphas[2] = (ushort)((4 * alphas[0] + 1 * alphas[1] + 2) / 5); // Bit code 010
                                alphas[3] = (ushort)((3 * alphas[0] + 2 * alphas[1] + 2) / 5); // Bit code 011
                                alphas[4] = (ushort)((2 * alphas[0] + 3 * alphas[1] + 2) / 5); // Bit code 100
                                alphas[5] = (ushort)((1 * alphas[0] + 4 * alphas[1] + 2) / 5); // Bit code 101
                                alphas[6] = 0x00; // Bit code 110
                                alphas[7] = 0xFF; // Bit code 111
                            }

                            // Note: Have to separate the next two loops,
                            // it operates on a 6-byte system.

                            // First three bytes
                            //uint bits = (uint)(alphamask[0]);
                            uint bits = (uint)((alphamask[0]) | (alphamask[1] << 8) | (alphamask[2] << 16));
                            for (int j = 0; j < 2; j++)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    // only put pixels out < width or height
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp + 3);
                                        rawData[offset] = (byte)alphas[bits & 0x07];
                                    }
                                    bits >>= 3;
                                }
                            }

                            // Last three bytes
                            //bits = (uint)(alphamask[3]);
                            bits = (uint)((alphamask[3]) | (alphamask[4] << 8) | (alphamask[5] << 16));
                            for (int j = 2; j < 4; j++)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    // only put pixels out < width or height
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp + 3);
                                        rawData[offset] = (byte)alphas[bits & 0x07];
                                    }
                                    bits >>= 3;
                                }
                            }
                        }
                    }
                }
            }

            return rawData;
        }

        private unsafe byte[] DecompressRGB(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];

            uint valMask = (uint)((header.pixelformat.rgbbitcount == 32) ? ~0 : (1 << (int)header.pixelformat.rgbbitcount) - 1);
            uint pixSize = (uint)(((int)header.pixelformat.rgbbitcount + 7) / 8);
            int rShift1 = 0; int rMul = 0; int rShift2 = 0;
            ComputeMaskParams(header.pixelformat.rbitmask, ref rShift1, ref rMul, ref rShift2);
            int gShift1 = 0; int gMul = 0; int gShift2 = 0;
            ComputeMaskParams(header.pixelformat.gbitmask, ref gShift1, ref gMul, ref gShift2);
            int bShift1 = 0; int bMul = 0; int bShift2 = 0;
            ComputeMaskParams(header.pixelformat.bbitmask, ref bShift1, ref bMul, ref bShift2);

            int offset = 0;
            int pixnum = width * height * depth;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                while (pixnum-- > 0)
                {
                    uint px = *((uint*)temp) & valMask;
                    temp += pixSize;
                    uint pxc = px & header.pixelformat.rbitmask;
                    rawData[offset + 0] = (byte)(((pxc >> rShift1) * rMul) >> rShift2);
                    pxc = px & header.pixelformat.gbitmask;
                    rawData[offset + 1] = (byte)(((pxc >> gShift1) * gMul) >> gShift2);
                    pxc = px & header.pixelformat.bbitmask;
                    rawData[offset + 2] = (byte)(((pxc >> bShift1) * bMul) >> bShift2);
                    rawData[offset + 3] = 0xff;
                    offset += 4;
                }
            }
            return rawData;
        }

        private unsafe byte[] DecompressRGBA(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];

            uint valMask = (uint)((header.pixelformat.rgbbitcount == 32) ? ~0 : (1 << (int)header.pixelformat.rgbbitcount) - 1);
            // Funny x86s, make 1 << 32 == 1
            uint pixSize = (header.pixelformat.rgbbitcount + 7) / 8;
            int rShift1 = 0; int rMul = 0; int rShift2 = 0;
            ComputeMaskParams(header.pixelformat.rbitmask, ref rShift1, ref rMul, ref rShift2);
            int gShift1 = 0; int gMul = 0; int gShift2 = 0;
            ComputeMaskParams(header.pixelformat.gbitmask, ref gShift1, ref gMul, ref gShift2);
            int bShift1 = 0; int bMul = 0; int bShift2 = 0;
            ComputeMaskParams(header.pixelformat.bbitmask, ref bShift1, ref bMul, ref bShift2);
            int aShift1 = 0; int aMul = 0; int aShift2 = 0;
            ComputeMaskParams(header.pixelformat.alphabitmask, ref aShift1, ref aMul, ref aShift2);

            int offset = 0;
            int pixnum = width * height * depth;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;

                while (pixnum-- > 0)
                {
                    uint px = *((uint*)temp) & valMask;
                    temp += pixSize;
                    uint pxc = px & header.pixelformat.rbitmask;
                    rawData[offset + 0] = (byte)(((pxc >> rShift1) * rMul) >> rShift2);
                    pxc = px & header.pixelformat.gbitmask;
                    rawData[offset + 1] = (byte)(((pxc >> gShift1) * gMul) >> gShift2);
                    pxc = px & header.pixelformat.bbitmask;
                    rawData[offset + 2] = (byte)(((pxc >> bShift1) * bMul) >> bShift2);
                    pxc = px & header.pixelformat.alphabitmask;
                    rawData[offset + 3] = (byte)(((pxc >> aShift1) * aMul) >> aShift2);
                    offset += 4;
                }
            }
            return rawData;
        }

        private unsafe byte[] Decompress3Dc(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];
            byte[] yColours = new byte[8];
            byte[] xColours = new byte[8];

            int offset = 0;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        for (int x = 0; x < width; x += 4)
                        {
                            byte* temp2 = temp + 8;

                            //Read Y palette
                            int t1 = yColours[0] = temp[0];
                            int t2 = yColours[1] = temp[1];
                            temp += 2;
                            if (t1 > t2)
                                for (int i = 2; i < 8; ++i)
                                    yColours[i] = (byte)(t1 + ((t2 - t1) * (i - 1)) / 7);
                            else
                            {
                                for (int i = 2; i < 6; ++i)
                                    yColours[i] = (byte)(t1 + ((t2 - t1) * (i - 1)) / 5);
                                yColours[6] = 0;
                                yColours[7] = 255;
                            }

                            // Read X palette
                            t1 = xColours[0] = temp2[0];
                            t2 = xColours[1] = temp2[1];
                            temp2 += 2;
                            if (t1 > t2)
                                for (int i = 2; i < 8; ++i)
                                    xColours[i] = (byte)(t1 + ((t2 - t1) * (i - 1)) / 7);
                            else
                            {
                                for (int i = 2; i < 6; ++i)
                                    xColours[i] = (byte)(t1 + ((t2 - t1) * (i - 1)) / 5);
                                xColours[6] = 0;
                                xColours[7] = 255;
                            }

                            //decompress pixel data
                            int currentOffset = offset;
                            for (int k = 0; k < 4; k += 2)
                            {
                                // First three bytes
                                uint bitmask = ((uint)(temp[0]) << 0) | ((uint)(temp[1]) << 8) | ((uint)(temp[2]) << 16);
                                uint bitmask2 = ((uint)(temp2[0]) << 0) | ((uint)(temp2[1]) << 8) | ((uint)(temp2[2]) << 16);
                                for (int j = 0; j < 2; j++)
                                {
                                    // only put pixels out < height
                                    if ((y + k + j) < height)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            // only put pixels out < width
                                            if (((x + i) < width))
                                            {
                                                int t;
                                                byte tx, ty;

                                                t1 = currentOffset + (x + i) * 3;
                                                rawData[t1 + 1] = ty = yColours[bitmask & 0x07];
                                                rawData[t1 + 0] = tx = xColours[bitmask2 & 0x07];

                                                //calculate b (z) component ((r/255)^2 + (g/255)^2 + (b/255)^2 = 1
                                                t = 127 * 128 - (tx - 127) * (tx - 128) - (ty - 127) * (ty - 128);
                                                if (t > 0)
                                                    rawData[t1 + 2] = (byte)(Math.Sqrt(t) + 128);
                                                else
                                                    rawData[t1 + 2] = 0x7F;
                                            }
                                            bitmask >>= 3;
                                            bitmask2 >>= 3;
                                        }
                                        currentOffset += bps;
                                    }
                                }
                                temp += 3;
                                temp2 += 3;
                            }

                            //skip bytes that were read via Temp2
                            temp += 8;
                        }
                        offset += bps * 4;
                    }
                }
            }

            return rawData;
        }

        private unsafe byte[] DecompressAti1n(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];
            byte[] colours = new byte[8];

            uint offset = 0;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        for (int x = 0; x < width; x += 4)
                        {
                            //Read palette
                            int t1 = colours[0] = temp[0];
                            int t2 = colours[1] = temp[1];
                            temp += 2;
                            if (t1 > t2)
                                for (int i = 2; i < 8; ++i)
                                    colours[i] = (byte)(t1 + ((t2 - t1) * (i - 1)) / 7);
                            else
                            {
                                for (int i = 2; i < 6; ++i)
                                    colours[i] = (byte)(t1 + ((t2 - t1) * (i - 1)) / 5);
                                colours[6] = 0;
                                colours[7] = 255;
                            }

                            //decompress pixel data
                            uint currOffset = offset;
                            for (int k = 0; k < 4; k += 2)
                            {
                                // First three bytes
                                uint bitmask = ((uint)(temp[0]) << 0) | ((uint)(temp[1]) << 8) | ((uint)(temp[2]) << 16);
                                for (int j = 0; j < 2; j++)
                                {
                                    // only put pixels out < height
                                    if ((y + k + j) < height)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            // only put pixels out < width
                                            if (((x + i) < width))
                                            {
                                                t1 = (int)(currOffset + (x + i));
                                                rawData[t1] = colours[bitmask & 0x07];
                                            }
                                            bitmask >>= 3;
                                        }
                                        currOffset += (uint)bps;
                                    }
                                }
                                temp += 3;
                            }
                        }
                        offset += (uint)(bps * 4);
                    }
                }
            }
            return rawData;
        }

        private unsafe byte[] DecompressLum(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            //byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];
            byte[] rawData = new byte[(width * height * depth) * 4];

            int lShift1 = 0; int lMul = 0; int lShift2 = 0;
            ComputeMaskParams(header.pixelformat.rbitmask, ref lShift1, ref lMul, ref lShift2);

            int offset = 0;
            int pixnum = width * height * depth;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                while (pixnum-- > 0)
                {
                    byte px = *(temp++);
                    rawData[offset + 0] = (byte)(((px >> lShift1) * lMul) >> lShift2);
                    rawData[offset + 1] = (byte)(((px >> lShift1) * lMul) >> lShift2);
                    rawData[offset + 2] = (byte)(((px >> lShift1) * lMul) >> lShift2);
                    rawData[offset + 3] = (byte)(((px >> lShift1) * lMul) >> lShift2);
                    offset += 4;
                }
            }
            return rawData;
        }

        private unsafe byte[] DecompressRXGB(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];

            Colour565 color_0 = new Colour565();
            Colour565 color_1 = new Colour565();
            Colour8888[] colours = new Colour8888[4];
            byte[] alphas = new byte[8];

            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        for (int x = 0; x < width; x += 4)
                        {
                            if (y >= height || x >= width)
                                break;
                            alphas[0] = temp[0];
                            alphas[1] = temp[1];
                            byte* alphamask = temp + 2;
                            temp += 8;

                            DxtcReadColors(temp, ref color_0, ref color_1);
                            temp += 4;

                            uint bitmask = ((uint*)temp)[1];
                            temp += 4;

                            colours[0].red = (byte)(color_0.red << 3);
                            colours[0].green = (byte)(color_0.green << 2);
                            colours[0].blue = (byte)(color_0.blue << 3);
                            colours[0].alpha = 0xFF;

                            colours[1].red = (byte)(color_1.red << 3);
                            colours[1].green = (byte)(color_1.green << 2);
                            colours[1].blue = (byte)(color_1.blue << 3);
                            colours[1].alpha = 0xFF;

                            // Four-color block: derive the other two colors.    
                            // 00 = color_0, 01 = color_1, 10 = color_2, 11 = color_3
                            // These 2-bit codes correspond to the 2-bit fields 
                            // stored in the 64-bit block.
                            colours[2].blue = (byte)((2 * colours[0].blue + colours[1].blue + 1) / 3);
                            colours[2].green = (byte)((2 * colours[0].green + colours[1].green + 1) / 3);
                            colours[2].red = (byte)((2 * colours[0].red + colours[1].red + 1) / 3);
                            colours[2].alpha = 0xFF;

                            colours[3].blue = (byte)((colours[0].blue + 2 * colours[1].blue + 1) / 3);
                            colours[3].green = (byte)((colours[0].green + 2 * colours[1].green + 1) / 3);
                            colours[3].red = (byte)((colours[0].red + 2 * colours[1].red + 1) / 3);
                            colours[3].alpha = 0xFF;

                            int k = 0;
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < 4; i++, k++)
                                {
                                    int select = (int)((bitmask & (0x03 << k * 2)) >> k * 2);
                                    Colour8888 col = colours[select];

                                    // only put pixels out < width or height
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp);
                                        rawData[offset + 0] = col.red;
                                        rawData[offset + 1] = col.green;
                                        rawData[offset + 2] = col.blue;
                                    }
                                }
                            }

                            // 8-alpha or 6-alpha block?    
                            if (alphas[0] > alphas[1])
                            {
                                // 8-alpha block:  derive the other six alphas.    
                                // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                                alphas[2] = (byte)((6 * alphas[0] + 1 * alphas[1] + 3) / 7);	// bit code 010
                                alphas[3] = (byte)((5 * alphas[0] + 2 * alphas[1] + 3) / 7);	// bit code 011
                                alphas[4] = (byte)((4 * alphas[0] + 3 * alphas[1] + 3) / 7);	// bit code 100
                                alphas[5] = (byte)((3 * alphas[0] + 4 * alphas[1] + 3) / 7);	// bit code 101
                                alphas[6] = (byte)((2 * alphas[0] + 5 * alphas[1] + 3) / 7);	// bit code 110
                                alphas[7] = (byte)((1 * alphas[0] + 6 * alphas[1] + 3) / 7);	// bit code 111
                            }
                            else
                            {
                                // 6-alpha block.
                                // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                                alphas[2] = (byte)((4 * alphas[0] + 1 * alphas[1] + 2) / 5);	// Bit code 010
                                alphas[3] = (byte)((3 * alphas[0] + 2 * alphas[1] + 2) / 5);	// Bit code 011
                                alphas[4] = (byte)((2 * alphas[0] + 3 * alphas[1] + 2) / 5);	// Bit code 100
                                alphas[5] = (byte)((1 * alphas[0] + 4 * alphas[1] + 2) / 5);	// Bit code 101
                                alphas[6] = 0x00;										// Bit code 110
                                alphas[7] = 0xFF;										// Bit code 111
                            }

                            // Note: Have to separate the next two loops,
                            //	it operates on a 6-byte system.
                            // First three bytes
                            uint bits = *((uint*)alphamask);
                            for (int j = 0; j < 2; j++)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    // only put pixels out < width or height
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp + 3);
                                        rawData[offset] = alphas[bits & 0x07];
                                    }
                                    bits >>= 3;
                                }
                            }

                            // Last three bytes
                            bits = *((uint*)&alphamask[3]);
                            for (int j = 2; j < 4; j++)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    // only put pixels out < width or height
                                    if (((x + i) < width) && ((y + j) < height))
                                    {
                                        uint offset = (uint)(z * sizeofplane + (y + j) * bps + (x + i) * bpp + 3);
                                        rawData[offset] = alphas[bits & 0x07];
                                    }
                                    bits >>= 3;
                                }
                            }
                        }
                    }
                }
            }
            return rawData;
        }

        private unsafe byte[] DecompressFloat(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(this.PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * this.PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];
            int size = 0;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                fixed (byte* destPtr = rawData)
                {
                    byte* destData = destPtr;
                    switch (pixelFormat)
                    {
                        case PixelFormat.R32F:  // Red float, green = blue = max
                            size = width * height * depth * 3;
                            for (int i = 0, j = 0; i < size; i += 3, j++)
                            {
                                ((float*)destData)[i] = ((float*)temp)[j];
                                ((float*)destData)[i + 1] = 1.0f;
                                ((float*)destData)[i + 2] = 1.0f;
                            }
                            break;

                        case PixelFormat.A32B32G32R32F:  // Direct copy of float RGBA data
                            Array.Copy(data, rawData, data.Length);
                            break;

                        case PixelFormat.G32R32F:  // Red float, green float, blue = max
                            size = width * height * depth * 3;
                            for (int i = 0, j = 0; i < size; i += 3, j += 2)
                            {
                                ((float*)destData)[i] = ((float*)temp)[j];
                                ((float*)destData)[i + 1] = ((float*)temp)[j + 1];
                                ((float*)destData)[i + 2] = 1.0f;
                            }
                            break;

                        case PixelFormat.R16F:  // Red float, green = blue = max
                            size = width * height * depth * bpp;
                            ConvR16ToFloat32((uint*)destData, (ushort*)temp, (uint)size);
                            break;

                        case PixelFormat.A16B16G16R16F:  // Just convert from half to float.
                            size = width * height * depth * bpp;
                            ConvFloat16ToFloat32((uint*)destData, (ushort*)temp, (uint)size);
                            break;

                        case PixelFormat.G16R16F:  // Convert from half to float, set blue = max.
                            size = width * height * depth * bpp;
                            ConvG16R16ToFloat32((uint*)destData, (ushort*)temp, (uint)size);
                            break;

                        default:
                            break;
                    }
                }
            }

            return rawData;
        }

        #region UNUSED
        private unsafe byte[] DecompressARGB(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            if (Check16BitComponents(header))
                return DecompressARGB16(header, data, pixelFormat);

            int sizeOfData = (int)((header.width * header.pixelformat.rgbbitcount / 8) * header.height * header.depth);
            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];

            if ((pixelFormat == PixelFormat.LUMINANCE) && (header.pixelformat.rgbbitcount == 16) && (header.pixelformat.rbitmask == 0xFFFF))
            {
                Array.Copy(data, rawData, data.Length);
                return rawData;
            }

            uint readI = 0, tempBpp;
            uint redL = 0, redR = 0;
            uint greenL = 0, greenR = 0;
            uint blueL = 0, blueR = 0;
            uint alphaL = 0, alphaR = 0;

            GetBitsFromMask(header.pixelformat.rbitmask, ref redL, ref redR);
            GetBitsFromMask(header.pixelformat.gbitmask, ref greenL, ref greenR);
            GetBitsFromMask(header.pixelformat.bbitmask, ref blueL, ref blueR);
            GetBitsFromMask(header.pixelformat.alphabitmask, ref alphaL, ref alphaR);
            tempBpp = header.pixelformat.rgbbitcount / 8;

            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                for (int i = 0; i < sizeOfData; i += bpp)
                {
                    //@TODO: This is SLOOOW...
                    //but the old version crashed in release build under
                    //winxp (and xp is right to stop this code - I always
                    //wondered that it worked the old way at all)
                    if (sizeOfData - i < 4)
                    {
                        //less than 4 byte to write?
                        if (tempBpp == 3)
                        {
                            //this branch is extra-SLOOOW
                            readI = (uint)(*temp | ((*(temp + 1)) << 8) | ((*(temp + 2)) << 16));
                        }
                        else if (tempBpp == 1)
                            readI = *((byte*)temp);
                        else if (tempBpp == 2)
                            readI = (uint)(temp[0] | (temp[1] << 8));
                    }
                    else
                        readI = (uint)(temp[0] | (temp[1] << 8) | (temp[2] << 16) | (temp[3] << 24));
                    temp += tempBpp;

                    rawData[i] = (byte)((((int)readI & (int)header.pixelformat.rbitmask) >> (int)redR) << (int)redL);

                    if (bpp >= 3)
                    {
                        rawData[i + 1] = (byte)((((int)readI & (int)header.pixelformat.gbitmask) >> (int)greenR) << (int)greenL);
                        rawData[i + 2] = (byte)((((int)readI & header.pixelformat.bbitmask) >> (int)blueR) << (int)blueL);

                        if (bpp == 4)
                        {
                            rawData[i + 3] = (byte)((((int)readI & (int)header.pixelformat.alphabitmask) >> (int)alphaR) << (int)alphaL);
                            if (alphaL >= 7)
                            {
                                rawData[i + 3] = (byte)(rawData[i + 3] != 0 ? 0xFF : 0x00);
                            }
                            else if (alphaL >= 4)
                            {
                                rawData[i + 3] = (byte)(rawData[i + 3] | (rawData[i + 3] >> 4));
                            }
                        }
                    }
                    else if (bpp == 2)
                    {
                        rawData[i + 1] = (byte)((((int)readI & (int)header.pixelformat.alphabitmask) >> (int)alphaR) << (int)alphaL);
                        if (alphaL >= 7)
                        {
                            rawData[i + 1] = (byte)(rawData[i + 1] != 0 ? 0xFF : 0x00);
                        }
                        else if (alphaL >= 4)
                        {
                            rawData[i + 1] = (byte)(rawData[i + 1] | (rawData[i + 3] >> 4));
                        }
                    }
                }
            }
            return rawData;
        }

        private unsafe byte[] DecompressARGB16(DDSStruct header, byte[] data, PixelFormat pixelFormat)
        {
            // allocate bitmap
            int bpp = (int)(PixelFormatToBpp(pixelFormat, header.pixelformat.rgbbitcount));
            int bps = (int)(header.width * bpp * PixelFormatToBpc(pixelFormat));
            int sizeofplane = (int)(bps * header.height);
            int width = (int)header.width;
            int height = (int)header.height;
            int depth = (int)header.depth;

            int sizeOfData = (int)((header.width * header.pixelformat.rgbbitcount / 8) * header.height * header.depth);
            byte[] rawData = new byte[depth * sizeofplane + height * bps + width * bpp];

            uint readI = 0, tempBpp = 0;
            uint redL = 0, redR = 0;
            uint greenL = 0, greenR = 0;
            uint blueL = 0, blueR = 0;
            uint alphaL = 0, alphaR = 0;
            uint redPad = 0, greenPad = 0, bluePad = 0, alphaPad = 0;

            GetBitsFromMask(header.pixelformat.rbitmask, ref redL, ref redR);
            GetBitsFromMask(header.pixelformat.gbitmask, ref greenL, ref greenR);
            GetBitsFromMask(header.pixelformat.bbitmask, ref blueL, ref blueR);
            GetBitsFromMask(header.pixelformat.alphabitmask, ref alphaL, ref alphaR);
            redPad = 16 - CountBitsFromMask(header.pixelformat.rbitmask);
            greenPad = 16 - CountBitsFromMask(header.pixelformat.gbitmask);
            bluePad = 16 - CountBitsFromMask(header.pixelformat.bbitmask);
            alphaPad = 16 - CountBitsFromMask(header.pixelformat.alphabitmask);

            redL = redL + redPad;
            greenL = greenL + greenPad;
            blueL = blueL + bluePad;
            alphaL = alphaL + alphaPad;

            tempBpp = header.pixelformat.rgbbitcount / 8;
            fixed (byte* bytePtr = data)
            {
                byte* temp = bytePtr;
                fixed (byte* destPtr = rawData)
                {
                    byte* destData = destPtr;
                    for (int i = 0; i < sizeOfData / 2; i += bpp)
                    {
                        //@TODO: This is SLOOOW...
                        //but the old version crashed in release build under
                        //winxp (and xp is right to stop this code - I always
                        //wondered that it worked the old way at all)
                        if (sizeOfData - i < 4)
                        {
                            //less than 4 byte to write?
                            if (tempBpp == 3)
                            {
                                //this branch is extra-SLOOOW
                                readI = (uint)(*temp | ((*(temp + 1)) << 8) | ((*(temp + 2)) << 16));
                            }
                            else if (tempBpp == 1)
                                readI = *((byte*)temp);
                            else if (tempBpp == 2)
                                readI = (uint)(temp[0] | (temp[1] << 8));
                        }
                        else
                            readI = (uint)(temp[0] | (temp[1] << 8) | (temp[2] << 16) | (temp[3] << 24));
                        temp += tempBpp;

                        ((ushort*)destData)[i + 2] = (ushort)((((int)readI & (int)header.pixelformat.rbitmask) >> (int)redR) << (int)redL);

                        if (bpp >= 3)
                        {
                            ((ushort*)destData)[i + 1] = (ushort)((((int)readI & (int)header.pixelformat.gbitmask) >> (int)greenR) << (int)greenL);
                            ((ushort*)destData)[i] = (ushort)((((int)readI & (int)header.pixelformat.bbitmask) >> (int)blueR) << (int)blueL);

                            if (bpp == 4)
                            {
                                ((ushort*)destData)[i + 3] = (ushort)((((int)readI & (int)header.pixelformat.alphabitmask) >> (int)alphaR) << (int)alphaL);
                                if (alphaL >= 7)
                                {
                                    ((ushort*)destData)[i + 3] = (ushort)(((ushort*)destData)[i + 3] != 0 ? 0xFF : 0x00);
                                }
                                else if (alphaL >= 4)
                                {
                                    ((ushort*)destData)[i + 3] = (ushort)(((ushort*)destData)[i + 3] | (((ushort*)destData)[i + 3] >> 4));
                                }
                            }
                        }
                        else if (bpp == 2)
                        {
                            ((ushort*)destData)[i + 1] = (ushort)((((int)readI & (int)header.pixelformat.alphabitmask) >> (int)alphaR) << (int)alphaL);
                            if (alphaL >= 7)
                            {
                                ((ushort*)destData)[i + 1] = (ushort)(((ushort*)destData)[i + 1] != 0 ? 0xFF : 0x00);
                            }
                            else if (alphaL >= 4)
                            {
                                ((ushort*)destData)[i + 1] = (ushort)(((ushort*)destData)[i + 1] | (rawData[i + 3] >> 4));
                            }
                        }
                    }
                }
            }
            return rawData;
        }
        #endregion

        #endregion

        #endregion

        #region Public Methods
        public void Dispose()
        {
            if (this.m_bitmap != null)
            {
                this.m_bitmap.Dispose();
                this.m_bitmap = null;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns a System.Imaging.Bitmap containing the DDS image.
        /// </summary>
        public System.Drawing.Bitmap BitmapImage
        {
            get { return this.m_bitmap; }
        }

        public byte[] RawImage
        {
            get { return this.m_rawData; }
        }

        /// <summary>
        /// Returns the DDS image is valid format.
        /// </summary>
        public bool IsValid
        {
            get { return this.m_isValid; }
        }
        #endregion

        #region Operators
        public static implicit operator DdsImage(System.Drawing.Bitmap value)
        {
            return new DdsImage(value);
        }

        public static explicit operator System.Drawing.Bitmap(DdsImage value)
        {
            return value.BitmapImage;
        }
        #endregion

        #region Nested Types

        #region Colour8888
        [StructLayout(LayoutKind.Sequential)]
        private struct Colour8888
        {
            public byte red;
            public byte green;
            public byte blue;
            public byte alpha;
        }
        #endregion

        #region Colour565
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct Colour565
        {
            public ushort blue; //: 5;
            public ushort green; //: 6;
            public ushort red; //: 5;
        }
        #endregion

        #region DDSStruct
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct DDSStruct
        {
            public uint size;		// equals size of struct (which is part of the data file!)
            public uint flags;
            public uint height;
            public uint width;
            public uint sizeorpitch;
            public uint depth;
            public uint mipmapcount;
            public uint alphabitdepth;
            //[MarshalAs(UnmanagedType.U4, SizeConst = 11)]
            public uint[] reserved;//[11];

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct pixelformatstruct
            {
                public uint size;	// equals size of struct (which is part of the data file!)
                public uint flags;
                public uint fourcc;
                public uint rgbbitcount;
                public uint rbitmask;
                public uint gbitmask;
                public uint bbitmask;
                public uint alphabitmask;
            }
            public pixelformatstruct pixelformat;

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct ddscapsstruct
            {
                public uint caps1;
                public uint caps2;
                public uint caps3;
                public uint caps4;
            }
            public ddscapsstruct ddscaps;
            public uint texturestage;

            public uint dxt10_dxgiFormat;
            public uint dxt10_resourceDimension;
            public uint dxt10_miscFlag;
            public uint dxt10_arraySize;
            public uint dxt10_miscFlags2;

            //#ifndef __i386__
            //void to_little_endian()
            //{
            //	size_t size = sizeof(DDSStruct);
            //	assert(size % 4 == 0);
            //	size /= 4;
            //	for (size_t i=0; i<size; i++)
            //	{
            //		((int32_t*) this)[i] = little_endian(((int32_t*) this)[i]);
            //	}
            //}
            //#endif
        }
        #endregion

        #region DDSStruct Flags
        private const int DDSD_CAPS = 0x00000001;
        private const int DDSD_HEIGHT = 0x00000002;
        private const int DDSD_WIDTH = 0x00000004;
        private const int DDSD_PITCH = 0x00000008;
        private const int DDSD_PIXELFORMAT = 0x00001000;
        private const int DDSD_MIPMAPCOUNT = 0x00020000;
        private const int DDSD_LINEARSIZE = 0x00080000;
        private const int DDSD_DEPTH = 0x00800000;
        #endregion

        #region pixelformat values
        private const int DDPF_ALPHAPIXELS = 0x00000001;
        private const int DDPF_FOURCC = 0x00000004;
        private const int DDPF_RGB = 0x00000040;
        private const int DDPF_LUMINANCE = 0x00020000;
        #endregion

        #region ddscaps
        // caps1
        private const int DDSCAPS_COMPLEX = 0x00000008;
        private const int DDSCAPS_TEXTURE = 0x00001000;
        private const int DDSCAPS_MIPMAP = 0x00400000;
        // caps2
        private const int DDSCAPS2_CUBEMAP = 0x00000200;
        private const int DDSCAPS2_CUBEMAP_POSITIVEX = 0x00000400;
        private const int DDSCAPS2_CUBEMAP_NEGATIVEX = 0x00000800;
        private const int DDSCAPS2_CUBEMAP_POSITIVEY = 0x00001000;
        private const int DDSCAPS2_CUBEMAP_NEGATIVEY = 0x00002000;
        private const int DDSCAPS2_CUBEMAP_POSITIVEZ = 0x00004000;
        private const int DDSCAPS2_CUBEMAP_NEGATIVEZ = 0x00008000;
        private const int DDSCAPS2_VOLUME = 0x00200000;
        #endregion

        #region fourccs
        private const uint FOURCC_DXT1 = 0x31545844;
        private const uint FOURCC_DXT2 = 0x32545844;
        private const uint FOURCC_DXT3 = 0x33545844;
        private const uint FOURCC_DXT4 = 0x34545844;
        private const uint FOURCC_DXT5 = 0x35545844;
        private const uint FOURCC_DXT10 = 808540228;
        private const uint FOURCC_BC4U = 0x55344342;
        private const uint FOURCC_BC4S = 0x53344342;
        private const uint FOURCC_BC5S = 0x53354342;
        private const uint FOURCC_RGBG = 0x47424752;
        private const uint FOURCC_GRGB = 0x42475247;
        private const uint FOURCC_UYVY = 0x59565955;
        private const uint FOURCC_YUY2 = 0x32595559;
        private const uint FOURCC_ATI1 = 0x31495441;
        private const uint FOURCC_ATI2 = 0x32495441;
        private const uint FOURCC_RXGB = 0x42475852;
        private const uint FOURCC_DOLLARNULL = 0x24;
        private const uint FOURCC_oNULL = 0x6f;
        private const uint FOURCC_pNULL = 0x70;
        private const uint FOURCC_qNULL = 0x71;
        private const uint FOURCC_rNULL = 0x72;
        private const uint FOURCC_sNULL = 0x73;
        private const uint FOURCC_tNULL = 0x74;
        #endregion


        #region DXGI Formats
        private enum DxgiFormat : uint
        {
            DXGI_FORMAT_UNKNOWN = 0,
            DXGI_FORMAT_R32G32B32A32_TYPELESS = 1,
            DXGI_FORMAT_R32G32B32A32_FLOAT = 2,
            DXGI_FORMAT_R32G32B32A32_UINT = 3,
            DXGI_FORMAT_R32G32B32A32_SINT = 4,
            DXGI_FORMAT_R32G32B32_TYPELESS = 5,
            DXGI_FORMAT_R32G32B32_FLOAT = 6,
            DXGI_FORMAT_R32G32B32_UINT = 7,
            DXGI_FORMAT_R32G32B32_SINT = 8,
            DXGI_FORMAT_R16G16B16A16_TYPELESS = 9,
            DXGI_FORMAT_R16G16B16A16_FLOAT = 10,
            DXGI_FORMAT_R16G16B16A16_UNORM = 11,
            DXGI_FORMAT_R16G16B16A16_UINT = 12,
            DXGI_FORMAT_R16G16B16A16_SNORM = 13,
            DXGI_FORMAT_R16G16B16A16_SINT = 14,
            DXGI_FORMAT_R32G32_TYPELESS = 15,
            DXGI_FORMAT_R32G32_FLOAT = 16,
            DXGI_FORMAT_R32G32_UINT = 17,
            DXGI_FORMAT_R32G32_SINT = 18,
            DXGI_FORMAT_R32G8X24_TYPELESS = 19,
            DXGI_FORMAT_D32_FLOAT_S8X24_UINT = 20,
            DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS = 21,
            DXGI_FORMAT_X32_TYPELESS_G8X24_UINT = 22,
            DXGI_FORMAT_R10G10B10A2_TYPELESS = 23,
            DXGI_FORMAT_R10G10B10A2_UNORM = 24,
            DXGI_FORMAT_R10G10B10A2_UINT = 25,
            DXGI_FORMAT_R11G11B10_FLOAT = 26,
            DXGI_FORMAT_R8G8B8A8_TYPELESS = 27,
            DXGI_FORMAT_R8G8B8A8_UNORM = 28,
            DXGI_FORMAT_R8G8B8A8_UNORM_SRGB = 29,
            DXGI_FORMAT_R8G8B8A8_UINT = 30,
            DXGI_FORMAT_R8G8B8A8_SNORM = 31,
            DXGI_FORMAT_R8G8B8A8_SINT = 32,
            DXGI_FORMAT_R16G16_TYPELESS = 33,
            DXGI_FORMAT_R16G16_FLOAT = 34,
            DXGI_FORMAT_R16G16_UNORM = 35,
            DXGI_FORMAT_R16G16_UINT = 36,
            DXGI_FORMAT_R16G16_SNORM = 37,
            DXGI_FORMAT_R16G16_SINT = 38,
            DXGI_FORMAT_R32_TYPELESS = 39,
            DXGI_FORMAT_D32_FLOAT = 40,
            DXGI_FORMAT_R32_FLOAT = 41,
            DXGI_FORMAT_R32_UINT = 42,
            DXGI_FORMAT_R32_SINT = 43,
            DXGI_FORMAT_R24G8_TYPELESS = 44,
            DXGI_FORMAT_D24_UNORM_S8_UINT = 45,
            DXGI_FORMAT_R24_UNORM_X8_TYPELESS = 46,
            DXGI_FORMAT_X24_TYPELESS_G8_UINT = 47,
            DXGI_FORMAT_R8G8_TYPELESS = 48,
            DXGI_FORMAT_R8G8_UNORM = 49,
            DXGI_FORMAT_R8G8_UINT = 50,
            DXGI_FORMAT_R8G8_SNORM = 51,
            DXGI_FORMAT_R8G8_SINT = 52,
            DXGI_FORMAT_R16_TYPELESS = 53,
            DXGI_FORMAT_R16_FLOAT = 54,
            DXGI_FORMAT_D16_UNORM = 55,
            DXGI_FORMAT_R16_UNORM = 56,
            DXGI_FORMAT_R16_UINT = 57,
            DXGI_FORMAT_R16_SNORM = 58,
            DXGI_FORMAT_R16_SINT = 59,
            DXGI_FORMAT_R8_TYPELESS = 60,
            DXGI_FORMAT_R8_UNORM = 61,
            DXGI_FORMAT_R8_UINT = 62,
            DXGI_FORMAT_R8_SNORM = 63,
            DXGI_FORMAT_R8_SINT = 64,
            DXGI_FORMAT_A8_UNORM = 65,
            DXGI_FORMAT_R1_UNORM = 66,
            DXGI_FORMAT_R9G9B9E5_SHAREDEXP = 67,
            DXGI_FORMAT_R8G8_B8G8_UNORM = 68,
            DXGI_FORMAT_G8R8_G8B8_UNORM = 69,
            DXGI_FORMAT_BC1_TYPELESS = 70,
            DXGI_FORMAT_BC1_UNORM = 71,
            DXGI_FORMAT_BC1_UNORM_SRGB = 72,
            DXGI_FORMAT_BC2_TYPELESS = 73,
            DXGI_FORMAT_BC2_UNORM = 74,
            DXGI_FORMAT_BC2_UNORM_SRGB = 75,
            DXGI_FORMAT_BC3_TYPELESS = 76,
            DXGI_FORMAT_BC3_UNORM = 77,
            DXGI_FORMAT_BC3_UNORM_SRGB = 78,
            DXGI_FORMAT_BC4_TYPELESS = 79,
            DXGI_FORMAT_BC4_UNORM = 80,
            DXGI_FORMAT_BC4_SNORM = 81,
            DXGI_FORMAT_BC5_TYPELESS = 82,
            DXGI_FORMAT_BC5_UNORM = 83,
            DXGI_FORMAT_BC5_SNORM = 84,
            DXGI_FORMAT_B5G6R5_UNORM = 85,
            DXGI_FORMAT_B5G5R5A1_UNORM = 86,
            DXGI_FORMAT_B8G8R8A8_UNORM = 87,
            DXGI_FORMAT_B8G8R8X8_UNORM = 88,
            DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM = 89,
            DXGI_FORMAT_B8G8R8A8_TYPELESS = 90,
            DXGI_FORMAT_B8G8R8A8_UNORM_SRGB = 91,
            DXGI_FORMAT_B8G8R8X8_TYPELESS = 92,
            DXGI_FORMAT_B8G8R8X8_UNORM_SRGB = 93,
            DXGI_FORMAT_BC6H_TYPELESS = 94,
            DXGI_FORMAT_BC6H_UF16 = 95,
            DXGI_FORMAT_BC6H_SF16 = 96,
            DXGI_FORMAT_BC7_TYPELESS = 97,
            DXGI_FORMAT_BC7_UNORM = 98,
            DXGI_FORMAT_BC7_UNORM_SRGB = 99,
            DXGI_FORMAT_AYUV = 100,
            DXGI_FORMAT_Y410 = 101,
            DXGI_FORMAT_Y416 = 102,
            DXGI_FORMAT_NV12 = 103,
            DXGI_FORMAT_P010 = 104,
            DXGI_FORMAT_P016 = 105,
            DXGI_FORMAT_420_OPAQUE = 106,
            DXGI_FORMAT_YUY2 = 107,
            DXGI_FORMAT_Y210 = 108,
            DXGI_FORMAT_Y216 = 109,
            DXGI_FORMAT_NV11 = 110,
            DXGI_FORMAT_AI44 = 111,
            DXGI_FORMAT_IA44 = 112,
            DXGI_FORMAT_P8 = 113,
            DXGI_FORMAT_A8P8 = 114,
            DXGI_FORMAT_B4G4R4A4_UNORM = 115,
            DXGI_FORMAT_P208 = 130,
            DXGI_FORMAT_V208 = 131,
            DXGI_FORMAT_V408 = 132
        }
        #endregion

        #region PixelFormat
        /// <summary>
        /// Various pixel formats/compressors used by the DDS image.
        /// </summary>
        private enum PixelFormat
        {
            /// <summary>
            /// 32-bit image; with 8-bit red, green, blue and alpha.
            /// </summary>
            RGBA,
            /// <summary>
            /// 24-bit image with 8-bit red, green, blue.
            /// </summary>
            RGB,
            /// <summary>
            /// 16-bit DXT-1 compression, 1-bit alpha.
            /// </summary>
            DXT1,
            /// <summary>
            /// DXT-2 Compression
            /// </summary>
            DXT2,
            /// <summary>
            /// DXT-3 Compression
            /// </summary>
            DXT3,
            /// <summary>
            /// DXT-4 Compression
            /// </summary>
            DXT4,
            /// <summary>
            /// DXT-5 Compression
            /// </summary>
            DXT5,
            DXT10,
            /// <summary>
            /// 3DC Compression
            /// </summary>
            THREEDC,
            /// <summary>
            /// ATI1n Compression
            /// </summary>
            ATI1N,
            LUMINANCE,
            LUMINANCE_ALPHA,
            RXGB,
            A16B16G16R16,
            R16F,
            G16R16F,
            A16B16G16R16F,
            R32F,
            G32R32F,
            A32B32G32R32F,
            BC4U,
            BC4S,
            /// <summary>
            /// Unknown pixel format.
            /// </summary>
            UNKNOWN
        }
        #endregion

        #endregion
    }
    #endregion

    #region Exceptions Class
    /// <summary>
    /// Thrown when an invalid file header has been encountered.
    /// </summary>
    public class InvalidFileHeaderException : Exception
    {
    }

    /// <summary>
    /// Thrown when the data does not contain a DDS image.
    /// </summary>
    public class NotADDSImageException : Exception
    {

    }

    /// <summary>
    /// Thrown when there is an unknown compressor used in the DDS file.
    /// </summary>
    public class UnknownFileFormatException : Exception
    {
    }



    [Serializable]
    public class DdsImageException : Exception
    {
        //------// Properties \\--------------------------------------------\\

        //------\\ Properties //--------------------------------------------//



        //------// Constructors \\------------------------------------------\\
        public DdsImageException()
        { }


        public DdsImageException(string message)
            : base(message)
        { }


        public DdsImageException(string message, Exception inner)
            : base(message, inner)
        { }


        protected DdsImageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        //------\\ Constructors //------------------------------------------//



        //------// Methods \\-----------------------------------------------\\

        //------\\ Methods //-----------------------------------------------//
    }

    #endregion
}
