using System.IO;

namespace AncoraMVVM.Rest
{
    /// <summary>
    /// Container for a file upload.
    /// </summary>
    public class HttpFileUpload
    {
        public HttpFileUpload(Stream fileStream, string paramName, string fileName)
        {
            FileStream = fileStream;
            Parameter = paramName;
            Filename = fileName;
        }

        public Stream FileStream { get; set; }
        public string Parameter { get; set; }
        public string Filename { get; set; }
    }
}
