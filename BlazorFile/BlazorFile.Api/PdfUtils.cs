﻿using System;
using Docnet.Core;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Docnet.Core.Converters;
using Docnet.Core.Models;

namespace BlazorFile.Api {
    public class PdfUtils {

        private readonly ExampleFixture _fixture = new ExampleFixture();


        public static string Pdf2Jpg(byte[] bytes, float w, float h) {
            using var docReader = new ExampleFixture().DocNet.GetDocReader(
                            bytes,
                            new PageDimensions(1080, 1920));

            using var pageReader = docReader.GetPageReader(0);

            var rawBytes = pageReader.GetImage();
            var width = pageReader.GetPageWidth();
            var height = pageReader.GetPageHeight();

            var characters = pageReader.GetCharacters();

            using var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(w, h);
            AddBytes(bmp, rawBytes);

            using var stream = new MemoryStream();

            bmp.Save(stream, ImageFormat.Jpeg);

            var resultName = $"{Guid.NewGuid()}.jpg";

            File.WriteAllBytes(Environment.CurrentDirectory + @"/wwwroot/Images/" +  resultName, stream.ToArray());

            return @"Images/" + resultName;

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
