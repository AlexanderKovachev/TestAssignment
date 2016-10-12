using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TestAssignment.DAL.Dapper
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private string _connectionName;

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString);
        }

        public EmployeeRepository(string connectionName)
        {
            _connectionName = connectionName;
        }

        public async Task<int> Delete(int id)
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                return await cn.DeleteAsync<Employee>(id);
            }
        }

        public async Task<Employee> FindById(int id)
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                return await cn.GetAsync<Employee>(id);
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                return await cn.GetListAsync<Employee>();
            }
        }

        public async Task<int> Insert(Employee emp)
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                return await cn.InsertAsync<int>(emp);
            }
        }

        public async Task<int> Update(Employee emp)
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                return await cn.UpdateAsync(emp);
            }
        }

        public void Dispose()
        {

        }
    }
}
