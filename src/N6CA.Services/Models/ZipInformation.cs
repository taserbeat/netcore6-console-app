namespace N6CA.Services.Models
{
    /// <summary>
    /// 郵便番号の情報
    /// </summary>
    public class ZipInformation
    {
        /// <summary>
        /// 郵便番号
        /// </summary>
        /// <value></value>
        public string? ZipCode { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        /// <value></value>
        public string? Address { get; set; }
    }
}
