﻿using RentalMoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalMoviesApp.ViewModels;
using System.Data.Entity;
namespace RentalMoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }


        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole("CanManageMovies"))
                return View("List");
            
                return View("ReadOnlyList");
            
        }
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}
        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);


        }
        public ActionResult FilterByGenre(string name)
        {
            var movie = _context.Movies.Include((n => n.Genre)).Where(n => n.Genre.Name.Equals(name)).ToList();


            if (movie == null)
                return HttpNotFound();

            return View(movie);


        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return HttpNotFound();
           
                _context.Movies.Remove(movieInDb);
                _context.SaveChanges();
            

            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View("List", movies);
        }
    }
}