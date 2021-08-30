using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Galleria.Interfaces;
using Microsoft.Extensions.Options;
using Cloudmersive.APIClient.NETCore.VirusScan.Api;
using Cloudmersive.APIClient.NETCore.VirusScan.Client;
using Cloudmersive.APIClient.NETCore.VirusScan.Model;

namespace Galleria.Services
{
    class FileHelper : IFileHelper
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignature = 
        new Dictionary<string, List<byte[]>>
        {
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            },
            { ".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                }
            },
        };

        private readonly IWebHostEnvironment _env;
        private readonly string _trustedFileName;
        private HelperOptions Options { get; }
        public string Message { get;set; }
        public FileHelper(IOptions<HelperOptions> optionsAccessor, IWebHostEnvironment env)
        {
            Options = optionsAccessor.Value;
            _env = env;
            _trustedFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }
        public string DisplayName(string FileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(FileName);
            var encodedName = HttpUtility.HtmlEncode(fileName);

            return encodedName;
        }

        public string DBFileURL(string FileName)
        {
            var fileExt = Path.GetExtension(FileName);
            var fileName = _trustedFileName + fileExt;
            return Path.Combine(Options.ImageFolder, fileName);
        }

        public string FileURL(string FileName)
        {
            var fileExt = Path.GetExtension(FileName);
            var fileName = _trustedFileName + fileExt;
            return Path.Combine(_env.WebRootPath + Options.ImageFolder, fileName);
        }

        public string TempFileURL(string FileName)
        {
            var tempFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var fileExt = Path.GetExtension(FileName);
            var fileName = tempFileName + fileExt;
            return Path.Combine(_env.WebRootPath + Options.TempFolder, fileName);
        }

        private bool isExtensionValid(IFormFile formFile)
        {
            var permittedExt = new List<String>();
            permittedExt.AddRange(Options.PermittedFileExtensions);
            var ext = Path.GetExtension(formFile.FileName);
            var extFound = permittedExt.Contains(ext);
            var extValid = false;
            if(extFound)
            {
                using (var reader = new BinaryReader(formFile.OpenReadStream()))
                {
                    var signatures = _fileSignature[ext];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    extValid = signatures.Any(signature => 
                        headerBytes.Take(signature.Length).SequenceEqual(signature));
                }
                return extFound && extValid;
            }
            return false;
        }

        private bool isFileSizeValid(long FileSize)
        {
            return FileSize < Options.FileSizeLimit;
        }

        public bool isFileValid(IFormFile formFile)
        {
            return isFileSizeValid(formFile.Length) && isExtensionValid(formFile);
        }

        public bool isScanValid(string TempFileURL)
        {
            Configuration.Default.AddApiKey("Apikey", Options.CloudmersiveApiKey);
            var apiInstance = new ScanApi();
            var inputFile = new System.IO.FileStream(TempFileURL, System.IO.FileMode.Open);
            try
            {
                // Scan a file for viruses
                VirusScanResult result = apiInstance.ScanFile(inputFile);
                var scanResult = (bool) result.CleanResult;
                if(!scanResult)
                {
                    Message = "Viruses found: ";
                    foreach(VirusFound virus in result.FoundViruses)
                    {
                        Message = Message + virus.VirusName + " ";
                    }
                }
                inputFile.Close();
                return (bool) result.CleanResult;
            }
            catch (Exception e)
            {
                Message = "Exception: " + e.Message;
                inputFile.Close();
                return false;
            }
        }
    }
}