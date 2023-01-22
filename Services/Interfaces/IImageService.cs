using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Threading.Tasks;

namespace ReelRoster.Services.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> EncodeImageAsync(IFormFile poster);
        Task<byte[]> EncodeImageURLAsync(string imageURL);
        string DecodeImage(byte[] poster, string ContentType);

    }
}
