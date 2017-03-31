using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ZenithWebSite.Models
{
    [ModelMetadataType(typeof(ActivityMetaData))]
    public partial class Activity { }

    public class ActivityMetaData
    {
        [Display(Name = "Activity")]
        public object ActivityId { get; set; }

        [Display(Name = "Activity Description")]
        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public object ActivityDescription { get; set; }

        [Display(Name = "Created")]
        public object CreationDate { get; set; }

        public object Events { get; set; }
    }
}
