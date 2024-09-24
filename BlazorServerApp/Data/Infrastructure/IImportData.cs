using BlazorServerApp.Data.Models;

namespace BlazorServerApp.Data.Infrastructure
{
    public interface IImportData
    {
        Task<List<ImportData>> GetImportData();
    }
}
