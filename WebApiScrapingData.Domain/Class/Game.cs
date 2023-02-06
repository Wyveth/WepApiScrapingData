using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Game : Identity
    {
        [DataMember]
        public string? Name_FR { get; set; }
        [DataMember]
        public string? Name_EN { get; set; }
        [DataMember]
        public string? Name_ES { get; set; }
        [DataMember]
        public string? Name_IT { get; set; }
        [DataMember]
        public string? Name_DE { get; set; }
        [DataMember]
        public string? Name_RU { get; set; }
        [DataMember]
        public string? Name_CO { get; set; }
        [DataMember]
        public string? Name_CN { get; set; }
        [DataMember]
        public string? Name_JP { get; set; }
    }
}
