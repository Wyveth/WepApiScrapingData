using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Mapper
{
    // Implémentation du mapper pour transférer des données de Person à PersonDto
    public class GenericMapper<TFrom, TTo> : IGenericMapper<TFrom, TTo>
    where TFrom : class, ITIdentity, new()
    where TTo : class, IIdentityDto, new()
    {
        public TTo Map(TFrom source)
        {
            if (source == null) return null;

            TTo destination = new();

            foreach (var sourceProperty in typeof(TFrom).GetProperties())
            {
                var destinationProperty = typeof(TTo).GetProperty(sourceProperty.Name);
                if (destinationProperty == null || !destinationProperty.CanWrite)
                    continue;

                var sourceValue = sourceProperty.GetValue(source);

                if (sourceValue == null)
                {
                    destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                    continue;
                }

                // Si les types sont compatibles
                if (destinationProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    destinationProperty.SetValue(destination, sourceValue);
                }
                else
                {
                    try
                    {
                        var convertedValue = Convert.ChangeType(sourceValue, destinationProperty.PropertyType);
                        destinationProperty.SetValue(destination, convertedValue);
                    }
                    catch
                    {
                        destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                    }
                }
            }

            return destination;
        }

        public TFrom MapReverse(TTo source, ScrapingContext context)
        {
            if (source == null) return null;

            TFrom destination = new();

            foreach (var sourceProperty in typeof(TTo).GetProperties())
            {
                var destinationProperty = typeof(TFrom).GetProperty(sourceProperty.Name);
                if (destinationProperty == null || !destinationProperty.CanWrite)
                    continue;

                var sourceValue = sourceProperty.GetValue(source);

                if (sourceValue == null)
                {
                    destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                    continue;
                }

                if (destinationProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    destinationProperty.SetValue(destination, sourceValue);
                }
                else
                {
                    try
                    {
                        // Gestion des propriétés de navigation via le ScrapingContext
                        string entityName = destinationProperty.PropertyType.Name;
                        var entityType = context.GetEntityTypeByName(entityName);

                        var getDbSetMethod = typeof(ScrapingContext).GetMethod(nameof(context.GetDbSetByName))
                            ?.MakeGenericMethod(entityType);
                        var dbSet = getDbSetMethod?.Invoke(context, new object[] { entityName, context });

                        if (dbSet != null && int.TryParse(sourceValue.ToString(), out int id))
                        {
                            var getByIdMethod = typeof(ScrapingContext).GetMethod(nameof(context.GetEntityById))
                                ?.MakeGenericMethod(entityType);
                            var newValue = getByIdMethod?.Invoke(context, new object[] { dbSet, id });

                            destinationProperty.SetValue(destination, newValue);
                        }
                        else
                        {
                            destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                        }
                    }
                    catch
                    {
                        destinationProperty.SetValue(destination, GetDefaultValue(destinationProperty.PropertyType));
                    }
                }
            }

            return destination;
        }

        private object GetDefaultValue(Type type)
            => type.IsValueType ? Activator.CreateInstance(type) : null;


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
