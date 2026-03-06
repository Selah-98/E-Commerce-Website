using Microsoft.AspNetCore.Mvc;
using JKCrafts.Pages.Transactions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.Products
{
    public class IndexModel : PageModel
    {

        public List<ProductInfo> ProductList = new List<ProductInfo>();

        public void OnGet()
        {
            try
            {

                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    String sql = "Select *FROM Products";
                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ProductInfo customerinfov = new ProductInfo();
                                    customerinfov.ProductID = "" + reader.GetInt32(0);
                                    customerinfov.Name = reader.GetString(1);
                                    customerinfov.Price = reader.GetString(2);



                                    ProductList.Add(customerinfov);


                                }
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
        public void OnPost(string ProductID)
        {
            ProductInfo customerinfov = new ProductInfo();
            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password={Jesus87@};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    string sql = "INSERT INTO Cart (Name,price)" +
                        "Select Name,Price" +
                        "From Production" +
                        "WHERE ID=@productID";

                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {
                        {

                            command.Parameters.AddWithValue("@productID", ProductID);



                            command.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("/Cart/Cart");
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class ProductInfo
    {
        public string ProductID;
        public string Name;
        public string Price;


    }
}