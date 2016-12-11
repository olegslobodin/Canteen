using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Canteen.Models
{
    public partial class Order
    {
        public virtual ICollection<Dish> Dishes
        {
            get
            {
                return Orders_Dishes.Select(od => od.Dish).ToList();
            }
        }
    }
}