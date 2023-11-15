//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Infrastructure.Models;

//namespace Infrastructure.ArchivedModels
//{
//    public class PreferenceListDetail
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        [DisplayName("Preference Rank")]
//        public int PreferenceRank { get; set; }

//        public bool? IsArchived { get; set; }

//        [Required]
//        [DisplayName("Preference List")]
//        public int PreferenceListId { get; set; }

//        [Required]
//        [DisplayName("Course")]
//        public int CourseId { get; set; }

//        [ForeignKey("PreferenceListId")]
//        public PreferenceList? PreferenceList { get; set; }

//        [ForeignKey("CourseId")]
//        public Course? Course { get; set; }
//    }
//}
