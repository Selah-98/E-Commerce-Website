using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JKCrafts.Pages.Products;
using System.Data.SqlClient;

namespace JKCrafts.Pages.Products
{
    public class Index1Model : PageModel
    {
       
    
        public ProductInfo production = new ProductInfo();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            production.Price = Request.Form["Price"];
            production.Name = Request.Form["Name"];

            if (
                production.Name.Length == 0 ||
                production.Price.Length == 0
                )
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password={Jesus87@};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                  
                    string sql = "Insert into Production" +
                        "(Name,Price)" + "VALUES" +
                        "(@Name,@Price);";

                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {

                        command.Parameters.AddWithValue("@Name", production.Name);
                        command.Parameters.AddWithValue("@Price", production.Price);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {
                errorMessage = ex.Message;
            }


            production.Name = "";
            production.Price = "";


            successMessage = "Added Successfully";

            Response.Redirect("/Production/Index");


        }
    }
}