using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Canteen.Models;

namespace Canteen.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string UserId { get; set; }
        
        public System.DateTime? Date { get; set; }

        public decimal? Price { get; set; }

        [DisplayName("Dishes")]
        [Required(ErrorMessage = "Please, seelct at leat one dish")]
        [MinLength(1, ErrorMessage = "Please, seelct at leat one dish")]
        public long[] DishIds { get; set; }
    }
}