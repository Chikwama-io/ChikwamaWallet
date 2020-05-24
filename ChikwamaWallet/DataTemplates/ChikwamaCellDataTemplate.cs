using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChikwamaWallet.DataTemplates
{
	class ChikwamaCellDataTemplate : ViewCell
	{
		public ChikwamaCellDataTemplate()
		{
	

			var TokenNameLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 16,
				TextColor = Color.Black
			};
			// Apply PlatformEffects to our TrailNameLabel Control
			TokenNameLabel.Effects.Add(Effect.Resolve("com.geniesoftstudios.LabelShadowEffect"));
			TokenNameLabel.SetBinding(Label.TextProperty, "TokenName");

			var TokenSymbolLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 12,
				TextColor = Color.FromHex("#666")
			};
			TokenSymbolLabel.SetBinding(Label.TextProperty, "TokenSymbol");

			var TokenBalanceLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 12,
				TextColor = Color.Black
			};
			TokenBalanceLabel.SetBinding(Label.TextProperty, "TokenBalance");

			var TokenAddressLabel = new Label()
			{
				FontSize = 12,
				TextColor = Color.Black
			};
			TokenAddressLabel.SetBinding(Label.TextProperty, "tokenAddress");

	
			var statusLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children = { TokenSymbolLabel,TokenBalanceLabel}
			};

			var DetailsLayout = new StackLayout
			{
				Padding = new Thickness(10, 0, 0, 0),
				Spacing = 0,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { TokenAddressLabel}
			};

			var cellLayout = new StackLayout
			{
				Spacing = 0,
				Padding = new Thickness(10, 5, 10, 5),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { statusLayout, DetailsLayout }
			};

			this.View = cellLayout;
		}
	}
	}
