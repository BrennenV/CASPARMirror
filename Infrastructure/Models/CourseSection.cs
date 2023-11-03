using Microsoft.AspNetCore.Mvc;
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

        public string? BannerCRN { get; set; }

        public string? SectionNotes { get; set; }

        public DateTime? SectionFirstDayEnrollment { get; set; }

        public DateTime? SectionFinalEnrollment { get; set; }

        public DateTime? SectionUpdated { get; set; }

        public DateTime? SectionBannerUpdated { get; set; }

        public bool? IsArchived { get; set; }

        [DisplayName("Course")]
        public int CourseId { get; set; }

        [DisplayName("Semester Instance")]
        public int SemesterInstanceId { get; set; }

        [DisplayName("Instructor")]
        public int? InstructorId { get; set; }

        [DisplayName("Modality")]
        public int? ModalityId { get; set; }

        [DisplayName("Classroom")]
        public int? ClassroomId { get; set; }

        [DisplayName("Time Block")]
        public int? TimeBlockId { get; set; }

        [DisplayName("Days of Week")]
        public int? DaysOfWeekId { get; set; }

        [DisplayName("Part of Term")]
        public int? PartOfTermId { get; set; }

        [DisplayName("Pay Model")]
        public int? PayModelId { get; set; }

        [DisplayName("Pay Organization")]
        public int? PayOrganizationId { get; set; }

        [DisplayName("Section Status")]
        public int? SectionStatusId { get; set; }

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
