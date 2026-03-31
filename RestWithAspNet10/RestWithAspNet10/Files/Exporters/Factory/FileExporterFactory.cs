using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Files.Exporters.Contract;
using RestWithAspNet10.Files.Exporters.Impl;

namespace RestWithAspNet10.Files.Exporters.Factory
{
    public class FileExporterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FileExporterFactory> _logger;

        public FileExporterFactory(IServiceProvider serviceProvider, ILogger<FileExporterFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IFileExporter GetExporter(string acceptHeader)
        {
            if (string.Equals(acceptHeader, MediaTypes.ApplicationXlsx, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Creating XlsxExporter for media type: {AcceptHeader}", acceptHeader);
                return _serviceProvider.GetService<XlsxExporter>(); 
            }
            else if (string.Equals(acceptHeader, MediaTypes.ApplicationCsv, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Creating CsvExporter for media type: {AcceptHeader}", acceptHeader);
                return _serviceProvider.GetService<CsvExporter>(); 
            }
            else
            {
                _logger.LogError("Unsupported media type: {AcceptHeader}", acceptHeader);
                throw new NotSupportedException($"The media type of '{acceptHeader}' is not supported.");
            }
        }
    }
}
