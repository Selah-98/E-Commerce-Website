using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.Transactions
{
    public class IndexModel : PageModel
   
    {
        public List<userinfo> listcustomers { get; set; } = new List<userinfo>();

        public void OnGet()
        {
            try
            {
               
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    
                    Connection.Open();
                    string sql = "Select *From Transactions";

                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userinfo customerinfo = new userinfo
                                {
                                    TransactionID = reader.GetInt32(0),
                                    Avalibablity = reader.GetString(1),
                                    Quantity = reader.GetString(2),
                                    ProductName = reader.GetString(3)
                                };

                                listcustomers.Add(customerinfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show an error message, etc.)
                // errorMessage = ex.Message; // You can display this error message on the page if needed
            }
        }
        public class userinfo
        {
            public int TransactionID { get; set; }
            public string Avalibablity { get; set; }
            public string Quantity { get; set; }
            public string ProductName { get; set; }
        }
    }
}

