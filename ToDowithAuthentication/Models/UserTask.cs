using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDowithAuthentication.Models
{
    public class UserTask
    {
        public int id { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public bool? isComplete { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.Now;
        public DateTime? DateCompleted { get; set; }

        /*--*/

        public string userId { get; set; }

        [ForeignKey("userId")]
        public ApplicationUser User { get; set; }
    }
}