using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XPlatform
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class PhotoBuilderDataModel : INotifyPropertyChanged
    {
        
        public PhotoBuilderDataModel()
        {
            FirstCommand = new Command(() => {
                BackCommand?.Invoke(this, new EventArgs());
                
            });
           
            
        }
        public event EventHandler BackCommand;
        public Command FirstCommand { get; private set; }

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

        private ImageSource _Image2;
        public ImageSource Image2
        {
            get
            {
                return _Image2;
            }
            set
            {
                _Image2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image2)));
            }
        }

        private ImageSource _Image3;
        public ImageSource Image3
        {
            get
            {
                return _Image3;
            }
            set
            {
                _Image3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image3)));
            }
        }

        public Command GetImage1
        {
            get
            {
                return new Command(param => GetImage(1)); 
            }
        }

        public Command GetImage2
        {
            get
            {
                return new Command(param => GetImage(2));
            }
        }

        public Command GetImage3
        {
            get
            {
                return new Command(param => GetImage(3));
            }
        }


        public Command GetImagePreview
        {
            get
            {
                return new Command(() => 
                {
                    //Greate a new image and add the other images if they exist.
                    Image newimage = new Image();
                    

                });
            }
        }



        async void GetImage(int imageIndex)
        {

            switch (imageIndex)
            {
                case 1:
                    pickPictureButton1Enabled = false;
                    break;

                case 2:
                    pickPictureButton2Enabled = false;
                    break;

                case 3:
                    pickPictureButton3Enabled = false;
                    break;

                default:
                    break;
            }

            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                Image image = new Image
                {
                    Source = ImageSource.FromStream(() => stream),
                    BackgroundColor = Color.Gray
                };

                TapGestureRecognizer recognizer = new TapGestureRecognizer();
                recognizer.Tapped += (sender2, args) =>
                {
                    //(this as ContentPage).Content = image;
                    switch (imageIndex)
                    {
                        case 1:
                            Image1 = image.Source;
                            pickPictureButton1Enabled = true;
                            break;

                        case 2:
                            Image2 = image.Source;
                            pickPictureButton2Enabled = true;
                            break;

                        case 3:
                            Image3 = image.Source;
                            pickPictureButton3Enabled = true;
                            break;

                        default:
                            break;
                    }

                };


                image.GestureRecognizers.Add(recognizer);

                //(this as ContentPage).Content = image;
                
                switch (imageIndex)
                {
                    case 1:
                        Image1 = image.Source;
                        pickPictureButton1Enabled = true;
                        break;

                    case 2:
                        Image2 = image.Source;
                        pickPictureButton2Enabled = true;
                        break;

                    case 3:
                        Image3 = image.Source;
                        pickPictureButton3Enabled = true;
                        break;

                    default:
                        break;
                }


            }
            else
            {
                switch (imageIndex)
                {
                    case 1:
                        pickPictureButton1Enabled = true;
                        break;

                    case 2:
                        pickPictureButton2Enabled = true;
                        break;

                    case 3:
                        pickPictureButton3Enabled = true;
                        break;

                    default:
                        break;
                }
            }

        }

        public bool _pickPictureButton1Enabled =true;
        public bool pickPictureButton1Enabled { get { return _pickPictureButton1Enabled; } set { _pickPictureButton1Enabled = value;PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(pickPictureButton1Enabled))); } }

        public bool _pickPictureButton2Enabled = true;
        public bool pickPictureButton2Enabled { get { return _pickPictureButton2Enabled; } set { _pickPictureButton2Enabled = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(pickPictureButton2Enabled))); } }

        public bool _pickPictureButton3Enabled = true;
        public bool pickPictureButton3Enabled { get { return _pickPictureButton3Enabled; } set { _pickPictureButton3Enabled = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(pickPictureButton3Enabled))); } }


        public event PropertyChangedEventHandler PropertyChanged;
    }



    public partial class PhotoBuilder : ContentPage
	{
        PhotoBuilderDataModel _vm;
        public PhotoBuilder ()
		{
			InitializeComponent ();
            _vm = (PhotoBuilderDataModel)this.BindingContext;
            _vm.BackCommand += (s1,e1) => { Navigation.PopModalAsync(); };

        }
    }
}