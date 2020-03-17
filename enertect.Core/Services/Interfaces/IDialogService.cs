using System;
using System.Threading.Tasks;
using enertect.Core.Data.DialogViewModels;

namespace enertect.Core.Services.Interfaces
{
    public interface IDialogService
    {
        Task<bool> ShowMessage(string title, string message, string buttonConfirmText, string buttonCancelText);

        //Task<string> ShowMultipleSelection(string title, DialogAction[] options);

        //Task ShowMessage(string title, string message, string buttonCloseText);

        //Task ShowMessage(string message);
    }
}
