namespace Orsted.Service
{
    public class FileService : BaseFileService
    {
        public override bool CheckFileExtension(string fileExtension)
        {
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                return false;
            }
            return true;
        }
    }
}
