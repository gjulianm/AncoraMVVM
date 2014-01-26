using AncoraMVVM.Base.Files;
using System.Linq;

namespace AncoraMVVM.Phone7.Implementations.Files
{
    class PhoneFile : IFile
    {
        private bool disposed = false;
        public PhoneFile(string path, FilePermissions permissions, System.IO.IsolatedStorage.IsolatedStorageFileStream stream)
        {
            // TODO: Complete member initialization
            this.CompletePath = path;
            this.Permissions = permissions;
            this.FileStream = stream;

            if (!string.IsNullOrWhiteSpace(path))
                Name = path.Split('/', '\\').Last();
        }

        public System.IO.Stream FileStream { get; set; }

        public string CompletePath { get; set; }

        public string Name { get; set; }
        public FilePermissions Permissions { get; set; }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these  
            // operations, as well as in your methods that use the resource. 
            if (!disposed)
            {
                if (disposing)
                {
                    if (FileStream != null)
                        FileStream.Dispose();
                }

                FileStream = null;
                // Indicate that the instance has been disposed.
                disposed = true;
            }
        }

    }
}
