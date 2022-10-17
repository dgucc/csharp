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

        public static byte[] Pdf2Jpg(byte[] bytes, float w, float h, int pageNumber=1) {
            using var docReader = new ExampleFixture().DocNet.GetDocReader(
                            bytes,
                            new PageDimensions(1080, 1920));

            using var pageReader = docReader.GetPageReader(pageNumber-1);

            var rawBytes = pageReader.GetImage();
            var width = pageReader.GetPageWidth();
            var height = pageReader.GetPageHeight();

            var characters = pageReader.GetCharacters();

            using var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(w, h);
            AddBytes(bmp, rawBytes);

            using var stream = new MemoryStream();

            bmp.Save(stream, ImageFormat.Jpeg);

            return stream.ToArray();

        }

        private static void AddBytes(Bitmap bmp, byte[] rawBytes) {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            var bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);
            var pNative = bmpData.Scan0;

            Marshal.Copy(rawBytes, 0, pNative, rawBytes.Length);
            bmp.UnlockBits(bmpData);
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
