using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RentalMoviesApp.Dtos;
using RentalMoviesApp.Models;

namespace RentalMoviesApp.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {


            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("Invalid customer Id");

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();
            customer.Movie = new List<Movie>();
            foreach (var movie in movies)
            {
                Console.WriteLine("Went here");
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");


                customer.Movie.Add(movie);

                movie.NumberAvailable--;
                movie.NumberInStock--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
                _context.SaveChanges();
            }

           

            return Ok();
        }

    }
}


