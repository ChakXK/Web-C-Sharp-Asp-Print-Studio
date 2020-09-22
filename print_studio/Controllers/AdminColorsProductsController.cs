using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using print_studio.Models;
using System.Drawing;
using Image = print_studio.Models.Image;

namespace print_studio.Controllers
{
    public class AdminColorsProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ColorsProducts
        public async Task<ActionResult> Index(int id)
        {
            var colorsProducts = db.ColorsProducts.Where(cp=>cp.id_producttype==id)
                .Include(c => c.Image).Include(c => c.ProductType);
            ViewBag.id = id;
            return View(await colorsProducts.ToListAsync());
        }

        // GET: ColorsProducts/Create
        public ActionResult Create(int id)
        {
            ViewBag.id_producttype = id;

            return View();
        }

        // POST: ColorsProducts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,color,id_producttype")] ColorsProduct colorsProduct,
            HttpPostedFileBase upload)
        {
            if (ModelState.IsValid && upload!=null)
            {
                Image img = await UploadImage(upload);
                colorsProduct.id_image = img.id;
                db.ColorsProducts.Add(colorsProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = colorsProduct.id_producttype });
            }
            ViewBag.id_producttype = colorsProduct.id_producttype;
            return View(colorsProduct);
        }

        // GET: ColorsProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorsProduct colorsProduct = await db.ColorsProducts.FindAsync(id);
            if (colorsProduct == null)
            {
                return HttpNotFound();
            }
            return View(colorsProduct);
        }

        // POST: ColorsProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ColorsProduct colorsProduct = await db.ColorsProducts.FindAsync(id);
            int? idp = colorsProduct.id_producttype;
            db.ColorsProducts.Remove(colorsProduct);
            await db.SaveChangesAsync();
          
            return RedirectToAction("Index",new { id = idp});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<Image> UploadImage(HttpPostedFileBase upload)
        {
            Image img = new Image();
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                img.name = fileName;
                db.Images.Add(img);
                await db.SaveChangesAsync();
                upload.SaveAs(Server.MapPath("~/Content/temp/-" + img.id+img.name));
                Bitmap b1 = new Bitmap(System.Drawing.Image.FromFile(Server.MapPath("~/Content/temp/-" +
                    +img.id + img.name)));
                Bitmap imag = new Bitmap(b1, new System.Drawing.Size(360, 440));
                imag.Save(Server.MapPath("~/Content/userfiles/" + img.id + img.name));
            }
            return img;
        }
    }
}
