using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using TestAssignment.DAL;
using TestAssignment.Web.Models;

namespace TestAssignment.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private IRepository<Employee> _repo;

        public EmployeeController(IRepository<Employee> repo)
        {
            _repo = repo;
        }

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var emps = await _repo.GetAll();

            return View(emps.Select(e => EmployeeDataModel.FromEmployee(e)));
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emp = await _repo.FindById(id.Value);
            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(EmployeeDataModel.FromEmployee(emp));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Age,TaxPercent")] EmployeeDataModel edm)
        {
            if (ModelState.IsValid)
            {
                Logger.Debug($"Creating new employee: {edm}");

                edm.Id = await _repo.Insert(edm.ToEmployee());

                return RedirectToAction("Index");
            }

            return View(edm);
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emp = await _repo.FindById(id.Value);
            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(EmployeeDataModel.FromEmployee(emp));
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Age,TaxPercent")] EmployeeDataModel edm)
        {
            if (ModelState.IsValid)
            {
                Logger.Debug($"Editing employee: {edm}");

                await _repo.Update(edm.ToEmployee());

                return RedirectToAction("Index");
            }

            return View(edm);
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emp = await _repo.FindById(id.Value);
            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(EmployeeDataModel.FromEmployee(emp));
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Logger.Debug($"Deleting employee with ID: {id}");

            await _repo.Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
