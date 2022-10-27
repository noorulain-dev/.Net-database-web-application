using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    /**
     * This static class should be a provider for an SQLite db
     * Make sure the package is included in your project
     * 
     * 
     * Run the following to install the package in your project directory :
     *      
     *          dotnet add package Microsoft.Data.Sqlite
     * 
     * Read documentation for Microsoft.Data.Sqlite
     * and complete the following functions.
     * 
     * 
     * Note "this" within the function parameter is called a method extension
     * Read : https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
     */
    static class Database
    {
        public static SqliteConnection CreateConnection() 
        {   
            return new SqliteConnection("Data Source=data.db");
        }

        public static void CreateTable(this SqliteConnection conn) 
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = 
            @"
                CREATE TABLE Product (
                    name TEXT,
                    desc TEXT,
                    colour TEXT,
                    price REAL,
                    PRIMARY KEY(name)
                )";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void InsertProducts(this SqliteConnection conn, IEnumerable<Product> products) 
        {
            conn.Open();
            
            foreach (var p in products){ 

                var command = conn.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO Product (name, desc, colour, price) 
                    VALUES($name, $desc, $colour, $price)
                ";

                command.Parameters.AddWithValue("$name", p.Name);
                command.Parameters.AddWithValue("$desc", p.Desc);
                command.Parameters.AddWithValue("$colour", p.Colour);
                command.Parameters.AddWithValue("$price", p.Price);
                
                command.ExecuteNonQuery();
            
            }

            conn.Close();
            
        }
        
        public static IEnumerable<Product> ReadProducts(this SqliteConnection conn) 
        {
            var products = new List<Product>();

            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = 
            @"
                SELECT name, desc, colour, price
                FROM product
            ";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    var desc = reader.GetString(1);
                    var colour = reader.GetString(2);
                    var price = new Decimal(reader.GetFloat(3));

                    products.Add(new Product(name, desc, colour, price));
                }
            }
            conn.Close();

            return products;

        }

        public static void DeleteProduct(this SqliteConnection conn, String name)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = 
            @"
                DELETE FROM Product WHERE name = $name
            ";

            command.Parameters.AddWithValue("$name", name);
            command.ExecuteNonQuery();

            conn.Close();
        }

        public static Product FindProduct(this SqliteConnection conn, String name)
        {
            var found = new Product("N/A", "N/A", "N/A", new Decimal(0));

            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = 
            @"
                SELECT
                name, desc, colour, price
                FROM Product WHERE name = $name
            ";

            command.Parameters.AddWithValue("$name", name);
            
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var namef = reader.GetString(0);
                    var desc = reader.GetString(1);
                    var colour = reader.GetString(2);
                    var price = new Decimal(reader.GetFloat(3));

                    found = new Product(namef, desc, colour, price);
                
                }
            }
            
            conn.Close();
            return found;
        }
    }
}
