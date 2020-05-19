﻿using System;
using System.Collections.Generic;
using UXDivers.Grial;
using Xamarin.Forms;

namespace enertect.iOS
{
	internal class ThemeColors : ThemeColorsBase
	{
		private readonly static Dictionary<string, Color> _themeColors = new Dictionary<string, Color>
		{
			{ "BaseButtonBorderColor", Color.Transparent },
			{ "AccentColor", Color.FromHex("#FFF8036C") },
			{ "BaseTextColor", Color.FromHex("#666666") },
			{ "ListViewSelectedItemBackgroundColor", Color.FromHex("#11000000") },
			{ "SplashBackgroundColor", Color.FromHex("#4F29BC") },
			{ "BrandBlockTextColor", Color.FromHex("#FFFFFF") },
			{ "BrandBlockBackgroundColor", Color.Transparent },
			{ "NavigationBarStartBackgroundColor", Color.FromHex("#A32CA3") },
			{ "NavigationBarEndBackgroundColor", Color.FromHex("#4B29BD") },
			{ "NavigationBarTextColor", Color.FromHex("#FFFFFF") },
			{ "MainMenuOverlayBackgroundColor", Color.FromHex("#22F1F3F5") },
			{ "SecondaryPageBackgroundColor", Color.FromHex("#FFFFFF") },
			{ "SecondaryPageTextColor", Color.FromHex("#666666") },
			{ "DataGridHeaderRowBackgroundColor", Color.FromHex("#F6F7F9") },
			{ "DataGridHeaderTextColor", Color.FromHex("#546F7A") },
			{ "DataGridBackgroundColor", Color.White },
			{ "DataGridCellTextColor", Color.FromHex("#546F7A") },
			{ "DialogsShimBackgroundColor", Color.FromHex("#CC000000") },
			{ "CustomActionSheetShimBackgroundColor", Color.FromHex("#CC000000") },
			{ "CustomActionSheetBackgroundColor", Color.FromHex("#F1F3F5") },
			{ "CustomActionSheetTextColor", Color.FromHex("#666666") },
			{ "TagItemBackgroundColor", Color.FromHex("#4F29BC") },
			{ "TagItemTextColor", Color.White },
			{ "CircleActionButtonFlatBackgroundColor", Color.FromHex("#4F29BC") },
			{ "CircleActionButtonFlatBorderColor", Color.FromHex("#646ABD") },
			{ "CircleActionButtonFlatIconColor", Color.FromHex("#FFFFFF") },
			{ "CircleActionButtonFlatTextColor", Color.FromHex("#666666") },
			{ "InverseTextColor", Color.FromHex("#FFFFFF") },
			{ "BrandColor", Color.FromHex("#ad1457") },
			{ "BrandNameColor", Color.FromHex("#FFFFFF") },
			{ "BaseLightTextColor", Color.FromHex("#7b7b7b") },
			{ "OverImageTextColor", Color.FromHex("#FFFFFF") },
			{ "EcommercePromoTextColor", Color.FromHex("#FFFFFF") },
			{ "SocialHeaderTextColor", Color.FromHex("#666666") },
			{ "ArticleHeaderBackgroundColor", Color.FromHex("#F1F3F5") },
			{ "ListViewItemTextColor", Color.FromHex("#666666") },
			{ "AboutHeaderBackgroundColor", Color.FromHex("#FFFFFF") },
			{ "BasePageColor", Color.FromHex("#FFFFFF") },
			{ "BaseTabbedPageColor", Color.FromHex("#fafafa") },
			{ "MainWrapperBackgroundColor", Color.FromHex("#F5F6F8") },
			{ "CategoriesListIconColor", Color.FromHex("#55000000") },
			{ "DashboardIconColor", Color.FromHex("#FFFFFF") },
			{ "ComplementColor", Color.FromHex("#5D30F3") },
			{ "TranslucidBlack", Color.FromHex("#55000000") },
			{ "TranslucidWhite", Color.FromHex("#11ffffff") },
			{ "OkColor", Color.FromHex("#22C064") },
			{ "ErrorColor", Color.Red },
			{ "WarningColor", Color.FromHex("#ffc107") },
			{ "NotificationColor", Color.FromHex("#1274d1") },
			{ "DisabledColor", Color.FromHex("#bbbbbb") },
			{ "PrimaryActionButtonStartColor", Color.FromHex("#6233ff") },
			{ "PrimaryActionButtonEndColor", Color.FromHex("#FF0C69") },
			{ "SaveButtonColor", Color.FromHex("#22c064") },
			{ "DeleteButtonColor", Color.FromHex("#D50000") },
			{ "TranslucidButtonColor", Color.FromHex("#33FFFFFF") },
			{ "PlaceholderColor", Color.FromHex("#A7B6BF") },
			{ "EntryAndEditorsBackgroundColor", Color.FromHex("#FFFFFF") },
			{ "EntryAndEditorsBorderColor", Color.FromHex("#DEDEDE") },
			{ "RoundedLabelBackgroundColor", Color.FromHex("#525ABB") },
			{ "MainMenuHeaderBackgroundColor", Color.Transparent },
			{ "MainMenuBackgroundColor", Color.Transparent },
			{ "MainMenuSeparatorColor", Color.Transparent },
			{ "MainMenuTextColor", Color.FromHex("#FFFFFF") },
			{ "MainMenuIconColor", Color.FromHex("#FFFFFF") },
			{ "ListViewSeparatorColor", Color.FromHex("#D3D3D3") },
			{ "BaseSeparatorColor", Color.FromHex("#7b7b7b") },
			{ "ChatRightBalloonBackgroundColor", Color.FromHex("#525ABB") },
			{ "ChatBalloonFooterTextColor", Color.FromHex("#FFFFFF") },
			{ "ChatRightTextColor", Color.FromHex("#FFFFFF") },
			{ "ChatLeftTextColor", Color.FromHex("#FFFFFF") },
			{ "RatingsColor", Color.FromHex("#ffb300") },
			{ "AndroidTabStripBackgroundColor", Color.FromHex("#11000000") },
			{ "AndroidTabItemDefaultTextColor", Color.FromHex("#77FFFFFF") },
			{ "AndroidTabItemSelectedTextColor", Color.FromHex("#FFFFFF") },
			{ "AndroidSelectedBackgroasundColor", Color.FromHex("#FF177A") },
			{ "SimpleTabDefaultStateTextColor", Color.FromHex("#A7A7A7") }
		};
		public ThemeColors() : base(_themeColors) { }
	}
}