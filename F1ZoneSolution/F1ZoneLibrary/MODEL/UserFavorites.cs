using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class UserFavorites
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ItemType { get; set; }
        public int ItemId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //adatbázis által generált érték
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
