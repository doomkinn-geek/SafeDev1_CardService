using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardService.Data
{
    [Table("Cards")]
    public class Card
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Column]
        [StringLength(20)]
        public string CardNo { get; set; }

        [Column]
        [StringLength(50)]
        public string? Name { get; set; }

        [Column]
        [StringLength(50)]
        public string? CVV2 { get; set; }

        [Column]
        public DateTime ExpDate { get; set; }

        public virtual Client Client { get; set; }
    }
}
