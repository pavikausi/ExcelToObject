using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Orsted.Employee.Authorization;
using Orsted.Service;
namespace Orsted.Employee.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiAuthorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly FileService _fileService;

        public EmployeeController(IExcelService excelService, FileService fileService)
        {
            _excelService = excelService;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ActionResult> List(IFormFile file)
        {
            List<Model.Employee> employees = new List<Model.Employee>();

            try
            {
                var fileSelected = IsFileSelected(file);
                if (!fileSelected)
                {
                    return BadRequest("File Not Selected");
                }
                var fileExtension = Path.GetExtension(file.FileName);
                var validFileFormat = _fileService.CheckFileExtension(fileExtension, new string[] { ".xls", ".xlsx" });
                if (!validFileFormat)
                {
                    return BadRequest("Invalid File Format");
                }

                //Read File
                var filePath = _fileService.GetFilePath();
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                    employees = _excelService.ReadFile(stream, fileExtension);
                }
                return employees.Count > 0 ? Ok(employees) : (ActionResult)BadRequest("Selected file is empty.");
            }
            catch (Exception)
            {
                return BadRequest("Something Went Wrong!, The Excel file uploaded has failed.");
            }
        }

        private bool IsFileSelected(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
