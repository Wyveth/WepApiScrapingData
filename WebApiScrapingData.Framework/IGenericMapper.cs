
using System.Reflection;

namespace WebApiScrapingData.Core
{
    public interface IGenericMapper<in TFrom, out TTo>
    {
        TTo Map(TFrom from);
    }
}

