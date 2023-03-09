using Microsoft.AspNetCore.Http;
using SpaceCamp.Application.Interfaces;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        public Task<PhotoUploadResult> AddPhotoAsync(IFormFile photoFile)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> DeletePhotoAsync(string publicId)
        {
            throw new System.NotImplementedException();
        }
    }
}
