using Microsoft.AspNetCore.Http;
using SpaceCamp.Application.Photos;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhotoAsync(IFormFile file);
        Task<string> DeletePhotoAsync(string publicId);
    }
}
