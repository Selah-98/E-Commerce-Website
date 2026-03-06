
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        
      
        public userinfo customerinfo = new userinfo();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
           
            customerinfo.Avalibablity = Request.Form["Avalibablity"];
            customerinfo.Quantity = Request.Form["Quantity"];
            customerinfo.ProductName = Request.Form["ProductName"];

            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    string sql = "Insert into Transactionings" +
                        "(Avalibablity,Quantity, ProductName)" + "VALUES" +
                        "(@Avalibablity,@Quantity,@ProductName)";

                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {



                        command.Parameters.AddWithValue("@Quantity", customerinfo.Quantity);
                        command.Parameters.AddWithValue("@ProductName", customerinfo.ProductName);
                        command.Parameters.AddWithValue("@Avalibablity", customerinfo.Avalibablity);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {
                errorMessage = ex.Message;
            }


            customerinfo.ProductName = "";
            customerinfo.Avalibablity = "";
            customerinfo.Quantity = "";


            successMessage = "Added Successfully";

            Response.Redirect("/Transaction/Index");


        }

        public class userinfo
        {
            public string Avalibablity { get; set; }
            public string Quantity { get; set; }
            public string ProductName { get; set; }

        }
    }
}



