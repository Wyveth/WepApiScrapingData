namespace WepApiScrapingData.DTOs
{
    public class AuthenticateUserDto
    {
        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        #endregion
    }
}
