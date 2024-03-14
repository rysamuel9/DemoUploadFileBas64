using DemoUploadFileBas64.Data;
using DemoUploadFileBas64.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoUploadFileBas64.Services
{
    public interface IFileUploadService
    {
        Task<Guid> UploadFileAsync(string fileName, string fileType, int fileSize, byte[] fileData);
        Task<List<UploadedFile>> GetUploadedFilesAsync();
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly ApplicationDbContext _context;

        public FileUploadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<UploadedFile>> GetUploadedFiles()
        {
            var result = _context.UploadedFiles.ToListAsync();
            return result;
        }

        public async Task<Guid> UploadFileAsync(string fileName, string fileType, int fileSize, byte[] fileData)
        {
            try
            {
                var fileId = Guid.NewGuid();
                var uploadDate = DateTime.UtcNow;

                var file = new UploadedFile
                {
                    FileID = fileId,
                    FileName = fileName,
                    FileType = fileType,
                    FileSize = fileSize,
                    UploadDate = uploadDate,
                    FileData = fileData
                };

                _context.UploadedFiles.Add(file);
                await _context.SaveChangesAsync();

                return fileId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UploadedFile>> GetUploadedFilesAsync()
        {
            return await _context.UploadedFiles.ToListAsync();
        }
    }
}
