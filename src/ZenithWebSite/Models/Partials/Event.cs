using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebSite.Models
{
    [ModelMetadataType(typeof(EventMetaData))]

    public partial class Event : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((DateTime)ToTime < (DateTime)FromTime)
            {
                yield return
                  new ValidationResult(errorMessage: "End Date must be greater than Start Date",
                                       memberNames: new[] { "FromTime" });
            }
        }
    }

    public class EventMetaData
    {
        [Display(Name = "Event Id")]
        public object EventId { get; set; }

        [Display(Name = "Start")]
        public object FromTime { get; set; }

        [Display(Name = "End")]
        public object ToTime { get; set; }

        [Display(Name = "Entered By")]
        public object EnteredBy { get; set; }

        [Display(Name = "Created")]
        public object CreationDate { get; set; }

        [Display(Name = "Active")]
        public object IsActive { get; set; }

        [Display(Name = "Activity")]
        public object ActivityId { get; set; }
        [Display(Name = "Activity")]
        public object Activity { get; set; }
    }
}
