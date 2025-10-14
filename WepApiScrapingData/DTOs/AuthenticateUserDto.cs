using ScrapySharp.Core;

namespace WepApiScrapingData.DTOs
{
    public class AuthenticateUserDto
    {
        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserName { get; set; }
        #endregion

        public AuthenticateUserDto(string email, string password, string username)
        {
            Email = email;
            Password = password;
            UserName = username;
        }
    }
}
