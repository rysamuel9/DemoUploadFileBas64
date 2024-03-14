using System.ComponentModel.DataAnnotations;

namespace DemoUploadFileBas64.Models
{
    public class UploadedFile
    {
        [Key]
        public Guid FileID { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public int FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] FileData { get; set; }
    }
}
