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



            pickPictureButton1.Clicked += async (sender, e) =>
            {
                pickPictureButton1.IsEnabled = false;
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
                        _vm.Image1 = image.Source;
                        pickPictureButton1.IsEnabled = true;
                    };
                    image.GestureRecognizers.Add(recognizer);

                    //(this as ContentPage).Content = image;
                    _vm.Image1 = image.Source;
                }
                else
                {
                    pickPictureButton1.IsEnabled = true;
                }
            };


            pickPictureButton2.Clicked += async (sender, e) =>
            {
                pickPictureButton2.IsEnabled = false;
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
                        _vm.Image2 = image.Source;
                        pickPictureButton2.IsEnabled = true;
                    };
                    image.GestureRecognizers.Add(recognizer);

                    //(this as ContentPage).Content = image;
                    _vm.Image2 = image.Source;
                }
                else
                {
                    pickPictureButton2.IsEnabled = true;
                }
            };

            pickPictureButton3.Clicked += async (sender, e) =>
            {
                pickPictureButton3.IsEnabled = false;
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
                        _vm.Image3 = image.Source;
                        pickPictureButton3.IsEnabled = true;
                    };
                    image.GestureRecognizers.Add(recognizer);

                    //(this as ContentPage).Content = image;
                    _vm.Image3 = image.Source;
                }
                else
                {
                    pickPictureButton3.IsEnabled = true;
                }
            };


        }



    }
}