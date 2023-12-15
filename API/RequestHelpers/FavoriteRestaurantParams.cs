using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.RequestHelpers
{
    public class FavoriteRestaurantParams
    {
        public string UserId { get; set; }
        [BindRequired]
        public int RestaurantId { get; set; }
    }
}