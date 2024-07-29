namespace TaskManager.API.DTO
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public double ExpiresIn { get; set; }
        public UsuarioPutGet User { get; set; } = null!;
    }
}
