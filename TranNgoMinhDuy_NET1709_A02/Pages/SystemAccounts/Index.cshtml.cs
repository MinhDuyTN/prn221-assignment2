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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.FunewsManagementDbContext _context;

        public IndexModel(BusinessObject.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SystemAccounts != null)
            {
                SystemAccount = await _context.SystemAccounts.ToListAsync();
            }
        }
    }
}
