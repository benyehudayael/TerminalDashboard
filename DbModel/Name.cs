using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDashboard.DbModel
{
    public class Name
    {
        [Key]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
