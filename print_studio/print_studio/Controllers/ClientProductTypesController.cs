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
using System.Drawing.Imaging;
using System.IO;

namespace print_studio.Controllers
{
    public class ClientProductTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientProductTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = await db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sizes = db.Sizes.ToList();
            return View(productType);
        }

        // POST: ClientProductTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult View(string idColor, string idSize, HttpPostedFileBase upload)
        {
            var product = db.ColorsProducts.Find(int.Parse(idColor));
            if (upload != null && idColor != null && idSize != null)
            {
                ViewBag.idColor = idColor;
                ViewBag.idSize = idSize;
                ViewBag.product = product;

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string fileName = unixTimestamp.ToString() + System.IO.Path.GetFileName(upload.FileName);
                fileName = fileName.Replace(" ", "-");
                upload.SaveAs(Server.MapPath("~/Content/temp/" + fileName));
                Bitmap b1 = new Bitmap(System.Drawing.Image.FromFile(Server.MapPath("~/Content/temp/" + fileName)));
                Bitmap imag = new Bitmap(b1, new System.Drawing.Size(150, 150));
                Bitmap dress = new Bitmap(System.Drawing.Image.FromFile(Server.MapPath("~/Content/userfiles/"
                    + product.Image.id + product.Image.name)));
                imag.Save(Server.MapPath("~/Content/temp/" + "1" + fileName));
                Graphics myGraphic = Graphics.FromImage(dress);
                imag.MakeTransparent(Color.White);
                myGraphic.DrawImage(imag, 105, 145, 150, 150);
                dress.Save(Server.MapPath("~/Content/temp/" + "2" + fileName));

                ViewBag.file = ("2" + fileName);
                return View();
            }

            return RedirectToAction("Details", new { id = product.id_producttype });

        }


        // POST: ClientProductTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string idColor, string idSize, string file)
        {
                ViewBag.idColor = int.Parse(idColor);
                ViewBag.idSize = idSize;
                ViewBag.file = file;

                return View(new PrintOrder());

        }



        // POST: ClientProductTypes/CreateOrder
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrder([Bind(Include = "id,surname,name,adress,phone,id_size,count")]
        PrintOrder printOrder, string file, int id_colorproduct)
        {
            if (ModelState.IsValid)
            {
               
                printOrder.id_status = db.OrderStatus.Where(s => s.name == "Новый").First().id;

                Models.Image img = new Models.Image();
                img.name = file;
                db.Images.Add(img);
                await db.SaveChangesAsync();
                System.IO.File.Move(Server.MapPath("~/Content/temp/" + file),
                    Server.MapPath("~/Content/userfiles/" + img.id + img.name));

                SavedProduct sp = new SavedProduct();
                sp.id_image = img.id;
                sp.id_colorsproduct = id_colorproduct;
                sp.id = img.id;
                db.SavedProducts.Add(sp);
                await db.SaveChangesAsync();
                printOrder.id_savedproduct = sp.id;

                printOrder.date = DateTime.Now;
                db.PrintOrders.Add(printOrder);
                await db.SaveChangesAsync();
                return View(); 
            }

            ViewBag.idColor = id_colorproduct;
            ViewBag.idSize = printOrder.id_size;
            ViewBag.file = file;

            return RedirectToAction("CreateOrder");
        }
    }
}
