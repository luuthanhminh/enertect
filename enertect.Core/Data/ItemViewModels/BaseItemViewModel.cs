using System;
using MvvmCross.ViewModels;

namespace enertect.Core.Data.ItemViewModels
{
    public class BaseItemViewModel : MvxViewModel
    {
        public MvxViewModel ParentViewModel { get; set; }
    }
}
