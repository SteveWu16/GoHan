using System;
using System.ComponentModel.DataAnnotations;

namespace GoHan.Models
{
    public class DBInitialModel
    {
        [Key]
        public int id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
