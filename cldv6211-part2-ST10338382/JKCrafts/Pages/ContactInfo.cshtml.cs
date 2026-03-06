using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKCrafts.Pages
{
    public class ContactInfoModel : PageModel
    {
        private readonly ILogger<ContactInfoModel> _logger;

        public ContactInfoModel(ILogger<ContactInfoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
        

    


