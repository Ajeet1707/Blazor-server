using Microsoft.AspNetCore.Mvc;
using WebAPIEndPoint.Models;

namespace WebAPIEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportDataController : ControllerBase
    {
        [HttpGet("GetData")]
        public IActionResult GetImportData()
        {
            List<ImportData> data = new List<ImportData>();
            data.Add(new ImportData
            {
                Category = "Import",
                ProdName = "Fisg Meat",
                CreatedAt = DateTime.Now.ToString(),
                Id = "1"
            });
            data.Add(new ImportData
            {
                Category = "Import",
                ProdName = "Pork Meat",
                CreatedAt = DateTime.Now.ToString(),
                Id = "2"
            });
            data.Add(new ImportData
            {
                Category = "Export",
                ProdName = "Raw Material",
                CreatedAt = DateTime.Now.ToString(),
                Id = "3"
            });

            return Ok(data);
        }
    }
}
