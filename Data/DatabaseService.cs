using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobPosting.Data
{
    public class DatabaseService
    {
        private readonly JobPostingContext _context;

        public DatabaseService(JobPostingContext context)
        {
            _context = context;
        }

        public async Task TestDatabaseConnectionAsync()
        {
            try
            {
                await _context.Database.OpenConnectionAsync().ConfigureAwait(false);
                Console.WriteLine("Connection to database successful.");
                await _context.Database.CloseConnectionAsync().ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                Console.WriteLine($"A database error occurred while connecting to the database: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"An invalid operation occurred while connecting to the database: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while connecting to the database: {ex.Message}");
            }
        }
    }
}