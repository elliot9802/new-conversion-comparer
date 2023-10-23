using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Winnovative;

namespace Services
{
    public class WinnovativeService : IUtilityService
    {
        private readonly IFileService _fileService;

        public WinnovativeService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            var pdfConverter = GetPdfConverter();
            byte[] downloadBytes;

            try
            {
                downloadBytes = pdfConverter.GetPdfBytesFromUrl(urlContent);
            }
            catch (Exception e)
            {
                Log(128, e.Message);
                throw; 
            }

            _fileService.WriteAllBytes(outputPath, downloadBytes);
            // If you want to retrieve the name of the file as in GetOrderPdfAttachmentAsync
            string fileName = Path.ChangeExtension(Path.GetFileName(urlContent), ".pdf");
            Console.WriteLine(fileName);
        }

        private void Log(int code, string message)
        {
        }

        private PdfConverter GetPdfConverter()
        {
            var pdfConverter = new PdfConverter
            {
                JavaScriptEnabled = false
            };

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Best;
            pdfConverter.PdfDocumentOptions.LeftMargin = 0;
            pdfConverter.PdfDocumentOptions.RightMargin = 0;
            pdfConverter.PdfDocumentOptions.TopMargin = 0;
            pdfConverter.PdfDocumentOptions.BottomMargin = 0;
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
            pdfConverter.PdfDocumentOptions.PdfPageSize.Width = 1024;

            return pdfConverter;
        }
    }
}
