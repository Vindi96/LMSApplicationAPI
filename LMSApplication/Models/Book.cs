using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApplication.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookPhoto { get; set; }
        public string SectionName { get; set; }
        public int BookCount { get; set; }

        public string BookAuthor { get; set; }
    }
}
