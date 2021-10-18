using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ImageSharingWithUpload.Models
{
    public class Image
    {
        [Required(ErrorMessage = "Id is required")]
        [RegularExpression(@"[a-zA-Z0-9_]+", ErrorMessage = "Only letters and numbers allowed for Id")]
        public String Id { get; set; }

        [Required(ErrorMessage = "Caption is required")]
        [StringLength(40)]
        public String Caption { get; set; }

        [StringLength(200)]
        public String Description { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date given")]
        public DateTime? DateTaken { get; set; }

        public String Userid { get; set; }

        public Image()
        {
        }
    }
}