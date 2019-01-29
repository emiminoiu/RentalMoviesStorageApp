using RentalMoviesApp.Dtos;
using RentalMoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
namespace RentalMoviesApp.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Rentals
        public ActionResult New()
        {
            return View();
        }

        public ActionResult ReturnRental()
        {
            return View();
        }

        public ActionResult FindMovies()
        {
            var movie = _context.Movies.ToList();
            ViewBag.MyProperty = movie;
            return View();
        
        }
    
        public ActionResult FindMyMovies(string name)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Name.Equals(name));
              
            var viewModel = new List<Customer>();
            
            var allCustomers = _context.Customers;
            
            foreach (var customer in allCustomers)
            {
             
                if (customer.Movie != null && customer.Movie.Contains(movie))
                {
                    viewModel.Add(customer);
                }
            }
            return View(viewModel);
        
        }
      
        [HttpPost]
        public ActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            if (customer == null)
                return RedirectToAction("New", "Rentals");

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();
            
           
            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return RedirectToAction("New", "Rentals");
               
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return RedirectToAction("New", "Rentals");
        }

    }
}