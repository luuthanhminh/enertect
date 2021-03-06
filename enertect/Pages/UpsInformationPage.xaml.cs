﻿using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    [MvxContentPagePresentation()]
    public partial class UpsInformationPage : BasePage<UpsInformationViewModel>
    {
        public UpsInformationPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }
    }
}
