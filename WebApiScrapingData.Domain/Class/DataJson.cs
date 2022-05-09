namespace WebApiScrapingData.Domain
{
    [Serializable]
    public class DataJson
    {
        //Numéro du Pokémon
        public string number = "";

        //Nom du Pokémon
        public string name = "";

        //Nom Affiché
        public string displayName = "";

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
        public string evolutions = "";

        //Type Evolution : Normal, Méga, Gigamax, Alola, Galar, Hisui
        public string typeEvolution = "";

        //Savoir Quand où comment le pokémon évolue
        public string whenEvolution = "";

        //Statistique PV
        public int statPv = 0;

        //Statistique Attaque
        public int statAttaque = 0;

        //Statistique Défense
        public int statDefense = 0;

        //Statistique Attaque Spéciale
        public int statAttaqueSpe = 0;

        //Statistique Défense Spéciale
        public int statDefenseSpe = 0;

        //Statistique Vitesse
        public int statVitesse = 0;

        //Statistique Total
        public int statTotal = 0;

        //Generation Number
        public int generation = 0;

        //Prochain Pokémon
        public string nextUrl = "";
    }
}