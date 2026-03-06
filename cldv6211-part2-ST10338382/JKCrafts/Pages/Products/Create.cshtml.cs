using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.Products
{
    public class CreateModel : PageModel
    {
        
   
        public userinfos customerinfo = new userinfos();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            customerinfo.Price = Request.Form["Price"];
            customerinfo.Name = Request.Form["Name"];

         
            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password={Jesus87@};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    string sql = "Insert into Products" +
                        "(Name,Price)" + "VALUES" +
                        "(@Name,@Price);";

                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {

                        command.Parameters.AddWithValue("@Name", customerinfo.Name);
                        command.Parameters.AddWithValue("@Price", customerinfo.Price);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {
                errorMessage = ex.Message;
            }


            customerinfo.Name = "";
            customerinfo.Price = "";


            successMessage = "Added Successfully";

            Response.Redirect("/Products/Index");


        }
        public class userinfos
        {
            public string Name { get; set; }
            public string Price { get; set; }
        }
    }
}
