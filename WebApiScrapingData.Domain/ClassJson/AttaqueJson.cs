namespace WebApiScrapingData.Domain.ClassJson
{
    public class AttaqueJson
    {
        //Nom de l'attaque
        public string? Name { get; set; }

        //Nom de l'attaque Anglais
        public string? NameEN { get; set; }

        //Description de l'attaque
        public string? Description { get; set; }

        //Niveau
        public string? Level { get; set; }

        //Puissance de l'attaque
        public string? Power { get; set; }

        //Précision de l'attaque
        public string? Precision { get; set; }

        //PP de l'attaque
        public string? PP { get; set; }

        //Catégorie de l'attaque
        public string? Category { get; set; }

        //Type de l'attaque
        public string? Type { get; set; }

        public string? TypeLearn { get; set; }

        public string? CTCS { get; set; }

        public string? Game { get; set; }
    }
}
