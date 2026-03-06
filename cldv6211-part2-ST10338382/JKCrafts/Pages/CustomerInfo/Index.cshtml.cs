using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace JKCrafts.Pages.CustomerInfo
{
    public class IndexModel : PageModel
    {


        public List<userinfo> listuser = new List<userinfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Server=tcp:diva.database.windows.net,1433;Initial Catalog=ST10338382;Persist Security Info=False;User ID=Brandy;Password=Jesus87@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select *FROM userrs";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userinfo customerinfos = new userinfo();
                                customerinfos.id = "" + reader.GetInt32(0);
                                customerinfos.name = reader.GetString(1);
                                customerinfos.email = reader.GetString(2);
                                customerinfos.phone = reader.GetString(3);
                                customerinfos.address = reader.GetString(4);

                                customerinfos.created_at = reader.GetDateTime(5).ToString();
                                listuser.Add(customerinfos);


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



    public class userinfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;

        public string created_at;
    }
}
