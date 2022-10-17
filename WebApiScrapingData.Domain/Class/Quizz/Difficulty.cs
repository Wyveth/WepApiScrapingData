using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    public class Difficulty : Identity
    {
        public string? Libelle_FR { get; set; }
        public string? Libelle_EN { get; set; }
        public string? Libelle_ES { get; set; }
        public string? Libelle_IT { get; set; }
        public string? Libelle_DE { get; set; }
        public string? Libelle_RU { get; set; }
        public string? Libelle_CO { get; set; }
        public string? Libelle_CN { get; set; }
        public string? Libelle_JP { get; set; }
    }
}
