using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiServer.Models
{
    [Table("HocrDocument")]
    public class HocrDocumentDto
    {
        [Key]
        public int HocrDocumentId { get; set; }

        public string DocumentName { get; set; }

        public string Value { get; set; }
        
        public List<HocrDto> Hocrs { get; set; }
    }
}