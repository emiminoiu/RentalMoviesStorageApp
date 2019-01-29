using RentalMoviesApp.Dtos;
using RentalMoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalMoviesApp.Controllers.Api
{
    public class ReturnRentalController : ApiController
    {
        private ApplicationDbContext _context;

        public ReturnRentalController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult ReturnRental(NewRentalDto newRental)
        {

            
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);
             
            if (customer == null)
                return BadRequest("Invalid customer Id");

              var movies = _context.Movies.Where(
             m => newRental.MovieIds.Contains(m.Id)).Where(c => newRental.CustomerId.Equals(customer.Id)).ToList();
          
            if (movies == null)
                return BadRequest("This customer has't rented any movies.");
            foreach (var movie in movies)
            {
                Console.WriteLine("Went here");
                customer.Movie.Remove(movie);

                movie.NumberAvailable++;
                movie.NumberInStock++;
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

