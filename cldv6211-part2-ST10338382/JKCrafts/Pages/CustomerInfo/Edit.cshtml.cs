using Microsoft.AspNetCore.Mvc;
using JKCrafts.Pages.CustomerInfo;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.CustomerInfo
{
  

    public class EditModel : PageModel
    {
        [BindProperty]
        public CustomerInfo customerInfos { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT id, name, email, phone, address, created_at FROM customers WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerInfos = new CustomerInfo
                                {
                                    id = reader.GetInt32(0),
                                    name = reader.GetString(1),
                                    email = reader.GetString(2),
                                    phone = reader.GetString(3),
                                    address = reader.GetString(4),
                                    created_at = reader.GetDateTime(5)
                                };
                                // Storing Date and Time
                                command.Parameters.AddWithValue("@created_at", DateTime.Now);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE customers SET name = @name, email = @email, phone = @phone, address = @address WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", customerInfos.id);
                        command.Parameters.AddWithValue("@name", customerInfos.name);
                        command.Parameters.AddWithValue("@email", customerInfos.email);
                        command.Parameters.AddWithValue("@phone", customerInfos.phone);
                        command.Parameters.AddWithValue("@address", customerInfos.address);

                        command.ExecuteNonQuery();
                    }
                }
                SuccessMessage = "Customer updated successfully!";
                return RedirectToPage("/CustomerInfo/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }

    public class CustomerInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime created_at { get; set; }
    }
}
