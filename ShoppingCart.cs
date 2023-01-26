using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace RefactorDemo
{
    public class ShoppingCart
    {
       
    private List<Product> _products = new List<Product>();

    public void AddProduct(string name)
    {
        string dbPath = new DirectoryInfo(@"..\..").FullName + @"\GroceryStore.mdf";
        string connectionString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0}", dbPath);
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Product WHERE Name = '{0}'", name), conn);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        Product p = null;

        if (dr.Read())
        {
            _products.Add(new Product
            {
                Id = dr.GetFieldValue<int>("Id"),
                Name = dr.GetFieldValue<string>("Name"),
                Price = dr.GetFieldValue<decimal>("Price")
            });
        }
    }

    public decimal GetCheckoutPrice()
    {
        decimal price = 0;

        foreach (Product p in _products)
            price += p.Price;

        return price;
    }
}
    }
