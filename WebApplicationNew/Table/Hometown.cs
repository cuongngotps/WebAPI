using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationNew.Table
{
    public class Hometown
    {
        [Key]
        public string ID { get; set; }


        public string Code { get; set; }

        public string Name { get; set; }
    }
}