using System.Collections.Generic;

namespace Galleria
{
    public class UploadData
    {
        public IList<Category> categories { get;set; }
        public IList<Color> colors { get;set; }
        public bool isModelValid { get;set; } = false;
        public bool isFileValid { get;set; } = false;
        public bool isScanValid { get;set; } = false;
        public string validationError { get;set; }
    }
}