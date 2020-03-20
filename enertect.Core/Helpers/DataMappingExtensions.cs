using System.Linq;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Data.Models.Ups;
using enertect.Core.ViewModels.Base;

namespace enertect.Core.Helpers
{
    public static class DataMappingExtensions
    {
        public static UpsItemViewModel ToItemViewModel(this UpsInformation item, BaseViewModel viewModel = null)
        {
           
            return new UpsItemViewModel()
            {
                ParentViewModel = viewModel,
                UpsName = item.UpsName,
                UpsId = item.UpsId,
                StringName = item.UpsId.ToString(),
                UpsInformation = item,
            };
        }
    }
}
