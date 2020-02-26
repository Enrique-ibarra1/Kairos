using System;

namespace Kairos.Models
{
    public class Watch
    {
        public int WatchId {get;set;}
        public string Name {get;set;}
        public string Company {get;set;}
        public double Price {get;set;}
        public string Color {get;set;}
        public string Size {get;set;}
        public string Material {get;set;}
        public string Description {get;set;}
        public string Gender{get;set;}
        public string ImageUrl {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}