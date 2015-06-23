using System.IO;
using AncoraMVVM.Base.Files;

namespace AncoraMVVM.Phone.Implementations.Files
{
    internal static class EnumConverters
    {
        public static FileMode ToFileMode(FileOpenMode mode)
        {
            switch (mode)
            {
                case FileOpenMode.Append:
                    return FileMode.Append;
                case FileOpenMode.Create:
                    return FileMode.Create;
                case FileOpenMode.Open:
                    return FileMode.Open;
                case FileOpenMode.OpenOrCreate:
                    return FileMode.OpenOrCreate;
                case FileOpenMode.Truncate:
                    return FileMode.Truncate;
                default:
                    return FileMode.OpenOrCreate;
            }
        }

        public static FileAccess ToFileAccess(FilePermissions perms)
        {
            switch (perms)
            {
                case FilePermissions.Read:
                    return FileAccess.Read;
                case FilePermissions.ReadWrite:
                    return FileAccess.ReadWrite;
                case FilePermissions.Write:
                    return FileAccess.Write;
                default:
                    return FileAccess.ReadWrite;
            }
        }
    }
}
