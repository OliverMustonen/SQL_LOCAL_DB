using ConsoleApp.Models.Entities;
using Microsoft.Data.SqlClient;

namespace ConsoleApp.Services;

internal class ProductService(string connectionString)
{
    private readonly string _connectionString = connectionString;


    /// <summary>
    /// This will create a new product and saves it to the database if no product with the same article number exists.
    /// </summary>
    /// <param name="entity">The product entity that will be saved to the database</param>
    /// <returns>Returns a string result value</returns>
    public string CreateProduct(ProductEntity entity)
    {

        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber) BEGIN INSERT INTO Products VALUES(@ArticleNumber, @Title, @Description, @Price) SELECT 'Product was created' AS MESSAGE END ELSE SELECT 'Product already exists' AS MESSAGE", conn);
        cmd.Parameters.AddWithValue("@ArticleNumber", entity.ArticleNumber);
        cmd.Parameters.AddWithValue("@Title", entity.Title);
        cmd.Parameters.AddWithValue("@Description", entity.Description ?? null);
        cmd.Parameters.AddWithValue("@Price", entity.Price);

        var result = cmd.ExecuteScalar().ToString();
        return result!;
    }


    public IEnumerable<ProductEntity> GetProducts()
    {
        var products = new List<ProductEntity>();

        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Products", conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            products.Add(new ProductEntity()
            {
                ArticleNumber = reader.GetString(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Price = reader.GetDecimal(3),
            });
        }

        return products;
    }

    public ProductEntity GetOneProduct(string articleNumber)
    {
        var productEntity = new ProductEntity();

        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = new SqlCommand("IF EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber) SELECT * FROM Products WHERE ArticleNumber = @ArticleNumber ELSE SELECT 'No product Found' AS Message", conn);
        cmd.Parameters.AddWithValue("@ArticleNumber", articleNumber);

        using var reader = cmd.ExecuteReader();
        if (reader.FieldCount == 1)
            return null!;

        while (reader.Read())
        {
            productEntity = new ProductEntity()
            {
                ArticleNumber = reader.GetString(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Price = reader.GetDecimal(3),
            };
        }

        return productEntity;
    }


}
