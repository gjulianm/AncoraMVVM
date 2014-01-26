
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AncoraMVVM.Base.Files
{
    public interface IFileManager
    {
        IFile OpenFile(string path, FilePermissions permissions, FileOpenMode mode);
        void DeleteFile(string path);
        IEnumerable<string> GetFilesIn(string path);
        void CreateFolder(string folderPath);
        void DeleteFolder(string folderPath);
        Task<IEnumerable<string>> ReadLines(string fileName);
        Task WriteLines(string fileName, IEnumerable<string> lines);
        Task<string> ReadContents(string fileName);
        Task WriteContents(string fileName, string contents);
    }
}
