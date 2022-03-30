using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Text { get; set; } = "";
        public int Priority { get; set; }
        public string DueDate { get; set; } = "";
        public string DueTime { get; set; } = "";
        public bool Status { get; set; }

    }
}