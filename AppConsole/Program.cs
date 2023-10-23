using Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AppConsole
{
    class Program
    {
        private const string BaseOutputPath = "HTML-to-PDF";
        private const string FileExtension = ".pdf";

        private static IUtilityService _pdfService;

        static void Main(string[] args)
        {
            string urlContent = @"https://messagequeue.actorsmartbook.se/Templates/ticket.aspx?orderid=3545624&uid=411ffdec-dcbc-491f-a629-8939d26dd031";

            while (true)
            {
                Console.WriteLine("Select PDF Conversion Service then press enter and wait: ");
                Console.WriteLine("1: Syncfusion");
                Console.WriteLine("2: EvoPdf");
                Console.WriteLine("3: ExpertPdf");
                Console.WriteLine("4: Winnovative");
                Console.WriteLine("5: Exit");

                int choice;
                bool validChoice = int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 5;
                string serviceIdentifier = string.Empty;

                if (!validChoice)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }

                if (choice == 5)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        _pdfService = new SyncfusionService(new FileService());
                        serviceIdentifier = "Syncfusion";
                        break;
                    case 2:
                        _pdfService = new EvoPdfService(new FileService());
                        serviceIdentifier = "EvoPdf";
                        break;
                    case 3:
                        _pdfService = new ExpertPdfService(new FileService());
                        serviceIdentifier = "ExpertPdf";
                        break;
                    case 4:
                        _pdfService = new WinnovativeService(new FileService());
                        serviceIdentifier = "Winnovative";
                        break;
                }
                try
                {
                    ConvertUrlToPdf(urlContent, serviceIdentifier);
                    Console.WriteLine("PDF Conversion done.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during PDF conversion: {ex.Message}");
                }
            }
        }

        private static void ConvertUrlToPdf(string urlContent, string serviceIdentifier)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string outputPath = GenerateOutputFileName($"UrlPDF-{serviceIdentifier}");
            _pdfService.ConvertUrlToPdf(urlContent, outputPath);
            stopwatch.Stop();
            Console.WriteLine($"Time taken for {serviceIdentifier}: {stopwatch.ElapsedMilliseconds} milliseconds");
        }
        private static string GenerateOutputFileName(string identifier)
        {
            return $"{identifier}-{BaseOutputPath}{FileExtension}";
        }
    }
}