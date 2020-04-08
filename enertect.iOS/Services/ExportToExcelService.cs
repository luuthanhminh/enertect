using System;
using System.IO;
using System.Threading.Tasks;
using enertect.iOS.Services;
using enertect.iOS.Views;
using enertect.UI.Services.Interfaces;
using QuickLook;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExportToExcelService))]
namespace enertect.iOS.Services
{
    public class ExportToExcelService : IExportToExcelService
    {
        public Task<bool> ExportAsExcel(string filename, string type, MemoryStream chartStream)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, filename);

            //Create a file and write the stream into it.
            FileStream fileStream = File.Open(filePath, FileMode.Create);
            chartStream.Position = 0;
            chartStream.CopyTo(fileStream);
            fileStream.Flush();
            fileStream.Close();

            //Invoke the saved document for viewing
            UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (currentController.PresentedViewController != null)
                currentController = currentController.PresentedViewController;
            UIView currentView = currentController.View;

            QLPreviewController qlPreview = new QLPreviewController();
            QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
            qlPreview.DataSource = new PreviewControllerDS(item);

            currentController.PresentViewController(qlPreview, true, null);

            return Task.FromResult(true);
        }

    }
}
