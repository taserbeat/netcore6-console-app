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
}
