using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiServer.Models
{
    [Table("Hocr")]
    public class HocrDto
    {
        [Key]
        public int HocrId { get; set; }

        public string Id { get; set; }

        public string Class { get; set; }

        public string Title { get; set; }

        public string Lang { get; set; }

        public string FullValue { get; set; }

        public HocrDocumentDto HocrDocument { get; set; }
    }
}