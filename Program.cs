using System;
using static P1.Product;
using System.Collections.Generic;

namespace P1
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = Database.CreateConnection();
            
            Database.CreateTable(conn);

            conn.InsertProducts(new List<Product>()
                {
                    new Product("Product 01", "Its small", "Yellow", new decimal(11.3)),
                    new Product("Product 02", "Its medium", "Blue", new decimal(1.1)),
                    new Product("Product 03", "Its large", "Green", new decimal(21.1)),
                    new Product("Product 04", "Its too big to handle", "Red", new decimal(31.1))
                }
            );
            
            var quit = false;

            while(!quit)
            {
                Console.WriteLine("1. List all products");
                Console.WriteLine("2. Insert a product");
                Console.WriteLine("3. Get a product");
                Console.WriteLine("4. Delete a product");
                Console.WriteLine("5. Quit");

                var input = Int16.Parse(Console.ReadLine());

                switch(input)
                {
                    case 1:
                    {
                        var products = conn.ReadProducts();
                        foreach (var p in products)
                        {
                            Console.WriteLine("______________________________________________");
                            Console.WriteLine(p.ToString() + "\n");
                        }
                    }
                        break;
                    case 2:
                    {
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        Console.Write("Description: ");
                        var desc = Console.ReadLine();
                        Console.Write("Colour: ");
                        var colour = Console.ReadLine();
                        Console.Write("Price: ");
                        var price = new Decimal(float.Parse(Console.ReadLine()));

                        var newProduct = new Product(name, desc, colour, price);
                        conn.InsertProducts(new List<Product>() {newProduct});
                    }
                        break;
                    case 3:
                    {
                        Console.Write("Search: ");
                        var name = Console.ReadLine();

                        var found = conn.FindProduct(name);
                        Console.WriteLine("_______________________________");
                        Console.WriteLine(found);
                    }
                        break;
                    case 4:
                    {
                        Console.Write("Delete: ");
                        var name = Console.ReadLine();

                        conn.DeleteProduct(name);
                    }
                        break;
                    case 5:
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong command. Try again");
                        break;
                }
            }
        }
    }
}
