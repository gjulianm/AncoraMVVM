
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace AncoraMVVM.Base.Files
{
    public abstract class BaseFileManager : IFileManager
    {
        public virtual async Task<IEnumerable<string>> ReadLines(string fileName)
        {
            var list = new List<string>();
            var str = await ReadContents(fileName);

            using (var sReader = new StringReader(str))
            {
                string line;
                while ((line = sReader.ReadLine()) != null)
                    list.Add(line);
            }

            return list;
        }

        public async Task WriteLines(string fileName, IEnumerable<string> lines)
        {
            string text = "";
            foreach (var line in lines)
                text += line + Environment.NewLine;
            await WriteContents(fileName, text);
        }

        public async Task<string> ReadContents(string fileName)
        {
            var list = new List<string>();
            using (var file = OpenFile(fileName, FilePermissions.Read, FileOpenMode.OpenOrCreate))
            {
                using (var reader = new StreamReader(file.FileStream))
                {
                    var str = await reader.ReadToEndAsync();
                    return str;
                }
            }
        }

        public async Task WriteContents(string fileName, string contents)
        {
            using (var file = OpenFile(fileName, FilePermissions.Write, FileOpenMode.Create))
            {
                using (var writer = new StreamWriter(file.FileStream))
                {
                    await writer.WriteAsync(contents);
                }
            }
        }

        public abstract IFile OpenFile(string path, FilePermissions permissions, FileOpenMode mode);
        public abstract void DeleteFile(string path);
        public abstract IEnumerable<string> GetFilesIn(string path);
        public abstract void CreateFolder(string folderPath);
        public abstract void DeleteFolder(string folderPath);
    }
}
