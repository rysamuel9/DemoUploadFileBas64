using DemoUploadFileBas64.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoUploadFileBas64.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
