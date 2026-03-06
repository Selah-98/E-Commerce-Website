using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKCrafts.Pages
{
    public class JKhumaloWorkShopModel : PageModel
    {
        

            private readonly ILogger<JKhumaloWorkShopModel> _logger;

        public JKhumaloWorkShopModel(ILogger<JKhumaloWorkShopModel> logger)
            {
                _logger = logger;
            }
            public void OnGet()
        {
        }
    }
}
