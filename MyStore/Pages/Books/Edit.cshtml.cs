using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Books
{
    public class EditModel : PageModel
    {
        public BooksInfo booksInfo = new BooksInfo();

        public string errorMessage = "";

        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["Id"];

            try
            {
                string connectionString = "Data Source=DESKTOP-GUM1BKT;Initial Catalog=mystore;Integrated Security=True";
                
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            booksInfo.id = Request.Form["Id"];
            booksInfo.booksName = Request.Form["booksName"];
            booksInfo.description = Request.Form["Description"];
            booksInfo.price = Request.Form["Price"];
            booksInfo.typeBook = Request.Form["TypeBook"];

            if (booksInfo.id.Length == 0 || booksInfo.booksName.Length == 0 || booksInfo.description.Length == 0 ||
                booksInfo.price.Length == 0 || booksInfo.typeBook.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-GUM1BKT;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE books " +
                                 "SET booksName = @booksName, description=@description, price=@price, typeBook=@typeBook " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@booksName", booksInfo.booksName);
                        command.Parameters.AddWithValue("@description", booksInfo.description);
                        command.Parameters.AddWithValue("@price", booksInfo.price);
                        command.Parameters.AddWithValue("@typeBook", booksInfo.typeBook);
                        command.Parameters.AddWithValue("@id", booksInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Books/Index");
        }
    }
}
