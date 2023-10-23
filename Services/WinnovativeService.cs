using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}
