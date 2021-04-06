using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    [Table("Ores")]
    public class Ore : Shape
    {
        [Key]
        public int OREID { get; set; }

        public bool Hurt { get; set; }

        public int Value { get; set; }

        public int Score { get; set; }

        public string OreType { get; set; }

        public int Level { get; set; }

        public bool canPass { get; set; }
    }
}
