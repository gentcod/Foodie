using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
   public class RestaurantRef
   {
      public int Id { get; set; }

      public int RestaurantId { get; set; }
      public string RecipeName { get; set; } = new Restaurant().Name;
   }
}