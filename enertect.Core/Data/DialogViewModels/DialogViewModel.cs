using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace enertect.Core.Data.DialogViewModels
{
    public class DialogViewModel : MvxViewModel
    {
        public TaskCompletionSource<string> CompletionSource;

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                SetProperty(ref _message, value);
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        private string _confirmText = "OK";
        public string ConfirmText
        {
            get
            {
                return _confirmText;
            }
            set
            {
                SetProperty(ref _confirmText, value);
            }
        }

        private string _closeText = "Close";
        public string CloseText
        {
            get
            {
                return _closeText;
            }
            set
            {
                SetProperty(ref _closeText, value);
            }
        }

        private ObservableCollection<DialogAction> _actions = new ObservableCollection<DialogAction>();
        public ObservableCollection<DialogAction> Actions
        {
            get
            {
                return _actions;
            }
            set
            {
                SetProperty(ref _actions, value);
            }
        }

    }

    public class DialogAction : MvxViewModel
    {
        public string Icon { get; set; }

        public string Label { get; set; }
    }
}
