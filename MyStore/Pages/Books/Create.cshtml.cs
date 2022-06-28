using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        public BooksInfo booksInfo = new BooksInfo();

        public string errorMessage = "";

        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            booksInfo.booksName = Request.Form["booksName"];
            booksInfo.description = Request.Form["Description"];
            booksInfo.price = Request.Form["Price"];
            booksInfo.typeBook = Request.Form["TypeBook"];

            if (booksInfo.booksName.Length == 0 || booksInfo.description.Length == 0 ||
                booksInfo.price.Length == 0 || booksInfo.typeBook.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // save the new client into the database
            try
            {
                string connectionString = "Data Source=DESKTOP-GUM1BKT;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO books " +
                                 "(booksName, description, price, TypeBook) VALUES " +
                                 "(@booksName, @description, @price, @typeBook);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@booksName", booksInfo.booksName);
                        command.Parameters.AddWithValue("@description", booksInfo.description);
                        command.Parameters.AddWithValue("@price", booksInfo.price);
                        command.Parameters.AddWithValue("@typeBook", booksInfo.typeBook);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            booksInfo.booksName = ""; booksInfo.description = ""; booksInfo.price = ""; booksInfo.typeBook= "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Books/Index");
        }
    }
}
