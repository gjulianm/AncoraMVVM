using System;
using System.IO;

namespace AncoraMVVM.Base.Files
{
    public class File : IDisposable
    {
        private bool disposed = false;

        public Stream FileStream { get; set; }
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
