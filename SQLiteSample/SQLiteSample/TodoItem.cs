using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace SQLiteSample
{
    [Table("User")]
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }             // <-1
        public string Text { get; set; }        // <-2
        public DateTime CreatedAt { get; set; } // <-3
        public bool Delete { get; set; }        // <-4
    }
}
