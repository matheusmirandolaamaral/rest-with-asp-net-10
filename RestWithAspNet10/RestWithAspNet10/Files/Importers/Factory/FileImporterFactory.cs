using RestWithAspNet10.Files.Importers.Contract;
using RestWithAspNet10.Files.Importers.Impl;

namespace RestWithAspNet10.Files.Importers.Factory
{
    public class FileImporterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FileImporterFactory> _logger;

        public FileImporterFactory(IServiceProvider serviceProvider, ILogger<FileImporterFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IFileImporter GetImporter(string fileName)
        {
            if(fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Creating CSV file importer for file: {FileName}", fileName);
                return _serviceProvider.GetRequiredService<CsvImporter>();
            }
            else if(fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Creating Excel file importer for file: {FileName}", fileName);
                return _serviceProvider.GetRequiredService<XlsxImporter>();
            }
            else
            {
                _logger.LogError("Unsupported file format: {FileName}", fileName);
                throw new NotSupportedException($"Unsupported file format: {fileName}");
            }
        }
    }
}
