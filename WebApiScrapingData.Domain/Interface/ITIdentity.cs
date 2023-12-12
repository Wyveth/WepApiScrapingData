namespace WebApiScrapingData.Domain.Interface
{
    public interface ITIdentity
    {
        public long Id { get; set; }
        
        public Guid Guid { get; set; }

        public string? UserCreation { get; set; }

        public DateTime DateCreation { get; set; }

        public string? UserModification { get; set; }

        public DateTime DateModification { get; set; }

        public int versionModification { get; set; }
    }
}
