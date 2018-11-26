using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevSlop.Slop.Data.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Display(Name = "What")]
        [DataType(DataType.Text)]
        public string What { get; set; }


        [Display(Name = "What URL")]
        [DataType(DataType.Url)]
        public string WhatUrl { get; set; }


        [Display(Name = "When")]
        [DataType(DataType.Date)]
        public DateTime When { get; set; }


        [Display(Name = "When URL")]
        [DataType(DataType.Url)]
        public string WhenUrl { get; set; }

        [Display(Name = "Where")]
        [DataType(DataType.Text)]
        public string Where { get; set; }
                     
        [Display(Name = "Where URL")]
        [DataType(DataType.Url)]
        public string WhereUrl { get; set; }


        [Display(Name = "Who")]
        [DataType(DataType.Text)]
        public string Who { get; set; }

        [Display(Name = "Who URL")]
        [DataType(DataType.Url)]
        public string WhoUrl { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
