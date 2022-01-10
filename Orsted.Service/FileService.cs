namespace Orsted.Service
{
    public class FileService : BaseFileService
    {
        public override bool CheckFileExtension(string fileExtension, string[] extensions)
        {
            foreach (var extension in extensions)
            {
                if (fileExtension == extension)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
