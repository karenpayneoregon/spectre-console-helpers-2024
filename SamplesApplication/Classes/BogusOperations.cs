using Bogus;
using SamplesApplication.Models;

namespace SamplesApplication.Classes;
internal class BogusOperations
{
    /// <summary>
    /// Generates a list of fake products.
    /// </summary>
    /// <param name="productCount">The number of products to generate. Defaults to 10 if not specified.</param>
    /// <returns>A list of generated <see cref="Product"/> objects, ordered by product name.</returns>
    public static List<Product> Products(int productCount = 10)
    {
        int identifier = 1;
        Faker<Product> fake = new Faker<Product>()
            .CustomInstantiator(f => new Product(identifier++))
            .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
            .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10, 2))
            .RuleFor(p => p.UnitsInStock, f => f.Random.Short(1, 5));

        return fake.Generate(productCount).OrderBy(x => x.ProductName).ToList();
    }
}
