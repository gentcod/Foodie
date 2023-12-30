using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.RequestHelpers
{
    public class FavoriteRestaurantParams
    {
        [BindRequired]
        public string UserId { get; set; }
        [BindRequired]
        public int RestaurantId { get; set; }
    }
}