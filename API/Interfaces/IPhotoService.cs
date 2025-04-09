using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {

        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        /*
        Task<ImageUploadResult> SetMainPhotoAsync(string publicId);
        Task<ImageUploadResult> GetPhotoAsync(string publicId);
        Task<IEnumerable<ImageUploadResult>> GetAllPhotosAsync();
        */
    }
}
