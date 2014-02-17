using AncoraMVVM.Base.Files;
using System.IO.IsolatedStorage;
using System.Linq;

namespace AncoraMVVM.Phone7.Implementations.Files
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
