using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class FilterViewModel
    {
        public string Search { get; set; }
        public string Filter { get; set; }
    }

    public class DataViewModel
    {
        public string albums { get; set; }
        public string users { get; set; }
        public string comments { get; set; }
        public string latest { get; set; }
        public string photos { get; set; }
    }
}