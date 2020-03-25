using System;
using System.Collections.Generic;
using System.Linq;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Data.Models;
using enertect.Core.Data.Models.Ups;
using enertect.Core.ViewModels.Base;

namespace enertect.Core.Helpers
{
    public static class DataMappingExtensions
    {
        public static UpsItemViewModel ToItemViewModel(this UpsInformation item, BaseViewModel viewModel = null)
        {
            string StringName = item.StringName;
            if (item.Items != null  && item.Items.Any())
            {
                StringName = item.Items.First().StringName;
            }
            var ItemViewModel = new UpsItemViewModel()
            {
                UpsName = item.UpsName,
                UpsId = item.UpsId,
                BatteryId = item.BatteryId,
                StringName = StringName,
                Voltage = item.Voltage,
                Resitance = item.Resitance,
                Temperature = item.Temperature
            };
            if (item.Items != null)
            {
                foreach (UpsInformation up in item.Items)
                {
                    var subItemViewModel = ToItemViewModel(up);
                    ItemViewModel.Items.Add(subItemViewModel);
                }
            }

            if (item.UpsHistoryTrendings != null)
            {
                foreach (UpsInformation up in item.UpsHistoryTrendings)
                {
                    var subItemViewModel = ToItemViewModel(up);
                    DateTime parsedDate = DateTime.Parse(up.DateTime);
                    subItemViewModel.DateValue = parsedDate.ToString("MM/dd");
                    ItemViewModel.UpsHistoryTrendings.Add(subItemViewModel);
                }
            }
            
            return ItemViewModel;
        }

        public static UpLimitViewModel ToUpLimitViewModel(this UpLimit item)
        {
            UpLimit limit = item;
            limit.TempUp = AppConstant.TEMP_UP;
            limit.TempDown = AppConstant.TEMP_DOWN;
            return new UpLimitViewModel()
            {
                VolUp = item.VolUp,
                VolDown = item.VolDown,
                IrUp = item.IrUp,
                IrDown = item.IrDown,
                TempUp = item.TempUp,
                TempDown = item.TempDown
            };
        }

        public static AlarmItemViewModel ToAlarmItemViewModel(this Alarm item, IList<UpsItemViewModel> ups)
        {
            UpsItemViewModel Up = ups.First(i => i.UpsId == item.Upsid);
            if(Up.StringName == null)
            {
                Up.StringName = "";
            }
            return new AlarmItemViewModel()
            {
              AlarmDate = DateTime.Parse(item.AlarmDate).ToString("dd-MM-yyyy hh:mm:ss") ,
              AlertType = item.AlertType,
              AlertValue = item.AlertValue,
              ActionTaken = String.IsNullOrEmpty(item.ActionTaken) ? "Update Action" : item.ActionTaken,
              UpsName = Up.UpsName,
              StringName = Up.StringName,
              Status = "Alarm",
              Brand = "Rocket",
              ProblemResolvedDate = String.IsNullOrEmpty(item.ProblemResolvedDate) ? " " : DateTime.Parse(item.ProblemResolvedDate).ToString("dd-MM-yyyy hh:mm:ss")
            };
        }

    }
}
