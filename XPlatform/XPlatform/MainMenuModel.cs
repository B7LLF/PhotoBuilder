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
            FirstCommand = new Command(() =>
            {
                OnButtonClicked();
            });

            PhotoCommand=new Command( async ()=>{

                TakePhotoEnabled = false;

                //do the get photo stuff
                Image returnedImage = await DependencyService.Get<IPicturePicker>().GetPhotoStreamAsync();

                if (returnedImage != null)
                {
                    Image image = new Image();

                    image = returnedImage;

                    Image1 = image.Source;

                }

                TakePhotoEnabled = true;

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

        private bool _TakePhotoEnabled =true;
        public bool TakePhotoEnabled { get { return _TakePhotoEnabled; } set { _TakePhotoEnabled = value; OnPropertyChanged(nameof(TakePhotoEnabled)); } }


    }

}
