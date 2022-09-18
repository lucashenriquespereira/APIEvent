using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEvent.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> SelectEvents()
        {
            var query = "SELECT * FROM CityEvent";

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> SelectEventsByTitle(string title)
        {

            var query = $"SELECT * FROM CityEvent WHERE Title LIKE ('%' +  @Title + '%') ";
            var parameters = new DynamicParameters(new
            {
                title
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> SelectEventsByDateAndLocal(DateTime dateHourEvent, string local)
        {

            var query = $"SELECT * FROM CityEvent WHERE CONVERT(DATE, DateHourEvent)= @DateHourEvent AND Local Like('%' +  @Local + '%') ";
            var parameters = new DynamicParameters(new
            {
                dateHourEvent,
                local
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> SelectEventsByPriceAndDate(decimal low, decimal high, DateTime dateHourEvent)
        {

            var query = $"SELECT * FROM CityEvent WHERE Price >= @low AND Price <= @high AND CONVERT(DATE, DateHourEvent)= @DateHourEvent";
            var parameters = new DynamicParameters(new
            {
                low,
                high,
                dateHourEvent
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,
                cityEvent.Status
            });

            try
            { 
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public bool DeleteCityEvent(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters(new
            {
                idEvent
            });

            try
            { 
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }
        public bool UpdateCityEvent(CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent set Title = @Title, Description = @Description,
                            DateHourEvent = @DateHourEvent, Local = @Local, Address = @Address,
                            Price = @Price, Status = @Status
                            where IdEvent = @IdEvent";

            var parameters = new DynamicParameters(cityEvent);
            
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }
    }
}