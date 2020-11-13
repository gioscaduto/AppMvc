using AppMvc.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppMvc.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        [Route("students-list")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Students.ToListAsync());
        }

        [HttpGet]
        [Route("student-details/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var student = await db.Students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpGet]
        [Route("new-student")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("new-student")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Active")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.RegistrationDate = DateTime.Now;
                db.Students.Add(student);
                await db.SaveChangesAsync();

                TempData["Message"] = "Student Successfully Registered";

                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpGet]
        [Route("edit-student/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            Student student = await db.Students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


        [HttpPost]
        [Route("edit-student/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Active")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.Entry(student).Property(s => s.RegistrationDate).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpGet]
        [Route("delete-student/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Student student = await db.Students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost]
        [Route("delete-student/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
