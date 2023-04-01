using System;
using Docnet.Core;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Docnet.Core.Converters;
using Docnet.Core.Models;

namespace BlazorFile.Api.Services {
    public class DocnetService {

        private readonly ExampleFixture _fixture = new ExampleFixture();

        public static byte[] PdfPage2Jpg(byte[] bytes, int pageNumber = 1) {
            using var docReader = new ExampleFixture().DocNet.GetDocReader(
                            bytes,
                            new PageDimensions(1080, 1920));

            pageNumber = pageNumber > docReader.GetPageCount() ? docReader.GetPageCount() : pageNumber;
            using var pageReader = docReader.GetPageReader(pageNumber - 1);

            var rawBytes = pageReader.GetImage(new NaiveTransparencyRemover(255, 255, 255)); // White background
            var _width = pageReader.GetPageWidth();
            var _height = pageReader.GetPageHeight();
            var characters = pageReader.GetCharacters();

            using var bmp = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);

            AddBytes(bmp, rawBytes);
            using var stream = new MemoryStream();
            bmp.Save(stream, ImageFormat.Jpeg);

            return stream.ToArray();
        }

        public static byte[] PdfPage2JpgFixedWidth(byte[] bytes, int fixedWidth, int pageNumber = 1) {
            byte[] jpeg = PdfPage2Jpg(bytes, pageNumber);
            using var ms = new MemoryStream(jpeg);
            Image img = Image.FromStream(ms);
            int _width = img.Width;
            int _height = img.Height;
            var scaledHeight = fixedWidth * _height / _width;
            Image scaledImage = new Bitmap(img, new Size(fixedWidth, scaledHeight));

            return ImageToByteArray(scaledImage);
        }

        public static byte[] PdfPage2JpgFixedHeight(byte[] bytes, int fixedHeight, int pageNumber = 1) {
            byte[] jpeg = PdfPage2Jpg(bytes, pageNumber);
            using var ms = new MemoryStream(jpeg);
            Image img = Image.FromStream(ms);
            int _width = img.Width;
            int _height = img.Height;
            var scaledWidth = _width * fixedHeight / _height;
            Image scaledImage = new Bitmap(img, new Size(scaledWidth, fixedHeight));

            return ImageToByteArray(scaledImage);
        }


        private static void AddBytes(Bitmap bmp, byte[] rawBytes) {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            var bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);
            var pNative = bmpData.Scan0;

            Marshal.Copy(rawBytes, 0, pNative, rawBytes.Length);
            bmp.UnlockBits(bmpData);
        }

        public static byte[] ImageToByteArray(System.Drawing.Image image) {
            using (MemoryStream ms = new MemoryStream()) {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }

    public class ExampleFixture : IDisposable {
        public IDocLib DocNet { get; }

        public ExampleFixture() {
            DocNet = DocLib.Instance;
        }

        public void Dispose() {
            DocNet.Dispose();
        }
    }
}
