namespace LearnTutorial.Models
{
    public class AuthenticationConfig
    {
        public string CookieName { get; set; }
        public string LoginPath { get; set; }
        public string LogoutPath { get; set; }
        public string AccessDeniedPath { get; set; }
    }
}
