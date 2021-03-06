﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XPlatform
{


	public partial class MainPage : ContentPage
	{
        private int ClickCount = 0;
        private MainMenuModel _VM; 

		public MainPage()
		{
			InitializeComponent();

            _VM = new MainMenuModel();

            BindingContext = _VM;

            _VM.MainTitle = "t";

            _VM.ButtonClicked += ((s1,e1)=> { OnButtonClicked(s1, e1); } );

        }

        public void OnButtonClicked(object sender, EventArgs args)
        {
            //Button button = (Button)sender;

            ClickCount++;

            DisplayAlert("Alert", "Clicks " + ClickCount.ToString(), "O.K.");

            _VM.MainTitle = "Main Title " + ClickCount.ToString();

            Navigation.PushModalAsync(new PhotoBuilder());

        }

        private void ContentPage_SizeChanged(object sender, EventArgs e)
        {


            string visualState = Width > Height ? "Landscape" : "Portrait";
            VisualStateManager.GoToState(mainStack, visualState);
            //VisualStateManager.GoToState(menuScroll, visualState);
            //VisualStateManager.GoToState(menuStack, visualState);

            //foreach (View child in menuStack.Children)
            //{
            //    VisualStateManager.GoToState(child, visualState);
            //}


        }
    }
}
