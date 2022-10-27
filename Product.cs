using System;

namespace P1
{
    class Product
    {

        public String Name
        {
            get;
            set;
        }

        public string Desc
        {
            get;
            set;
        }

        public string Colour
        {
            get;
            set;
        }

        public Decimal Price
        {
            get;
            set;
        }

        public Product(string name, string desc, string colour, Decimal price)
        {
            Name = name;
            Desc = desc;
            Colour = colour;
            Price = price;
        }

        public override string ToString()
        {
            // To be modified
            // You can choose to print in json string or any other human readable format
            return "The product name is: " + Name + "\nIts price is " + Price + "\nIts colour is " + Colour +  "\n" + "Further description: " + Desc;
        }
        
    }
}
