using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace TranNgoMinhDuyRazorPages.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.FunewsManagementDbContext _context;

        public DetailsModel(BusinessObject.FunewsManagementDbContext context)
        {
            _context = context;
        }

      public Tag Tag { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tags == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(m => m.TagId == id);
            if (tag == null)
            {
                return NotFound();
            }
            else 
            {
                Tag = tag;
            }
            return Page();
        }
    }
}
