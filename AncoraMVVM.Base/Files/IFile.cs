using System;
using System.IO;

namespace AncoraMVVM.Base.Files
{
    public interface IFile : IDisposable
    {
        Stream FileStream { get; set; }
        string CompletePath { get; set; }
        string Name { get; set; }
        FilePermissions Permissions { get; set; }
    }
}
