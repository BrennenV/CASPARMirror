using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PreferenceListDetailModality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Preference List Detail")]
        public int PreferenceListDetailId { get; set; }

        [Required]
        [DisplayName("Modality")]
        public int ModalityId { get; set; }

        [DisplayName("Days of the Week")]
        public int? DaysOfWeekId { get; set; }

        [DisplayName("Time Block")]
        public int? TimeBlockId { get; set; }

        [DisplayName("Campus")]
        public int? CampusId { get; set; }

        [ForeignKey("PreferenceListDetailId")]
        public PreferenceListDetail? PreferenceListDetail { get; set; }

        [ForeignKey("ModalityId")]
        public Modality? Modality { get; set; }

        [ForeignKey("DaysOfWeekId")]
        public DaysOfWeek? DaysOfWeek { get; set; }

        [ForeignKey("TimeBlockId")]
        public TimeBlock? TimeBlock { get; set; }

        [ForeignKey("CampusId")]
        public Campus? Campus { get; set; }
    }
}
