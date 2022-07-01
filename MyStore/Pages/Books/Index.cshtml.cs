using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Books
{
    public partial class IndexModel : PageModel
    {
        public List<BooksInfo> listBooks = new List<BooksInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-GUM1BKT;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BooksInfo booksInfo = new BooksInfo();
                                booksInfo.id = "" + reader.GetInt32(0);
                                booksInfo.booksName = reader.GetString(1);
                                booksInfo.description = reader.GetString(2);
                                booksInfo.price = "" + reader.GetDecimal(3);
                                booksInfo.typeBook = reader.GetString(4);
                                booksInfo.image = reader.GetString(5);

                                listBooks.Add(booksInfo);
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

    public class BooksInfo
    {
        public string id { get; set; }
        public string booksName { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string typeBook { get; set; }
        public string image { get; set; }
    }

}
