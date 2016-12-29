using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FloatActionButtonXF
{
	public partial class FloatActionButtonXFPage : ContentPage
	{
		public FloatActionButtonXFPage()
		{
			InitializeComponent();

			deleteCard.IsVisible = false;

			string senderName = "";

			for (int i = 0; i < 20; i++)
			{
				StackLayout stack = new StackLayout();
				stack.VerticalOptions = LayoutOptions.FillAndExpand;
				stack.HorizontalOptions = LayoutOptions.FillAndExpand;
				stack.BackgroundColor = Color.FromHex("#D8D8D8");
				stack.Padding = new Thickness(0, 0, 0, 1);

				Grid gridCard = new Grid();

				//this gesture will control only the card selected,
				//but, we need to set all the cards as available before...
				var cardGestureRecognizer = new TapGestureRecognizer();
				cardGestureRecognizer.Tapped += (s, e) =>
				{
					//then we got all the itens in the list...
					var p = cardListContainer.Children;

					//we got the sender object...
					var g = s as Grid;

					var l = g.Children[0] as Label;

					string[] aL = l.Text.Split('|');

					if (aL[1] == "0")
					{
						l.Text = string.Format("{0}|{1}", aL[0], 1);
						g.BackgroundColor = Color.FromHex("#bdbdbd");
						showDeleteIcon(true);
						senderName = aL[0];
					}
					else if (aL[1] == "1")
					{
						l.Text = string.Format("{0}|{1}", aL[0], 0);
						g.BackgroundColor = Color.White;
						showDeleteIcon(false);
						senderName = "";
					}

					//we check all the itens from the list...
					for (int y = 0; y < p.Count; y++)
					{
						//and then, we set all the itens to white...

						var _s = p[y] as StackLayout;

						var _g = _s.Children[0] as Grid;

						var _l = _g.Children[0] as Label;

						string[] _aL = _l.Text.Split('|');

						if (_aL[0] != senderName)
						{
							_l.Text = string.Format("{0}|{1}", _aL[0], 0);
							_g.BackgroundColor = Color.White;
						}
					}



				};

				gridCard.GestureRecognizers.Add(cardGestureRecognizer);

				//gridCard.RowSpacing = 10;
				//gridCard.ColumnSpacing = 10;
				gridCard.BackgroundColor = Color.White;

				gridCard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				gridCard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

				gridCard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				gridCard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

				Label descricao = new Label();
				descricao.Text = string.Format("{0} {1}", "Item", i);
				descricao.FontSize = 14;
				descricao.VerticalOptions = LayoutOptions.CenterAndExpand;
				descricao.HorizontalOptions = LayoutOptions.StartAndExpand;
				descricao.FontAttributes = FontAttributes.Bold;
				descricao.TextColor = Color.Black;
				descricao.Margin = new Thickness(20, 10, 0, -5);

				Label numero = new Label();
				numero.Text = string.Format("{0} {1}", "0000.0000.0000.000", i);
				numero.FontSize = 12;
				numero.VerticalOptions = LayoutOptions.CenterAndExpand;
				numero.HorizontalOptions = LayoutOptions.StartAndExpand;
				numero.FontAttributes = FontAttributes.Bold;
				numero.TextColor = Color.Black;
				numero.Margin = new Thickness(20, 0, 0, 0);

				Image imagem = new Image();
				imagem.VerticalOptions = LayoutOptions.CenterAndExpand;
				imagem.HorizontalOptions = LayoutOptions.End;
				imagem.Source = ImageSource.FromFile("ic_shopping_cart_black_48dp.png");
				imagem.WidthRequest = 25;
				imagem.Margin = new Thickness(0, 0, 25, 0);

				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "clickBuyCredit");
				imagem.GestureRecognizers.Add(tapGestureRecognizer);

				Label isSelected = new Label();
				isSelected.Text = string.Format("{0}|{1}", descricao.Text, 0);
				isSelected.IsVisible = false;

				gridCard.Children.Add(isSelected, 0, 0);
				gridCard.Children.Add(descricao, 0, 0);
				gridCard.Children.Add(numero, 0, 1);
				gridCard.Children.Add(imagem, 1, 0);
				Grid.SetRowSpan(imagem, 2);

				stack.Children.Add(gridCard);

				cardListContainer.Children.Add(stack);
			}

			var screenGestureRecognizer = new TapGestureRecognizer();
			screenGestureRecognizer.Tapped += (s, e) =>
			{
				showDeleteIcon(false);
			};
			scrollView.GestureRecognizers.Add(screenGestureRecognizer);
		}


		public void showDeleteIcon(bool show)
		{
			if (show)
			{
				deleteCard.IsVisible = true;
				deleteCard.FadeTo(1, 1000);
			}
			else
			{
				deleteCard.IsVisible = false;
				deleteCard.FadeTo(0, 5000);
			}
		}
	}
}