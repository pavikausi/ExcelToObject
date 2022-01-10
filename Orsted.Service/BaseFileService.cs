using System.IO;

namespace Orsted.Service
{
    public abstract class BaseFileService
    {
        public string GetFilePath()
        {
            return Path.GetTempFileName();
        }

        public abstract bool CheckFileExtension(string fileExtension, string[] extensions);
    }
}
