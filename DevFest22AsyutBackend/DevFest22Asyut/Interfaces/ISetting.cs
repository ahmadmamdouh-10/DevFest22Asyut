namespace DevFest22Asyut.Interfaces
{
    public interface ISetting
    {
        string Issuer { get; set; }
        string SecretKey { get; set; }
        string From { get; set; }
        string Password { get; set; }
        string Host { get; set; }
        int Port { get; set; }

    }

    public class Setting : ISetting
    {
        public string? Issuer { get; set; }
        public string? SecretKey { get; set; }
        public string? From { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
    }

}
