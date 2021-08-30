using Microsoft.AspNetCore.Hosting;
using Galleria.Interfaces;

namespace Galleria.Services
{
    class EmailText : IEmailText
    {
        private readonly IWebHostEnvironment _env;

        public EmailText(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string getEmailTitle()
        {
            return "Welcome to Galleria";
        }

        public string getEmailText()
        {
            return System.IO.File.ReadAllText(_env.WebRootPath + "/email/email.html");
        }
    }
}