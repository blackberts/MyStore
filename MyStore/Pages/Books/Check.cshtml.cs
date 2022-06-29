using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Books
{
    public class CheckModel : PageModel
    {
        public BooksInfo booksInfo = new BooksInfo();
        public void OnGet()
        {
            // with Request.Query["id"] i'm finally did this
            // This code get id number when you press on button "check"
            string id = Request.Query["Id"];
            try
            {
                string connectionString = "Data Source=DESKTOP-GUM1BKT;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                booksInfo.id = "" + reader.GetInt32(0);
                                booksInfo.booksName = reader.GetString(1);
                                booksInfo.description = reader.GetString(2);
                                booksInfo.price = "" + reader.GetDecimal(3);
                                booksInfo.typeBook = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

