using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

            PhotoCommand=new Command( async ()=>{

            //do the get photo stuff
            Image stream = await DependencyService.Get<IPicturePicker>().GetPhotoStreamAsync();

                if (stream != null)
                {
                    Image image = new Image();

                    image = stream;

                    Image1 = image.Source;

                }


            });


        }


    private ImageSource _Image1;
    public ImageSource Image1
    {
        get
        {
            return _Image1;
        }
        set
        {
            _Image1 = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image1)));
        }
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

        public Command PhotoCommand { get; private set; }
        


    }

}
