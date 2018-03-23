using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using UsingExternalDataInCallPro.Models;
using Newtonsoft.Json;

namespace UsingExternalDataInCallPro.Controllers
{
    public class LookupController : Controller
    {
        private DataContext db = new DataContext();

        #region CRUD
        // GET: Lookup
        public async Task<ActionResult> Index()
        {
            return View(await db.Lookup.ToListAsync());
        }

        // GET: Lookup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lookup/Create
        // We should use a view model that only contains the properties we which to bind
        // for simplicity of the demo I am using the EF class directly
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Lookup lookupViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Lookup.Add(lookupViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lookupViewModel);
        }

        // GET: Lookup/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookupViewModel = await db.Lookup.FindAsync(id);
            if (lookupViewModel == null)
            {
                return HttpNotFound();
            }
            return View(lookupViewModel);
        }

        // GET: Lookup/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookupViewModel = await db.Lookup.FindAsync(id);
            if (lookupViewModel == null)
            {
                return HttpNotFound();
            }
            return View(lookupViewModel);
        }

        // POST: Lookup/Edit/5
        // We should use a view model that only contains the properties we which to bind
        // for simplicity of the demo I am using the EF class directly
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Lookup lookupViewModel)
        {
            if (ModelState.IsValid)
            {
                Lookup updateViewModel = await db.Lookup.FindAsync(lookupViewModel.ID);
                if (updateViewModel == null)
                {
                    return HttpNotFound();
                }
                updateViewModel.FirstName = lookupViewModel.FirstName;
                updateViewModel.LastName = lookupViewModel.LastName;
                updateViewModel.CLEntryID = lookupViewModel.CLEntryID;
                updateViewModel.Reserved = lookupViewModel.Reserved;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lookupViewModel);
        }

        // GET: Lookup/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookupViewModel = await db.Lookup.FindAsync(id);
            if (lookupViewModel == null)
            {
                return HttpNotFound();
            }
            return View(lookupViewModel);
        }

        // POST: Lookup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lookup lookupViewModel = await db.Lookup.FindAsync(id);
            db.Lookup.Remove(lookupViewModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion 

        #region API
        // GET: /Lookup
        [HttpGet]
        public async Task<string> GetList() => JsonConvert.SerializeObject(await db.Lookup.ToListAsync());

        //POST: /Lookup/Reserve
        [HttpPost]
        public async Task<string> Reserve(int id, int clentryid)
        {
            Lookup item = db.Lookup.Find(id);
            if (item == null)
            {
                return null;
            }
            item.Reserved = true;
            item.CLEntryID = clentryid;
            await db.SaveChangesAsync();
            return JsonConvert.SerializeObject(item);
        }

        //POST: /Lookup/Unreserve
        [HttpPost]
        public async Task<string> Unreserve(int id)
        {
            Lookup item = db.Lookup.Find(id);
            if (item == null)
            {
                return null;
            }
            item.Reserved = false;
            item.CLEntryID = 0;
            await db.SaveChangesAsync();
            return JsonConvert.SerializeObject(item);
        }
        #endregion

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
