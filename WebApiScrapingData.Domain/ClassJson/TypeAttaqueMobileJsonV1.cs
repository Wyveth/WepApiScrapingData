namespace WebApiScrapingData.Domain.ClassJson
{
    [Serializable]
    public class TypeAttaqueMobileJsonV1
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        //Url de l'Image
        public string? UrlImg { get; set; }

        //Url de l'Image Interne
        public string? PathImg { get; set; }
    }
}
