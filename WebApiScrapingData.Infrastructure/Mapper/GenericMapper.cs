using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Mapper
{
    // Implémentation du mapper pour transférer des données de Person à PersonDto
    public class GenericMapper<TFrom, TTo> : IGenericMapper<TFrom, TTo>
        where TFrom : class, ITIdentity,  new()
        where TTo : class, IIdentityDto, new()
    {
        public TTo Map(TFrom source)
        {
            //TTo destination = new();

            //foreach (var sourceProperty in typeof(TFrom).GetProperties())
            //{
            //    var destinationProperty = typeof(TTo).GetProperty(sourceProperty.Name);
            //    if (destinationProperty != null && destinationProperty.CanWrite)
            //    {
            //        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            //    }
            //}

            //return destination;

            TTo destination = new();

            foreach (var sourceProperty in typeof(TFrom).GetProperties())
            {
                var destinationProperty = typeof(TTo).GetProperty(sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // Vérifiez si les types sont compatibles avant de définir la valeur
                    if (sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                    }
                    else
                    {
                        // Ajoutez une logique de conversion si les types ne sont pas directement compatibles
                        // Exemple basique : vous pourriez utiliser Convert.ChangeType
                        var convertedValue = Convert.ChangeType(sourceProperty.GetValue(source), destinationProperty.PropertyType);
                        destinationProperty.SetValue(destination, convertedValue);
                    }
                }
                // Ajoutez une logique pour traiter les propriétés manquantes si nécessaire
                // Exemple : initialisez avec une valeur par défaut
                else if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                }
            }

            return destination;
        }

        public TFrom MapReverse(TTo source, ScrapingContext context)
        {
            TFrom destination = new();

            foreach (var sourceProperty in typeof(TTo).GetProperties())
            {
                var destinationProperty = typeof(TFrom).GetProperty(sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // Vérifiez si les types sont compatibles avant de définir la valeur
                    if (sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                    }
                    else
                    {
                        string name = "";
                        // Gérez les propriétés de navigation en appliquant récursivement la logique de mappage
                        if (destinationProperty.PropertyType.Name.Contains("List"))
                        {
                            var lolilol = typeof(TFrom).Name;
                            name = destinationProperty.Name;
                            name = GetType(destinationProperty.Name);
                        }
                        else
                        {
                            name = destinationProperty.PropertyType.Name;
                        }
                        
                        var entityType = context.GetEntityTypeByName(name);
                        var method = typeof(ScrapingContext).GetMethod(nameof(context.GetDbSetByName));
                        var genericMethod = method?.MakeGenericMethod(entityType);
                        object[] parameters = { name, context };
                        var dbSet = genericMethod?.Invoke(context, parameters);

                        if (dbSet != null)
                        {
                            var id = int.Parse(sourceProperty.GetValue(source).ToString());

                            var methodGetById = typeof(ScrapingContext).GetMethod(nameof(context.GetEntityById));
                            var genericMethodGetById = methodGetById?.MakeGenericMethod(entityType);
                            object[] parametersGetById = { dbSet, id };

                            var newValue = genericMethodGetById?.Invoke(context, parametersGetById);

                            var convertedValue = Convert.ChangeType(newValue, destinationProperty.PropertyType);
                            destinationProperty.SetValue(destination, convertedValue);
                        }
                        
                    }
                }
                // Ajoutez une logique pour traiter les propriétés manquantes si nécessaire
                // Initialisez avec une valeur par défaut
                else if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                }
            }

            return destination;
        }

        // Méthode utilitaire pour obtenir la valeur par défaut d'un type
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private string GetName(string type, string objName)
        {
            string name = "";
                switch (type)
                {
                    case "Pokemon_TypePoks":
                        name = "TypePok";
                        break;
                    case "Pokemon_Weaknesses":
                        name = "TypePok";
                        break;
                    case "Pokemon_Talents":
                        name = "Talent";
                        break;
                    case "Pokemon_Attaques":
                        name = "Attaque";
                        break;
                    default:
                        name = "";
                        break;
                }

            return name;
        }

        private string GetType(string type)
        {
            string name = "";
            switch (type)
            {
                case "Pokemon_Attaques":
                    name = "Pokemon_Attaque";
                break;
                case "Pokemon_Talents":
                    name = "Pokemon_Talent";
                break;
                case "Pokemon_TypePoks":
                    name = "Pokemon_TypePok";
                break;
                case "Pokemon_Weaknesses":
                    name = "Pokemon_TypePok";
                break;
                default:
                    name = "";
                break;
            }

            return name;
        }
    }
}
