using Dapper;
using DrapperBook.Data;
using DrapperBook.Models;

namespace DrapperBook.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            int result = 0;
            var query = "insert into Employee values(@Ename,@Department,@Salary)";
            var parameters = new DynamicParameters();
            parameters.Add("@Ename", employee.Ename);
            parameters.Add("@Department", employee.Department);
            parameters.Add("@Salary", employee.Salary);
            using (var connection = context.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, parameters);
            }
            return result;
        }

        public async Task<int> DeleteEmployee(int id)
        {
            int result = 0;
            var query = "delete from Employee where Eid=@Eid";

            using (var connection = context.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, new { Eid = id });
            }
            return result;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var qry = "select * from Employee where Eid=@Eid";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Employee>(qry, new { Eid = id });
                return result;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var qry = "select * from Employee";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<Employee>(qry);
                return result.ToList();
            }
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            int result = 0;
            var query = "update Employee set Ename=@Ename,Department=@Department,Salary=@Salary where Eid=@Eid";
            var parameters = new DynamicParameters();
            parameters.Add("@Ename", employee.Ename);
            parameters.Add("@Department", employee.Department);
            parameters.Add("@Salary", employee.Salary);
            parameters.Add("@Eid", employee.Eid);
            using (var connection = context.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, parameters);
            }
            return result;
        }
    }
}
