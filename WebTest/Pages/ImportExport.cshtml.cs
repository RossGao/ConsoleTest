using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.XSSF.UserModel;

namespace WebTest.Pages
{
    public class ImportExportModel : PageModel
    {
        private IHostingEnvironment host;

        public ImportExportModel(IHostingEnvironment theHost)
        {
            host = theHost;
        }

        public void OnGet()
        {
        }

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

                row.CreateCell(0).SetCellValue("����");
                row.CreateCell(1).SetCellValue("���֤��");
                row.CreateCell(2).SetCellValue("����");
                row.CreateCell(3).SetCellValue("�Ա�");
                row.CreateCell(4).SetCellValue("���");
                row.CreateCell(5).SetCellValue("����");
                row.CreateCell(6).SetCellValue("����");
                row.CreateCell(7).SetCellValue("�ֻ�����");
                row.CreateCell(8).SetCellValue("��ְʱ��");
                row.CreateCell(9).SetCellValue("��ע��Ϣ");

                wb.Write(fs);
            }

            using (var stream = new FileStream(Path.Combine(rootPath, fileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}