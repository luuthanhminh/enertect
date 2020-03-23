using System;
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

        public static UpLimit ToUpLimit(this UpLimit item)
        {
            UpLimit limit = item;
            limit.TempUp = AppConstant.TEMP_UP;
            limit.TempDown = AppConstant.TEMP_DOWN;
            return limit;
        }

        public static UpsInformation ConvertDate(this UpsInformation item)
        {
            foreach (UpsInformation up in item.UpsHistoryTrendings)
            {
                DateTime parsedDate = DateTime.Parse(up.DateTime);
                up.DateValue = parsedDate.ToString("MM/dd");
            }
            
            return item;
        }
    }
}
