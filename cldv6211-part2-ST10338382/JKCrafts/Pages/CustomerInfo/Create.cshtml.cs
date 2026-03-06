using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace JKCrafts.Pages.CustomerInfo
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly string _connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
            UserInfo = new UserInfo();  // Initialize UserInfo
        }

        [BindProperty]
        public UserInfo UserInfo { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO userrs (Name, Email, Phone, Address, CreatedAt) " +
                                 "VALUES (@Name, @Email, @Phone, @Address, GETDATE())";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", UserInfo.Name);
                        command.Parameters.AddWithValue("@Email", UserInfo.Email);
                        command.Parameters.AddWithValue("@Phone", UserInfo.Phone);
                        command.Parameters.AddWithValue("@Address", UserInfo.Address);
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("/CustomerInfo/Index");  // Redirect to the Index page after successful save
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user.");
                return Page();
            }
        }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
