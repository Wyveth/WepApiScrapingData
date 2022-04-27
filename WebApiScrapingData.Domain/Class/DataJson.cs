namespace WebApiScrapingData.Domain
{
    [Serializable]
    public class DataJson
    {
        //Numéro du Pokémon
        public string number = "";

        //Nom du Pokémon
        public string name = "";

        //Description du Pokémon Version X
        public string descriptionVx = "";

        //Description du Pokémon Version
        public string descriptionVy = "";

        //Url de l'Image
        public string urlImg = "";

        //Url du Sprite
        public string urlSprite = "";

        //Taille du Pokémon
        public string size = "";

        //Catégorie du Pokémon
        public string category = "";

        //Poids du Pokémon
        public string weight = "";

        //Talent du Pokémon
        public string talent = "";

        //Description du Talent
        public string descriptionTalent = "";

        //Nom des Types
        public string types = "";

        //Nom des Faiblesses
        public string weakness = "";

        //Evolution/Famille du Pokémon
        public string evolution = "";

        //Type Evolution : Normal, Méga, Gigamax, Alola, Galar, Variant
        public string typeEvolution = "";

        //Generation Number
        public int generation = 0;

        //Prochain Pokémon
        public string nextUrl = "";
    }
}