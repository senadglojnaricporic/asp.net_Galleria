using Microsoft.AspNetCore.Http;

namespace Galleria.Interfaces
{
    public interface IFileHelper
    {
        string Message { get;set; }

        string DisplayName(string FileName);

        string DBFileURL(string FileName);

        string FileURL(string FileName);

        string TempFileURL(string FileName);

        bool isFileValid(IFormFile formFile);

        bool isScanValid(string TempFileURL);
    }
}