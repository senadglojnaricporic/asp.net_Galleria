namespace Galleria
{
    public class LoginData
    {
        public bool Succeeded { get;set; }
        public bool RequiresTwoFactor { get;set; }
        public bool IsLockedOut { get;set; }
    }
}