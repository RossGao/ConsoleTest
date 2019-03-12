using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebTest
{
    [Route("importexport")]
    public class ImportExportController : Controller
    {
        private IHostingEnvironment host;

        public ImportExportController(IHostingEnvironment theHost)
        {
            host = theHost;
        }

        [HttpPost]
        public IActionResult Import(IFormFile student)
        {
            if (student != null)
            {
                var workbook = WorkbookFactory.Create(student.OpenReadStream());

                if (workbook.NumberOfSheets > 0)
                {
                    var sheet = workbook.GetSheetAt(0);

                    if (sheet != null && sheet.LastRowNum > 1)
                    {
                        var data = ConvertToObjs<Student>(sheet);

                        if (data != null && data.Count > 0)
                        {
                            return Ok(data);
                        }

                        return NotFound();
                    }
                }
            }

            return BadRequest();
        }

        [Route("pattern")]
        [HttpGet]
        public async Task<IActionResult> OnPostExportAsync()
        {
            var rootPath = host.WebRootPath;
            var fileName = @"ImportPattern.xlsx";
            var url = $"{Request.Scheme}{Request.Host}{fileName}";
            var file = new FileInfo(Path.Combine(rootPath, fileName));
            var memory = new MemoryStream();

            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootPath, fileName));
            }

            using (var fs = new FileStream(Path.Combine(rootPath, fileName), FileMode.Create, FileAccess.Write))
            {
                var wb = new XSSFWorkbook();
                var sheet = wb.CreateSheet(fileName);
                var row = sheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("姓名");
                row.CreateCell(1).SetCellValue("身份证号");
                row.CreateCell(2).SetCellValue("城市");
                row.CreateCell(3).SetCellValue("性别");
                row.CreateCell(4).SetCellValue("婚否");
                row.CreateCell(5).SetCellValue("年龄");
                row.CreateCell(6).SetCellValue("部门");
                row.CreateCell(7).SetCellValue("手机号码");
                row.CreateCell(8).SetCellValue("入职时间");
                row.CreateCell(9).SetCellValue("备注信息");

                wb.Write(fs);
            }

            using (var stream = new FileStream(Path.Combine(rootPath, fileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        private ICollection<T> ConvertToObjs<T>(ISheet sheet) where T : new()
        {
            var header = sheet.GetRow(0);
            var objs = new T[sheet.LastRowNum].Select(t => new T()).ToArray();

            if (header.LastCellNum > 0)
            {
                var formatter = new DataFormatter(CultureInfo.CurrentCulture);

                for (int i = 0; i < header.LastCellNum; i++)
                {
                    var propertyCell = header.GetCell(i);
                    var propertyInfo = typeof(T).GetProperty(propertyCell.StringCellValue); // The property must be pubic.
                    if (propertyInfo != null)
                    {
                        for (int j = 0; j < objs.Length; j++)
                        {
                            var valueRow = sheet.GetRow(j + 1);
                            var cellValue = formatter.FormatCellValue(valueRow.GetCell(i));
                            propertyInfo.SetValue(objs[j], Convert.ChangeType(cellValue, propertyInfo.PropertyType));
                        }
                    }
                }
            }

            return objs;
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Grade { get; set; }
    }
}

