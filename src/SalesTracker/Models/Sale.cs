using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTracker.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int Quantity { get; set; }
        public string SalesComment { get; set; }

        public Sale(int itemId, int quantity, int saleId = 0)
        {
            //UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
            SaleId = saleId;
            SalesComment = "Great job selling " + quantity + " of these!  Keep it up.";
        }
        public Sale() { }
    }
}
