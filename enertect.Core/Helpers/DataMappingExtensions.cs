using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Data.Models;
using enertect.Core.Data.Models.Home;
using enertect.Core.Data.Models.Ups;
using enertect.Core.ViewModels.Base;

namespace enertect.Core.Helpers
{
    public static class DataMappingExtensions
    {
        public static UpsItemViewModel ToItemViewModel(this UpsInformation item, BaseViewModel viewModel = null)
        {
            string StringName = item.StringName;
            if (item.Items != null  && item.Items.Any() && item.UpsName == StringName)
            {
                StringName = item.Items.Count().ToString();
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
                    subItemViewModel.DateOfTime = parsedDate.ToString("MM/dd/yyyy");
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

        public static OverviewItemViewModel ToOverviewModel(this OverView overView)
        {
            return new OverviewItemViewModel()
            {
                SiteName = $"Site: {overView.SiteName}",
                TotalUPS = $"{overView.TotalUPS} Ups",
                TotalStrings = $"{overView.TotalStrings} Strings",
                TotalBatteries = $"{overView.TotalBatteries} Batteries",
                SiteLocation = overView.SiteLocation,
                EnertectModel = overView.EnertectModel,
                SiteEmail = overView.SiteEmail,
                SiteContact = overView.SiteContact
            };
        }

        public static AlarmsOverViewModel ToAlarmsOverViewModel(this AlarmsOverView alarm)
        {
            return new AlarmsOverViewModel()
            {
                TotalAlarms = $"{alarm.TotalAlarms} - Alarms",
                OpenAlarms = alarm.OpenAlarms,
                CriticalAlarms = alarm.CriticalAlarms,
                Overviews = new ObservableCollection<AlarmOverViewItemModel>()
                {
                    new AlarmOverViewItemModel()
                    {
                        OverViewName = $"CriticalAlarms({alarm.CriticalAlarms})",
                        Value = alarm.CriticalAlarms,
                    },
                    new AlarmOverViewItemModel()
                    {
                        OverViewName = $"OpenAlarms({alarm.OpenAlarms})",
                        Value = alarm.OpenAlarms,
                    },
                }
            };
        }

        public static UpsReadingItemViewModel ToUpsReadingItemViewModel (this UpsReadingsList item)
        {
            return new UpsReadingItemViewModel()
            {
                UpsName = item.UpsName.Replace("\r\n", ""),
                UpsId = item.UpsId,
                UpsReadings = new ObservableCollection<ReadingItemViewModel>(item.UpsReadings.Select(v => v.ToReadingItemViewModel()))
            };
        }

        public static ReadingItemViewModel ToReadingItemViewModel(this Reading item)
        {
            return new ReadingItemViewModel()
            {
                UpsName = item.UpsName.Replace("\r\n", ""),
                StringName = item.StringName,
                StringVoltage = item.StringVoltage,
                StringIR = item.StringIR,
                StringTemperature = item.StringTemperature,
                AlarmsReadings = item.AlarmsReadings
            };
        }

        public static AlarmItemViewModel ToAlarmItemViewModel(this Alarm item, IList<UpsItemViewModel> ups)
        {
            if (ups != null)
            {

                UpsItemViewModel Up = ups.First(i => i.UpsId == item.Upsid);
                if (Up.StringName == null)
                {
                    Up.StringName = "";
                }
                return new AlarmItemViewModel()
                {
                    AlarmDate = DateTime.Parse(item.AlarmDate).ToString("dd-MM-yyyy hh:mm:ss"),
                    AlertType = item.AlertType,
                    AlertValue = item.AlertValue,
                    ResolveValue = item.TrueValue,
                    ActionTaken = String.IsNullOrEmpty(item.ActionTaken) ? "Update Action" : item.ActionTaken,
                    UpsName = Up.UpsName,
                    StringName = Up.StringName,
                    Status = item.ProblemResolved ? "Normal" : "Alarm",
                    Color = item.ProblemResolved ? "#869AA8" : "#E53E4E",
                    Brand = "Rocket",
                    ProblemResolvedDate = String.IsNullOrEmpty(item.ProblemResolvedDate) ? "" : DateTime.Parse(item.ProblemResolvedDate).ToString("dd-MM-yyyy hh:mm:ss"),
                    AlarmTime = String.IsNullOrEmpty(item.ProblemResolvedDate) ? "" : Utils.RelativeDate(DateTime.Parse(item.ProblemResolvedDate), DateTime.Parse(item.AlarmDate))
                };
            }
            else
            {
                return new AlarmItemViewModel()
                {
                    UpsName = item.UpsName.Replace("\r\n", ""),
                    AlertType = item.AlertType,
                    AlertValue = item.AlertValue,
                };
            }
        }

    }
}
