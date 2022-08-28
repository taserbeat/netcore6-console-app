using N6CA.Services.Models;

namespace N6CA.Services
{
    /// <summary>
    /// 郵便情報サービスのインターフェース
    /// </summary>
    public interface IZipInformationService
    {
        /// <summary>
        /// 郵便情報を取得する
        /// </summary>
        /// <param name="zipCode">検索する郵便番号</param>
        /// <returns></returns>
        Task<ZipInformation> GetPostInformation(string zipCode);
    }

    public class ZipInformationServiceMock : IZipInformationService
    {
        public ZipInformationServiceMock()
        {

        }

        public Task<ZipInformation> GetPostInformation(string zipCode)
        {
            var zipInformation = new ZipInformation()
            {
                ZipCode = zipCode,
                Address = "北海道札幌市",
            };

            return Task.FromResult(zipInformation);
        }
    }
}
