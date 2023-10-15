using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgrammersBlog.Entities.ComplexTypes
{
    public enum OrderBy
    {
        [Display(Name = "Date")]
        Date =0,
        [Display(Name = "ViewCount")]
        ViewCount =1,
        [Display(Name = "CommentCount")]
        CommentCount =2,
    }
}
