using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Models;

namespace JobPortal.ViewModel
{
    public class CountryViewModel
    {
        public Country country { get; set; }
        public State state { get; set; }
        public City city { get; set; }
        
    }
}