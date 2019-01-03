using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XPlatform
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
        Task<Image> GetPhotoStreamAsync();
    }

}