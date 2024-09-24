using BlazorServerApp.Data.Infrastructure;
using BlazorServerApp.Data.Models;

namespace BlazorServerApp.Data.Service
{
    public class ImportDataService : IImportData
    {
        private readonly HttpClient _httpClient;
        public ImportDataService(HttpClient httpClient) 
        {
           this._httpClient = httpClient;
        }
        public async Task<List<ImportData>> GetImportData()
        {
            List<ImportData> data = new List<ImportData>();
            data = await _httpClient.GetFromJsonAsync<List<ImportData>>("api/ImportData/GetData");
            return data;
        }
    }
}
