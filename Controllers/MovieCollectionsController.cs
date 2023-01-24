using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReelRoster.Data;
using ReelRoster.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelRoster.Controllers
{
    public class MovieCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieCollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index(int? id)
        {
            id ??= (await _context.Collection.FirstOrDefaultAsync(c => c.Name.ToUpper() == "ALL")).Id;

            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name", id);

            var allMovieIds = await _context.Movie.Select(m => m.Id).ToListAsync();

            var movieIdsInCollection = await _context.MovieCollection
                                        .Where(m => m.CollectionId == id)
                                        .OrderBy(m => m.Order)
                                        .Select(m => m.MovieId)
                                        .ToListAsync();

            var movieIdsNotInCollection = allMovieIds.Except(movieIdsInCollection);

            var moviesInCollection = new List<Movie>();
            movieIdsInCollection.ForEach(movieId => moviesInCollection.Add(_context.Movie.Find(movieId)));

            ViewData["IdsInCollection"] = new MultiSelectList(moviesInCollection, "Id", "Title");

            var moviesNotInCollection = await _context.Movie.AsNoTracking().Where(m => movieIdsNotInCollection.Contains(m.Id)).ToListAsync();
            ViewData["IdsNotInCollection"] = new MultiSelectList(moviesNotInCollection, "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, List<int> idsInCollection)
        {
            var oldRecords = _context.MovieCollection.Where(c => c.CollectionId == id);
            _context.MovieCollection.RemoveRange(oldRecords);
            await _context.SaveChangesAsync();

            if(idsInCollection != null)
            {
                int index = 1;
                idsInCollection.ForEach(movieId =>
                {
                    _context.Add(new MovieCollection()
                    {
                        CollectionId = id,
                        MovieId = movieId,
                        Order = index++
                    });
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new {id});
        }
    }
}
