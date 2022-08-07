using WebApiScrapingData.Domain.Class;

namespace WebApiScrapingData.Domain
{
    [Serializable]
    public class DataJson
    {
        //Numéro du Pokémon
        public string number = "";

        public DataInfo FR;

        public DataInfo EN;

        public DataInfo ES;

        public DataInfo IT;

        public DataInfo DE;

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

        //Url de l'Image
        public string urlImg = "";

        //Url du Sprite
        public string urlSprite = "";

        public DataJson()
        {
            FR = new DataInfo();
            EN = new DataInfo();
            ES = new DataInfo();
            IT = new DataInfo();
            DE = new DataInfo();
        }
    }
}