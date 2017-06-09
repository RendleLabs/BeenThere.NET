using System.ComponentModel.DataAnnotations;

namespace RocketStop.DockingService.Models
{
    public class Docking
    {
        public int Id { get; set; }

        [MaxLength(16), Required]
        public string Bay { get; set; }

        [MaxLength(128), Required]
        public string ShipName { get; set; }
    }
}