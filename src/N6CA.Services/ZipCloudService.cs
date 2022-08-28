using Microsoft.Extensions.Logging;
using N6CA.Services.Models;

namespace N6CA.Services
{
    public class ZipCloudService : IZipInformationService
    {
        private readonly ILogger<ZipCloudService> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        public ZipCloudService(ILogger<ZipCloudService> logger)
        {
            _logger = logger;
        }

        public Task<ZipInformation> GetPostInformation(string zipCode)
        {
            throw new NotImplementedException();
        }
    }
}
