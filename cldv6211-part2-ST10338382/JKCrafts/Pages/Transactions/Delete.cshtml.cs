using Microsoft.AspNetCore.Mvc;
using JKCrafts.Pages.Transactions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.Transactions
{
    public class DeleteModel : PageModel
    {
    
        public Transaction Transaction { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Transactions/Index");
            }

            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT TransactionID, Avalibablity, Quantity, ProductName FROM Transactions WHERE TransactionID = @TransactionID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionID", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Transaction = new Transaction
                                {
                                    TransactionID = reader.GetInt32(0),
                                    Availability = reader.GetString(1),
                                    Quantity = reader.GetInt32(2),
                                    ProductName = reader.GetString(3)
                                };
                            }
                            else
                            {
                                return RedirectToPage("/Transactions/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Transactions/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Transactions WHERE TransactionID = @TransactionID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionID", Transaction.TransactionID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not done here for brevity)
                return RedirectToPage("/Transactions/Index");
            }

            return RedirectToPage("/Transactions/Index");
        }
    }
}

public class Transaction
{
    public int TransactionID { get; set; }
    public string Availability { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
}
