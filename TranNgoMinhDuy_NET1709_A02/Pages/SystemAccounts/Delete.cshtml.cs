using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace TranNgoMinhDuyRazorPages.Pages.SystemAccounts
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.FunewsManagementDbContext _context;

        public DeleteModel(BusinessObject.FunewsManagementDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null || _context.SystemAccounts == null)
            {
                return NotFound();
            }

            var systemaccount = await _context.SystemAccounts.FirstOrDefaultAsync(m => m.AccountId == id);

            if (systemaccount == null)
            {
                return NotFound();
            }
            else 
            {
                SystemAccount = systemaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null || _context.SystemAccounts == null)
            {
                return NotFound();
            }
            var systemaccount = await _context.SystemAccounts.FindAsync(id);

            if (systemaccount != null)
            {
                SystemAccount = systemaccount;
                _context.SystemAccounts.Remove(SystemAccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
