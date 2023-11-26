
namespace WebApiScrapingData.Core
{
    public interface IGenericMapper<in TFrom, out TTo>
    {
        TTo Map(TFrom from);
    }

    // Implémentation du mapper pour transférer des données de Person à PersonDto
    public class GenericMapper<TFrom, TTo> : IGenericMapper<TFrom, TTo> 
        where TFrom : class, new()
        where TTo : class, new()
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

        public TFrom MapReverse(TTo source)
        {
            //TFrom destination = new();

            //foreach (var sourceProperty in typeof(TTo).GetProperties())
            //{
            //    var destinationProperty = typeof(TFrom).GetProperty(sourceProperty.Name);
            //    if (destinationProperty != null && destinationProperty.CanWrite)
            //    {
            //        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            //    }
            //}

            //return destination;

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

        // Méthode utilitaire pour obtenir la valeur par défaut d'un type
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
