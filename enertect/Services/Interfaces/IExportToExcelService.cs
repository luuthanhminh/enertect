using System;
using System.IO;
using System.Threading.Tasks;

namespace enertect.UI.Services.Interfaces
{
    public interface IExportToExcelService
    {
        Task<bool> ExportAsExcel(string filename, string type, MemoryStream chartStream);
    }
}
