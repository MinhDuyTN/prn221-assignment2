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
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.FunewsManagementDbContext _context;

        public DetailsModel(BusinessObject.FunewsManagementDbContext context)
        {
            _context = context;
        }

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
    }
}
