using System.IO.IsolatedStorage;
using System.Linq;
using AncoraMVVM.Base.Files;

namespace AncoraMVVM.Phone.Implementations.Files
{
    class PhoneFile : File
    {
        public PhoneFile(string path, FilePermissions permissions, IsolatedStorageFileStream stream)
        {
            this.CompletePath = path;
            this.Permissions = permissions;
            this.FileStream = stream;

            if (!string.IsNullOrWhiteSpace(path))
                Name = path.Split('/', '\\').Last();
        }
    }
}
