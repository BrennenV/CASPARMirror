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
    public class CourseSection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? BannerCRN { get; set; }

        [Required]
        public string? SectionNotes { get; set; }

        [Required]
        public int SectionFirstDayEnrollment { get; set; }

        [Required]
        public int SectionFinalEnrollment { get; set; }

        [Required]
        public DateTime? SectionUpdated { get; set; }

        [Required]
        public DateTime? SectionBannerUpdated { get; set; }

        [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Semester Instance")]
        public int SemesterInstanceId { get; set; }

        [Required]
        [DisplayName("Instructor")]
        public int InstructorId { get; set; }

        [Required]
        [DisplayName("Modality")]
        public int ModalityId { get; set; }

        [Required]
        [DisplayName("Classroom")]
        public int ClassroomId { get; set; }

        [Required]
        [DisplayName("Time Block")]
        public int TimeBlockId { get; set; }

        [Required]
        [DisplayName("Days of Week")]
        public int DaysOfWeekId { get; set; }

        [Required]
        [DisplayName("Part of Term")]
        public int PartOfTermId { get; set; }

        [Required]
        [DisplayName("Pay Model")]
        public int PayModelId { get; set; }

        [Required]
        [DisplayName("Pay Organization")]
        public int PayOrganizationId { get; set; }

        [Required]
        [DisplayName("Section Status")]
        public int SectionStatusId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [ForeignKey("SemesterInstanceId")]
        public SemesterInstance? SemesterInstance { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor? Instructor { get; set; }

        [ForeignKey("ModalityId")]
        public Modality? Modality { get; set; }

        [ForeignKey("ClassroomId")]
        public Classroom? Classroom { get; set; }

        [ForeignKey("TimeBlockId")]
        public TimeBlock? TimeBlock { get; set; }

        [ForeignKey("DaysOfWeekId")]
        public DaysOfWeek? DaysOfWeek { get; set; }

        [ForeignKey("PartOfTermId")]
        public PartOfTerm? PartOfTerm { get; set; }

        [ForeignKey("PayModelId")]
        public PayModel? PayModel { get; set; }

        [ForeignKey("PayOrganizationId")]
        public PayOrganization? PayOrganization { get; set; }

        [ForeignKey("SectionStatusId")]
        public SectionStatus? SectionStatus { get; set; }
    }
}
