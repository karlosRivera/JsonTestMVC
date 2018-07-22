using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JsonTestMVC.Models
{
    public class Customer
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Mail to me")]
        public bool SelfSend { get; set; }

        [Required]
        [Display(Name = "3rd party")]
        public bool thirdParty { get; set; }

        [Required]
        [Display(Name = "Others")]
        public bool Others { get; set; }
    }

    public class Customer1
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public List<Options> option { get; set; }

    }

    public class Options
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}