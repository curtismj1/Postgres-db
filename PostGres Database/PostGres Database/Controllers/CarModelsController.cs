using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PostGres_Database.Models;
using Npgsql;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.IO;
using System.Drawing;

namespace PostGres_Database.Controllers
{
    public class CarModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=postgres;Database=postgres;");
   
        public CarModelsController()
        {
            
            conn.Open();

        }
        // GET: CarModels
        public ActionResult Index()
        {
            return View(db.CarModels.ToList());
        }

        // GET: CarModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = db.CarModels.Find(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        // GET: CarModels/Create
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Upload([Bind(Include ="ImagePaths, Make")]CarModel carModel, HttpPostedFileBase uploadedImage)
        {
         
            WebImage photo = null;
            var newFileName = "";
            var imagePath = "";
            photo = WebImage.GetImageFromRequest();
            if (photo != null)
            {
                newFileName = Guid.NewGuid().ToString() + "_" +
                    Path.GetFileName(photo.FileName);
                imagePath = @"images\" + newFileName;

                photo.Save(@"~\" + imagePath);
                carModel.ImagePaths.Add(imagePath);
            }
            if (uploadedImage != null && uploadedImage.ContentLength > 0)
            {
                carModel.Images.Add(Image.FromStream(uploadedImage.InputStream, true, true));
            }
            return View("Create", carModel);
        }
        private string Sanitize(string input)
        {
            if(input == null) { return ""; }
            string test = input.Replace("'", @"''");
            return test;
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,tbVehicle_pkey,WheelDrive,Condition,VIN,MPGFuelRating,PriceRange,Make,Model,Year,Color,Mileage,TransmissionType,Latitude,Longitude,CarType,Comment,ManualLatitude,ManualLongutude,ImagePaths")] CarModel carModel)
        {


            try
            {
                /*WebRequest request = WebRequest.Create("https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyBk5FAZnwz6pOV2TUAuV9krx8b_Hz4vmGM ");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"user\":\"test\"," +
                                  "\"password\":\"bla\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //conn.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = $@"INSERT INTO ""tbVehicle"" VALUES('{carModel.ID}','{Sanitize(carModel.Make)}','{Sanitize(carModel.Model)}','{Sanitize(carModel.Year)}', '{Sanitize(carModel.Color)}', '{Sanitize(carModel.Mileage)}', '{carModel.WheelDrive}', '{carModel.Condition}', '{carModel.VIN}','{carModel.MPGFuelRating}','{Sanitize(carModel.TransmissionType)}','{Sanitize(carModel.Latitude)}', '{Sanitize(carModel.Longitude)}','{carModel.PriceRange}', '{Sanitize(carModel.CarType)}', '{Sanitize(carModel.Comment)}')";
                try{
                    cmd.ExecuteNonQuery();
                } 
                catch(HttpException e)
                {
                    Console.Write(e.GetHtmlErrorMessage());
                }
            }

            if (ModelState.IsValid)
            {    
                db.CarModels.Add(carModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carModel);
        }

        // GET: CarModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = new CarModel();
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,tbVehicle_pkey,WheelDrive,Condition,VIN,MPGFuelRating,PriceRange,Make,Model,Year,Color,Mileage,TransmissionType,Latitude,Longitude,CarType,Comment,ManualLatitude,ManualLongutude")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = db.CarModels.Find(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModel carModel = db.CarModels.Find(id);
            db.CarModels.Remove(carModel);
            db.SaveChanges();
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
