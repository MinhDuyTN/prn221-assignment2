using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace TranNgoMinhDuyRazorPages.Pages.NewsArticles
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.FunewsManagementDbContext _context;

        public DeleteModel(BusinessObject.FunewsManagementDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.NewsArticles == null)
            {
                return NotFound();
            }

            var newsarticle = await _context.NewsArticles.FirstOrDefaultAsync(m => m.NewsArticleId == id);

            if (newsarticle == null)
            {
                return NotFound();
            }
            else 
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.NewsArticles == null)
            {
                return NotFound();
            }
            var newsarticle = await _context.NewsArticles.FindAsync(id);

            if (newsarticle != null)
            {
                NewsArticle = newsarticle;
                _context.NewsArticles.Remove(NewsArticle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
