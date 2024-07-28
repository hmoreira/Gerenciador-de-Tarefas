namespace TaskManager.API.DTO
{
    public class AuthenticationData
    {
        public AuthenticationData(string secretKey, string issuer, string audience, int hoursExpiration)
        {
            SecretKey = secretKey;
            Issuer = issuer;
            Audience = audience;
            HoursExpiration = hoursExpiration;
        }

        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int HoursExpiration { get; set; }
    }
}
