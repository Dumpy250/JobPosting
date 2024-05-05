using Microsoft.AspNetCore.Mvc;
using JobPosting.Data;
using JobPosting.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobPosting.Controllers
{
    public class JobsController : Controller
    {
        private readonly JobPostingContext _context;

        public JobsController(JobPostingContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var jobs = await _context.Jobs
            .Include(j => j.Employer) // Include the Employer in the query
            .ToListAsync();

            return View(jobs);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Employer) // Include the Employer in the query
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            var employers = new SelectList(_context.Employers, "Id", "Name");
            if (employers == null || !employers.Any())
            {
                // Log an error message or throw an exception
                throw new Exception("No employers found in the database.");
            }

            ViewBag.Employers = employers;

            // Convert SelectList to string and print it
            var employersString = string.Join(", ", employers.Items.Cast<Employer>().Select(e => $"Id: {e.Id}, Name: {e.Name}"));
            System.Diagnostics.Debug.WriteLine(employersString);

            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,EmployerId")] Job job)
        {
            if (ModelState.IsValid)
            {
                var employerExists = await _context.Employers.AnyAsync(e => e.Id == job.EmployerId);
                if (!employerExists)
                {
                    // Log an error message or throw an exception
                    throw new Exception("The selected employer does not exist.");
                }

                // Set the Employer and ApplicantJobs properties
                job.Employer = await _context.Employers.FindAsync(job.EmployerId);
                job.ApplicantJobs = new List<ApplicantJob>(); // Initialize with an empty list

                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
            .Include(j => j.Employer) // Include the Employer in the query
            .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            // Populate ViewBag.EmployerId with a SelectList of Employers
            ViewBag.EmployerId = new SelectList(_context.Employers, "Id", "Name", job.EmployerId);

            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,EmployerId")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var employerExists = await _context.Employers.AnyAsync(e => e.Id == job.EmployerId);
                if (!employerExists)
                {
                    // Log an error message or throw an exception
                    throw new Exception("The selected employer does not exist.");
                }

                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}