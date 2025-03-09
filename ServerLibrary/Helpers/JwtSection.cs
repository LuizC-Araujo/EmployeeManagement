namespace ServerLibrary.Helpers
{
    public class JwtSection
    {
        public string? Key { get; private set; }
        public string? Issuer { get; private set; }
        public string? Audience { get; private set; }
    }
}
