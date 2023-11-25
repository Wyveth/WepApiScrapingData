﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Abstract
{
    [DataContract]
    public abstract class Identity : ITIdentity
    {
        [Key]
        [DataMember(Name = DataMember.Id)]
        public long Id { get; set; }

        public string? UserCreation { get; set; }

        public DateTime DateCreation { get; set; }

        public string? UserModification { get; set; }

        public DateTime DateModification { get; set; }

        public int versionModification { get; set; }
    }
}
