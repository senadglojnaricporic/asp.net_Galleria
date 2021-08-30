namespace Galleria.Services
{
    public class HelperOptions
    {
        public string ImageFolder { get;set; }
        public string TempFolder { get;set; }
        public long FileSizeLimit { get;set; }
        public string[] PermittedFileExtensions { get;set; }
        public string CloudmersiveApiKey { get;set; }
    }
}

