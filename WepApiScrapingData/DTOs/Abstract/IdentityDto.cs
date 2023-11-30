using WebApiScrapingData.Domain.Interface;

namespace WepApiScrapingData.DTOs.Abstract
{
    public abstract class IdentityDto: IIdentityDto
    {
        public long Id { get; set; }
    }
}
