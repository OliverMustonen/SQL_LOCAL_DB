using ConsoleApp.Models.Entities;
using ConsoleApp.Services;

var productService = new ProductService(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\.NET\C#(C-sharp)\Projects\SQL\ConsoleApp\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

var product = productService.GetOneProduct("A1");
if (product != null)
    Console.WriteLine($"{product.ArticleNumber}, {product.Title}, {product.Description}, {product.Price}");
else
    Console.WriteLine("No product was fond.");

Console.ReadKey();