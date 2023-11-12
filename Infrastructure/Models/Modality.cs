using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
	public class Modality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Modality Name")]
        public string? ModalityName { get; set; }

        public bool? IsArchived { get; set; }
    }
}
