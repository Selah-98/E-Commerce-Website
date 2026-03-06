using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace JKCrafts.Pages.Cart
{
    public class CartModel : PageModel
    {
        public List<CartItems> listcustomers = new List<CartItems>();
        public void OnGet()
        {
            try
            {

                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    String sql = "Select *FROM Cart";
                    using (SqlCommand cmd = new SqlCommand(sql, Connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())

                            {

                                CartItems customerInfo = new CartItems();

                                customerInfo.Id = "" + reader.GetInt32(0);

                                customerInfo.Name= reader.GetString(1);

                                customerInfo.Price = reader.GetString(2);

                                listcustomers.Add(customerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }

            
            
      
    
    public class CartItems
    {
        public string Id;
        public string Name;
        public string Price;    

    }
    }

