using System.Web;
using Microsoft.Extensions.Logging;
using N6CA.Services.Models;
using Newtonsoft.Json;

namespace N6CA.Services
{
    public class ZipCloudService : IZipInformationService
    {
        private readonly ILogger<ZipCloudService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _clientPolicyName = "ZipCloudServicePolicy";
        private const string _zipCloudApiUrl = "https://zipcloud.ibsnet.co.jp/api/search";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        public ZipCloudService(ILogger<ZipCloudService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ZipInformation> GetPostInformation(string zipCode)
        {
            // Todo: ZipCloudと通信ができないので調査が必要
            var client = _httpClientFactory.CreateClient(_clientPolicyName);

            var queryString = HttpUtility.ParseQueryString("");
            queryString.Add("zipcode", zipCode);

            var url = new UriBuilder(_zipCloudApiUrl)
            {
                Query = queryString.ToString(),
            };

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = url.Uri,
            };

            try
            {
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"http response error: code={response.StatusCode}");
                    throw new InvalidOperationException();
                }

                var buffer = await response.Content.ReadAsStringAsync();
                var zipCloudResponse = JsonConvert.DeserializeObject<ZipCloudResponse>(buffer);

                if (
                    zipCloudResponse is null ||
                    zipCloudResponse.Results is null
                )
                {
                    _logger.LogError("郵便番号の結果が取得できませんでした");
                    throw new InvalidOperationException();
                }

                var zipCloudResult = zipCloudResponse.Results.First();
                if (zipCloudResult is null)
                {
                    _logger.LogError("郵便番号の結果が取得できませんでした");
                    throw new InvalidOperationException();
                }

                var zipInformation = new ZipInformation()
                {
                    ZipCode = zipCloudResult.ZipCode,
                    Address = $"{zipCloudResult.Address1}{zipCloudResult.Address2}{zipCloudResult.Address3}",
                };

                return zipInformation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"http request error. url: {url.Uri}", ex);
                throw ex;
            }
        }
    }

    /// <summary>
    /// ZipCloudのレスポンス
    /// http://zipcloud.ibsnet.co.jp/doc/api
    /// </summary>
    public class ZipCloudResponse
    {
        /// <summary>
        /// エラーの内容 (エラー発生時のみ)
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }

        /// <summary>
        /// HTTPステータス
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<ZipCloudResult>? Results { get; set; }

        public class ZipCloudResult
        {
            /// <summary>
            /// 都道府県名
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "address1")]
            public string? Address1 { get; set; }

            /// <summary>
            /// 市区町村名
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "address2")]
            public string? Address2 { get; set; }

            /// <summary>
            /// 町域名
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "address3")]
            public string? Address3 { get; set; }

            /// <summary>
            /// 都道府県名カナ
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "kana1")]
            public string? Kana1 { get; set; }

            /// <summary>
            /// 市区町村名カナ
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "kana2")]
            public string? Kana2 { get; set; }

            /// <summary>
            /// 町域名カナ
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "kana3")]
            public string? Kana3 { get; set; }

            /// <summary>
            /// 都道府県コード JIS X 0401 に定められた2桁の都道府県コード
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "prefcode")]
            public string? PrefCode { get; set; }

            /// <summary>
            /// 7桁の郵便番号 (ハイフンなし)
            /// </summary>
            /// <value></value>
            [JsonProperty(PropertyName = "zipcode")]
            public string? ZipCode { get; set; }
        }
    }
}
