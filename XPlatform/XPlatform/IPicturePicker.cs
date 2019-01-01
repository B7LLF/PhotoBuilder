using System.IO;
using System.Threading.Tasks;

namespace XPlatform
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}