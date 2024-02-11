using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class EmailUs
    {
        [Key]
        public int MailId { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Subject { get; set; }
        public string textarea { get; set; }
    }
}
