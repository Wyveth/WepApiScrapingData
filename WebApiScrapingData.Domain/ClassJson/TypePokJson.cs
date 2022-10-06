using System.Text.Json.Serialization;

namespace WebApiScrapingData.Domain.ClassJson
{
    [Serializable]
    public class TypePokJson
    {
        public string? Name_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? Name_JP { get; set; }
        public string? UrlMiniGo { get; set; }
        [JsonIgnore]
        public byte[]? DataMiniGo { get; set; }
        public string? UrlFondGo { get; set; }
        [JsonIgnore]
        public byte[]? DataFondGo { get; set; }
        [JsonIgnore]
        public string? UrlMiniHome { get; set; }
        [JsonIgnore]
        public byte[]? DataMiniHome { get; set; }
        public string? UrlIconHome { get; set; }
        [JsonIgnore]
        public byte[]? DataIconHome { get; set; }
        public string? UrlAutoHome { get; set; }
        public byte[]? DataAutoHome { get; set; }
        public string? ImgColor { get; set; }
        public string? InfoColor { get; set; }
        public string? TypeColor { get; set; }
    }
}
