﻿using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    public class Answer : Identity
    {
        //Numéro d'ordre Des Réponses
        public int Order { get; set; }

        //Libellé des réponses
        public string? Libelle { get; set; }

        //Type des réponses
        public string? Type { get; set; }

        //Si la réponse est sélectionnée
        public bool IsSelected { get; set; }

        //Si réponse correcte
        public bool IsCorrect { get; set; }
    }
}
