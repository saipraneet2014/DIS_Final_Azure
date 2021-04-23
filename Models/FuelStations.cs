using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DIS_Final_Azure.Models
{
    public class Fuel_Stations
    {
        public string ID { get; set; }
        public string Station_name { get; set; }
        public string Fuel_type { get; set; }
        public string Owner_type_code { get; set; }
        public string Street_address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public ICollection<EV> Station_EV_ID { get; set; }
        public ICollection<Federal_Agency> Federal_ID { get; set; }
    }

    public class EV
    {
        public string Station_EV_ID { get; set; }
        public string ID { get; set; }
        public string Ports { get; set; }
        public string Posts { get; set; }
        public Fuel_Stations station { get; set; }
    }

    public class Federal_Agency_Stations
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ICollection<Fuel_Stations> ID { get; set; }
        public ICollection<Federal_Agency> Federal_ID { get; set; }
    }

    public class Federal_Agency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Federal_ID { get; set; }
        public string Federal_name { get; set; }
    }
    
    public class CreateStation
    {
        public string ID { get; set; }
        [Required]
        [Url]
        public string Station_name { get; set; }
        [Required]
        public string Fuel_type { get; set; }
        [Required]
        public string Owner_type_code { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public ICollection<EV> Station_EV_ID { get; set; }
        [Required]
        public ICollection<Federal_Agency> Federal_ID { get; set; }
    }


    //DBcontext//
}




