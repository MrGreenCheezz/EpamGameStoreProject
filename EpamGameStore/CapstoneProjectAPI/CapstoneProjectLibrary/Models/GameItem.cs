using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProjectLibrary.Models
{
    public class GameItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Genres { get; set; }
    }
}
