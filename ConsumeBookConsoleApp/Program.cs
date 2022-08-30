using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeBookConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            BookviewModal bookviewModal = new BookviewModal();
            bookviewModal.BookName = "New_Book";
            bookviewModal.BookAuthor = "New Author";
            bookviewModal.CourseName = "MCA";
            bookviewModal.PurchaseDate = DateTime.Now;
            bookviewModal.BookId = 10;


            //var task = obj.GetBook();
            var task = obj.UpdateBook(bookviewModal);
            task.Wait();
            var result = task.Result;
            //foreach (var item in task.Result)
            //{
            //    Console.WriteLine("Book Id="+item.BookId);
            //    Console.WriteLine("Book Name=" + item.BookName);
            //    Console.WriteLine("Course Name=" + item.CourseName);
            //    Console.WriteLine("Book Author=" + item.BookAuthor);
            //    Console.WriteLine("Purchase Date=" + item.PurchaseDate);
            //    Console.WriteLine("------------------------");
            //}
            Console.ReadKey();
        }
        public async Task<List<BookviewModal>> GetBook()
        {
            List<BookviewModal> list = new List<BookviewModal>();
            using (var httpClient = new HttpClient())
            {
                using (var respnse = await httpClient.GetAsync("https://localhost:44331/api/Book/GetBooks"))
                {
                    var apiResponse = await respnse.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<BookviewModal>>(apiResponse);
                }
            }
            return list;
        }
        public async Task<BookviewModal> GetBookbyId(int bookId)
        {
            BookviewModal list = new BookviewModal();
            using (var httpClient = new HttpClient())
            {
                using (var respnse = await httpClient.GetAsync("https://localhost:44331/api/Book/GetBooks/" + bookId))
                {
                    var apiResponse = await respnse.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<BookviewModal>(apiResponse);
                }
            }
            return list;
        }

        public async Task<int> PostBook(BookviewModal bookviewModal)
        {
            int result = 0;
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(bookviewModal));
                using (var response = await client.PostAsync("https://localhost:44331/api/Book/PostBook", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if(apiResponse!=null)
                        result= Convert.ToInt32(apiResponse);
                }
            }
            return result;
        }

        public async Task<int> UpdateBook(BookviewModal bookviewModal)
        {
            int result = 0;
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(bookviewModal), Encoding.UTF8, "application/json");
                using (var response = await client.PutAsync("https://localhost:44331/api/Book/UpdateBook", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != null)
                        result = Convert.ToInt32(apiResponse);
                }
            }
            return result;
        }


        public async Task<int> DeleteBook(int bookId)
        {
            using (var client = new HttpClient())
            {
                using (var reponse = await client.DeleteAsync("https://localhost:44331/api/Book/DeteleBook/" + bookId))
                {
                    var apiResponse = await reponse.Content.ReadAsStringAsync();
                    return Convert.ToInt32(apiResponse);
                }
            }
            return 0;

        }
    }
}
