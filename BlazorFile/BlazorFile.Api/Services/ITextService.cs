using iText.Kernel.Pdf;
using iTextSharp.text.pdf;
using PdfReader = iTextSharp.text.pdf.PdfReader;

namespace BlazorFile.Api.Services {
    public class ITextService {
        public static byte[] UpdatePDFMetaData(byte[] pdf, string author, string title, string abstr) {
            PdfReader reader = new PdfReader(pdf);
            using MemoryStream ms = new MemoryStream();
            using (PdfStamper stamper = new(reader, ms)) {

                Dictionary<String, String> info = reader.Info;

                // Before...
                Console.WriteLine("Before : ...");
                Console.WriteLine(info["Title"]);
                Console.WriteLine(info["Author"]);

                reader.Info.Clear();

                // Modify Metadata
                info["Title"] = title;
                info["Author"] = author;
                info["Abstract"] = abstr;
                stamper.MoreInfo = info;
            }


            // For test purposes            
            //File.WriteAllBytes(Environment.CurrentDirectory + @"/wwwroot/Pdfs/result.pdf", ms.ToArray());

            return ms.ToArray();
        }
    }
}
