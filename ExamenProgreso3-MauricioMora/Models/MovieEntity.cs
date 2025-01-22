using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ExamenProgreso3_MauricioMora.Models
{
    public class MovieEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string MainActor { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
        public string Mmora { get; set; } 
    }
}
