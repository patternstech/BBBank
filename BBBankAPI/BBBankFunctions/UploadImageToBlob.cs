using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BBBankFunctions
{
    public class UploadImageToBlob
    {
        private readonly ILogger<UploadImageToBlob> _logger;

        public UploadImageToBlob(ILogger<UploadImageToBlob> logger)
        {
            _logger = logger;
        }

        [Function("UploadImageToBlob")]
        [BlobOutput("profile-pics/{fileName}")]
        public async Task<byte[]> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
             string fileName)
        {
            _logger.LogInformation("Processing file upload...");

            var file = req.Form.Files[0];

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            _logger.LogInformation($"{fileName} uploaded successfully: ");

            return fileBytes;

        }
    }
}
