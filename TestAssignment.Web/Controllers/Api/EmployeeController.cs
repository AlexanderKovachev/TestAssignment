using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TestAssignment.DAL;
using TestAssignment.Web.Models;
using NLog;

namespace TestAssignment.Web.Controllers.Api
{
    public class EmployeeController : ApiController
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private IRepository<Employee> _repo;

        public EmployeeController(IRepository<Employee> repo)
        {
            _repo = repo;
        }

        // GET: api/Employee
        [ResponseType(typeof(IEnumerable<EmployeeDataModel>))]
        public async Task<IHttpActionResult> Get()
        {
            var emps = await _repo.GetAll();

            return Ok(emps.Select(e => EmployeeDataModel.FromEmployee(e)));
        }

        // GET: api/Employee/5
        [ResponseType(typeof(EmployeeDataModel))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var emp = await _repo.FindById(id);

            if (emp == null)
                return NotFound();

            return Ok(EmployeeDataModel.FromEmployee(emp));
        }

        // POST: api/Employee
        [ResponseType(typeof(EmployeeDataModel))]
        public async Task<IHttpActionResult> Post([FromBody]EmployeeDataModel edm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Logger.Debug($"Creating new employee (via API): {edm}");
            edm.Id = await _repo.Insert(edm.ToEmployee());

            return CreatedAtRoute("DefaultApi", new { edm.Id }, edm);
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(EmployeeDataModel))]
        public async Task<IHttpActionResult> Put(int id, [FromBody]EmployeeDataModel edm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Logger.Debug($"Editing employee (via API): {edm}");
            await _repo.Update(edm.ToEmployee());

            return Ok(edm);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(EmployeeDataModel))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Logger.Debug($"Deleting employee (via API) with ID: {id}");

            var emp = await _repo.FindById(id);

            if (emp == null)
                return NotFound();

            await _repo.Delete(emp.Id);

            return Ok(emp);
        }
    }
}