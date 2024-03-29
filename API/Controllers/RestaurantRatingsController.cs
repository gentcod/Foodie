using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
   public class RestaurantRatingsController : BaseApiController
    {
        private readonly FoodieContext _context;
        public RestaurantRatingsController(FoodieContext context)
        {
         _context = context;
            
        }

        [HttpGet(Name = "restaurantRating")]
        public async Task<ActionResult<RestaurantRatingsDto>> GetRestaurantRatings()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            var restaurantsRatings = await _context.RestaurantRatings.ToListAsync();

            var restaurantsRatingsDto = restaurantsRatings.MapRestaurantsRatingsToDto(restaurants);

            return Ok(restaurantsRatingsDto);
        }

        [HttpGet(":id")]
        public async Task<ActionResult<RestaurantRatingsDto>> GetRestaurantRatingsById([BindRequired][FromQuery]int restaurantId)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(el => el.Id == restaurantId);
            if (restaurant == null) return BadRequest(new ProblemDetails{ Title = "Restaurant not found"});

            var restaurantRatings = await _context.RestaurantRatings.Where(el => el.RestaurantId == restaurantId).ToListAsync();

            var restaurantRatingsDto = restaurantRatings.MapRestaurantRatingsToDto(restaurant);

            return Ok(restaurantRatingsDto);
        }

        [Authorize]
        [HttpPost("AddRating")]
        public async Task<ActionResult<Restaurant>> AddRating(RatingDto ratingDto, [BindRequired][FromQuery]int restaurantId)
        {
            if (ratingDto.RatingNum < 1 || ratingDto.RatingNum > 5) return BadRequest(new ProblemDetails { Title = "Rating number is out of rating" });

            var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null) return BadRequest(new ProblemDetails { Title = "Restaurant not found" });

            var restaurantRatings = await _context.RestaurantRatings.Where(el => el.RestaurantId == restaurantId).ToListAsync();
            restaurantRatings ??= new List<RestaurantRating>();

            restaurantRatings.Add(new RestaurantRating
            {
                RatingNum = ratingDto.RatingNum,
                Comment = ratingDto.Comment,
            });

            restaurant.Rating = Math.Round(restaurantRatings.Average(rating => rating.RatingNum), 1);
            restaurant.RestaurantRatings = restaurantRatings;

            _context.Restaurants.Update(restaurant);
            var result = _context.SaveChangesAsync();
            if (result != null) return CreatedAtRoute("restaurantRating", restaurantRatings.MapRestaurantRatingsToDto(restaurant));

            return BadRequest(new ProblemDetails { Title = "Problem adding Restaurant Rating" });
        }
    }
}