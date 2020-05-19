using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using enertect.Core.Data.DialogViewModels;
using enertect.Core.Services.Interfaces;
using enertect.UI.Controls.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace enertect.UI.Services
{
    public class DialogService : IDialogService
    {
        const string APP_NAME = "Enertect";
        public async Task<bool> ShowMessage(string title, string message, string buttonConfirmText, string buttonCancelText)
        {
            try
            {
                var result = await Application.Current.MainPage.DisplayAlert(title, message, buttonConfirmText, buttonCancelText);

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<string> ShowMessage(string title, string message, string buttonCloseText)
        {
            try
            {
                var model = new DialogViewModel()
                {
                    Title = title,
                    Message = message,
                    CloseText = buttonCloseText
                };

                model.CompletionSource = new TaskCompletionSource<string>();

                var simpleDialog = new SimpleDialogNoTitle(model);

                await PopupNavigation.Instance.PushAsync(simpleDialog);

                var result = await model.CompletionSource.Task;

                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<string> ShowMultipleSelection(string title, DialogAction[] options)
        {
            try
            {
                var dialogViewModel = new DialogViewModel()
                {
                    Title = title,
                    Actions = new ObservableCollection<DialogAction>(options)
                };

                dialogViewModel.CompletionSource = new TaskCompletionSource<string>();



                var dialog = new CustomActionSheet(dialogViewModel);

                await PopupNavigation.Instance.PushAsync(dialog);

                var result = await dialogViewModel.CompletionSource.Task;

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task ShowMessage(string message)
        {
            try
            {
                var simpleDialog = new SimpleDialogNoTitle(new DialogViewModel()
                {
                    Title = APP_NAME,
                    Message = message,
                });

                await PopupNavigation.Instance.PushAsync(simpleDialog);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
