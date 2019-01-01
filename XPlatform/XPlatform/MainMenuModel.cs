using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace XPlatform
{
    class MainMenuModel : INotifyPropertyChanged
    {

        public MainMenuModel()
        { 
            FirstCommand = new Command(() =>{ OnButtonClicked();

                

            });

        }


        public event EventHandler ButtonClicked;
        protected void OnButtonClicked()
        {
            ButtonClicked?.Invoke(this, new EventArgs());
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        private bool _isChecked;
        public bool isChecked { get{ return _isChecked; }set { _isChecked = value;OnPropertyChanged(nameof(isChecked)); } }

        private string _MainTitle;
        public string MainTitle { get { return _MainTitle; }set { _MainTitle = value; OnPropertyChanged(nameof(MainTitle)); } }

        public Command FirstCommand { get;private set; }



    }

}
