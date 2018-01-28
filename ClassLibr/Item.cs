using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibr
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
        public int Price { get; set; }
        public EnumTypeItem Type { get; set; }

        public Item()
        {
            Id = Guid.NewGuid();
        }

        public Item(string name, double count, int price, EnumTypeItem type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Count = count;
            Price = price;
        }

        override public string ToString()
        {
            return Name;
        }
    }
}
