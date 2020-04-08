using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using enertect.Droid.Services;
using enertect.UI.Services.Interfaces;
using Java.IO;
using Android;
using Android.Content.PM;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Android.Widget;

[assembly: Dependency(typeof(ExportToExcelService))]
namespace enertect.Droid.Services
{
    public class ExportToExcelService : IExportToExcelService
    {
        
        public Task<bool> ExportAsExcel(string filename, string type, MemoryStream chartStream)
        {
            try
            {
                string root = null;
                //Get the root path in android device.
                var context = CrossCurrentActivity.Current.AppContext;
                var activity = CrossCurrentActivity.Current.Activity;
                int YOUR_ASSIGNED_REQUEST_CODE = 9;

                if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted &&
                    ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
                {
                    if (Android.OS.Environment.IsExternalStorageEmulated)
                    {
                        root = Android.OS.Environment.ExternalStorageDirectory.ToString();
                    }
                    else
                        root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    //Create directory and file 
                    Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
                    myDir.Mkdir();

                    Java.IO.File file = new Java.IO.File(myDir, filename);

                    //Remove if the file exists
                    if (file.Exists()) file.Delete();

                    //Write the stream into the file
                    FileOutputStream outs = new FileOutputStream(file);
                    outs.Write(chartStream.ToArray());

                    outs.Flush();
                    outs.Close();

                    //Invoke the created file for viewing
                    if (file.Exists())
                    {
                        Android.Net.Uri pathProvider = Android.Net.Uri.FromFile(file);
                        if (((int)Android.OS.Build.VERSION.SdkInt) > 24)
                        {
                            pathProvider = FileProvider.GetUriForFile(activity, context.PackageName + ".provider", file);
                        }
                        string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(pathProvider.ToString());
                        string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                        Intent intent = new Intent(Intent.ActionView);
                        intent.SetDataAndType(pathProvider, mimeType);
                        intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                        var shareIntent = Intent.CreateChooser(intent, "Choose App");
                        shareIntent.AddFlags(ActivityFlags.NewTask);
                        context.StartActivity(shareIntent);
                    }

                    return Task.FromResult(true);
                }
                else
                {
                    ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, YOUR_ASSIGNED_REQUEST_CODE);
                }
                return Task.FromResult(false);
            }
            catch(Exception ex)
            {
                var context = CrossCurrentActivity.Current.AppContext;
                Toast.MakeText(context, ex.Message, ToastLength.Short).Show();
                return Task.FromResult(false);
            }
            
        }
    }
}
