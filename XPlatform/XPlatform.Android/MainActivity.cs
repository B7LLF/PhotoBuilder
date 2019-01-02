using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using Android.Graphics;
using Java.IO;
using Xamarin.Forms;
using Android.Graphics.Drawables;

namespace XPlatform.Droid
{
    [Activity(Label = "XPlatform", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            
        }


        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;
        public static readonly int PickPhotoId = 1001;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        public TaskCompletionSource<Image> PickPhotoTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }

            if (requestCode == PickPhotoId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    //Android.Net.Uri uri = intent.Data;
                    //Stream stream = ContentResolver.OpenInputStream(uri);

                    var extras = intent.Extras.Get("data");
                    //Bitmap imageBitmap = (Bitmap)extras;
                    Bitmap drawable = (Bitmap)intent.Extras.Get("data");

                    MemoryStream stream = new MemoryStream();
                    drawable.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);

                    var image = new Image();
                    image.Source = ImageSource.FromStream(() => new MemoryStream(stream.ToArray()));

                    // Set the Stream as the completion of the Task
                    PickPhotoTaskCompletionSource.SetResult(image);
                }
                else
                {
                    PickPhotoTaskCompletionSource.SetResult(null);
                }
            }


        }


    }
}

