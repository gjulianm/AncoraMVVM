using AncoraMVVM.Base.Files;
using System.IO.IsolatedStorage;

namespace AncoraMVVM.Phone7.Implementations.Files
{
    public class PhoneFileManager : BaseFileManager
    {
        public override File OpenFile(string path, FilePermissions permissions, FileOpenMode mode)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var stream = isolatedStorage.OpenFile(path, EnumConverters.ToFileMode(mode), EnumConverters.ToFileAccess(permissions));
                return new PhoneFile(path, permissions, stream);
            }
        }

        public override void DeleteFile(string path)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                isolatedStorage.DeleteFile(path);
            }
        }

        public override System.Collections.Generic.IEnumerable<string> GetFilesIn(string path)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                return isolatedStorage.GetFileNames(path + "*");
            }
        }

        public override void CreateFolder(string folderPath)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                isolatedStorage.CreateDirectory(folderPath);
            }
        }

        public override void DeleteFolder(string folderPath)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                isolatedStorage.DeleteDirectory(folderPath);
            }
        }
    }
}
