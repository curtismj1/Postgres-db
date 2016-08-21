using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using System.Drawing;

namespace PostGres_Database.Models
{
    public class CarModel
    {
        public CarModel()
        {
            Images = new List<Image>();
            ImagePaths = new List<string>();
        }
        public int tbVehicle_pkey { get; set; }
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Wheel Drive")]
        public int WheelDrive { get; set; }
        [Display(Name = "Condition")]
        public int Condition { get; set; }
        [Display(Name = "VIN")]
        public int VIN { get; set; }
        [Display(Name = "Miles Per Gallon")]
        public int MPGFuelRating { get; set; }
        [Display(Name = "Price Range")]
        public int PriceRange { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Display(Name = "Mileage")]
        public string Mileage { get; set; }
        [Display(Name = "Transmission Type")]
        public string TransmissionType { get; set; }
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        [Display(Name = "Car Type")]
        public string CarType { get; set; }
        [Display(Name = "Comment")]
        public string Comment { get; set; } 
        public string ManualLatitude { get; set; }
        public string ManualLongutude { get; set; }
        public List<Image> Images { get; set; } 
        public List<string> ImagePaths { get; set; }
    }
}