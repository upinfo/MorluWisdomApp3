using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorluWisdomApp3.Data;
using MorluWisdomApp3.Models;

namespace MorluWisdomApp3.Controllers
{
    public class Wise_SayingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Wise_SayingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wise_Saying
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wise_Saying.ToListAsync());
        }

        // GET: Wise_Saying/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Wise_Saying/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Wise_Saying.Where( j => j.YourQuestion.Contains (SearchPhrase)).ToListAsync());
        }
        // GET: Wise_Saying/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wise_Saying = await _context.Wise_Saying
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wise_Saying == null)
            {
                return NotFound();
            }

            return View(wise_Saying);
        }

        // GET: Wise_Saying/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wise_Saying/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,YourQuestion,MorluAnswer")] Wise_Saying wise_Saying)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wise_Saying);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wise_Saying);
        }

        // GET: Wise_Saying/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wise_Saying = await _context.Wise_Saying.FindAsync(id);
            if (wise_Saying == null)
            {
                return NotFound();
            }
            return View(wise_Saying);
        }

        // POST: Wise_Saying/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,YourQuestion,MorluAnswer")] Wise_Saying wise_Saying)
        {
            if (id != wise_Saying.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wise_Saying);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Wise_SayingExists(wise_Saying.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wise_Saying);
        }

        // GET: Wise_Saying/Delete/5

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wise_Saying = await _context.Wise_Saying
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wise_Saying == null)
            {
                return NotFound();
            }

            return View(wise_Saying);
        }

        // POST: Wise_Saying/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wise_Saying = await _context.Wise_Saying.FindAsync(id);
            _context.Wise_Saying.Remove(wise_Saying);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Wise_SayingExists(int id)
        {
            return _context.Wise_Saying.Any(e => e.Id == id);
        }
    }
}
