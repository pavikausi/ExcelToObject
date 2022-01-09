using System.Collections.Generic;
using Orsted.Model;
using System.IO;

namespace Orsted.Service
{
    public interface IExcelService
    {
        public List<Employee> ReadFile(Stream stream, string fileExt);
    }
}
