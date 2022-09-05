using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Domain.Class
{
    public class DataInfo
    {
        //Nom du Pokémon
        public string name = "";

        //Nom Affiché
        public string displayName = "";

        //Description du Pokémon Version X
        public string descriptionVx = "";

        //Description du Pokémon Version
        public string descriptionVy = "";

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

        //Savoir Quand où comment le pokémon évolue
        public string whenEvolution = "";

        //Prochain Pokémon
        public string nextUrl = "";
    }
}
