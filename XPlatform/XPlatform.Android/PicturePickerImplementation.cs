using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using DependencyServiceSample.Droid;
using XPlatform.Droid;
using XPlatform;
using Android.Provider;

[assembly: Dependency(typeof(PicturePickerImplementation))]

namespace DependencyServiceSample.Droid
{
    public class PicturePickerImplementation : IPicturePicker 
    {

        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = Forms.Context as MainActivity;

            //MainActivity activity = (MainActivity)Android.App.Application.Context;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return activity.PickImageTaskCompletionSource.Task;
        }


        Task<Image> IPicturePicker.GetPhotoStreamAsync()
        {
            // Define the Intent for getting images
            //Intent intent = new Intent();
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            // Get the MainActivity instance
            MainActivity activity = Forms.Context as MainActivity;

            //MainActivity activity = (MainActivity)Android.App.Application.Context;

            activity.StartActivityForResult(intent, MainActivity.PickPhotoId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickPhotoTaskCompletionSource = new TaskCompletionSource<Image>();

            // Return Task object
            return activity.PickPhotoTaskCompletionSource.Task;
        }
    }
}