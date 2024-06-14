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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.FunewsManagementDbContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(BusinessObject.FunewsManagementDbContext context,IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<NewsArticle> NewsArticles { get; set; }
        public IList<NewsArticle> NewsArticle { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            /*            if (_context.NewsArticles != null)
                        {
                            NewsArticle = await _context.NewsArticles
                            .Include(n => n.Category)
                            .Include(n => n.CreatedBy).ToListAsync();
                        }*/
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
                CurrentFilter = searchString;
            }
            IQueryable<NewsArticle> na = from s in _context.NewsArticles
                                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                na = na.Where(na => na.NewsTitle.Contains(searchString)
                || na.NewsContent.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    na = na.OrderByDescending(s => s.NewsTitle);
                    break;
                case "Date":
                    na = na.OrderByDescending(s => s.CreatedDate);
                    break;
                case "date_desc":
                    na = na.OrderBy(s => s.CreatedDate);
                    break;
                default:
                    na = na.OrderBy(s => s.NewsTitle);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            NewsArticles = await PaginatedList<NewsArticle>.CreateAsync(
            na.AsNoTracking(), pageIndex ?? 1,pageSize);
        }
    }
}
