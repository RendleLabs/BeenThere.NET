using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RocketStop.Models
{
    public class NewDockingViewModel : Docking
    {
        public List<SelectListItem> Bays { get; set; }
    }
}