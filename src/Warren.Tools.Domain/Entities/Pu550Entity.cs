using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warren.Tools.Domain.Models
{
    [Table("pu_550")]
    [Keyless]
    public class Pu550Entity
    {
        [Column("bondCode")]
        public int BondCode { get; set; }

        [Column("maturity")]
        public DateTime Maturity { get; set; }

        [Column("unitprice")]
        public double UnitPrice { get; set; }

        [Column("movementDate")]
        public DateTime MovementDate { get; set; }

        
    }
}
