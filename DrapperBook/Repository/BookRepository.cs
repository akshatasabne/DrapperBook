using Dapper;
using DrapperBook.Data;
using DrapperBook.Models;
using System.Data;

namespace DrapperBook.Repository
{
    public class BookRepository : IBookRepository
    {

        //Using SQL
        //private readonly ApplicationDbContext context;

        //public BookRepository(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}

        //public async Task<int> AddBook(Book book)
        //{
        //    int result = 0;
        //    var query = "insert into Book values(@name,@author,@price)";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@name", book.Name);
        //    parameters.Add("@author", book.Author);
        //    parameters.Add("@price", book.Price);
        //    using (var connection = context.CreateConnection())
        //    {
        //        result = await connection.ExecuteAsync(query, parameters);
        //    }
        //    return result;
        //}



        //public async Task<int> DeleteBook(int id)
        //{
        //    int result = 0;
        //    var query = "delete from Book where id=@id";

        //    using (var connection = context.CreateConnection())
        //    {
        //        result = await connection.ExecuteAsync(query, new { id });
        //    }
        //    return result;
        //}

        //public async Task<Book> GetBookById(int id)
        //{
        //    var qry = "select * from Book where id=@id";
        //    using (var connection = context.CreateConnection())
        //    {
        //        var result = await connection.QuerySingleOrDefaultAsync<Book>(qry, new { id });
        //        return result;
        //    }
        //}

        //public async Task<IEnumerable<Book>> GetBooks()
        //{
        //    var qry = "select * from Book";
        //    using (var connection = context.CreateConnection())
        //    {
        //        var result = await connection.QueryAsync<Book>(qry);
        //        return result.ToList();
        //    }
        //}

        //public async Task<int> UpdateBook(Book book)
        //{
        //    int result = 0;
        //    var query = "update Book set name=@name,author=@author,price=@price where id=@id";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@name", book.Name);
        //    parameters.Add("@author", book.Author);
        //    parameters.Add("@price", book.Price);
        //    parameters.Add("@id", book.Id);
        //    using (var connection = context.CreateConnection())
        //    {
        //        result = await connection.ExecuteAsync(query, parameters);
        //    }
        //    return result;
        //}
        


        /// <summary>
        /// With StoredProcedure
        /// </summary>
        private readonly ApplicationDbContext context;

        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddBook(Book book)
        {
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@name", book.Name);
            parameters.Add("@author", book.Author);
            parameters.Add("@price", book.Price);
            using (var connection = context.CreateConnection())
            {
                result = await connection.ExecuteAsync("SP_book_InsertBook", parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }



        public async Task<int> DeleteBook(int id)
        {
            int result = 0;


            using (var connection = context.CreateConnection())
            {
                result = await connection.ExecuteAsync("SP_book_DeleteBook", new { id }, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<Book> GetBookById(int id)
        {

            using (var connection = context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Book>("SP_book_GetBookById", new { id }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {

            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<Book>("SP_book_GetBooks", commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> UpdateBook(Book book)
        {
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@name", book.Name);
            parameters.Add("@author", book.Author);
            parameters.Add("@price", book.Price);
            parameters.Add("@id", book.Id);
            using (var connection = context.CreateConnection())
            {
                result = await connection.ExecuteAsync("SP_book_UpdateBook", parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

    }
}
