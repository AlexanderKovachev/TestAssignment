using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace TestAssignment.DAL.EF
{
    public class EmployeeRepository : DbContext, IRepository<Employee>
    {
        public EmployeeRepository() : base("DefaultConnection") { }

        public EmployeeRepository(string connectionName) : base(connectionName) { }

        public DbSet<Employee> Employees { get; set; }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await Employees.ToListAsync();
        }

        public async Task<Employee> FindById(int id)
        {
            return await Employees.FindAsync(id);
        }

        public async Task<int> Insert(Employee emp)
        {
            Employees.Add(emp);

            return await SaveChangesAsync();
        }

        public async Task<int> Update(Employee emp)
        {
            Entry(emp).State = EntityState.Modified;

            return await SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var employeeDataModel = await Employees.FindAsync(id);
            Employees.Remove(employeeDataModel);

            return await SaveChangesAsync();
        }
    }
}
