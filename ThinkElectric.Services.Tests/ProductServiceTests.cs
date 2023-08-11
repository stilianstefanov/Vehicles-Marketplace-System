namespace ThinkElectric.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Product;
using MongoDB.Bson;
using Web.ViewModels.Product;

using static DatabaseSeeder;

public class ProductServiceTests
{
    private DbContextOptions<ThinkElectricDbContext> _dbOptions;
    private ThinkElectricDbContext _dbContext;

    private IProductService _productService;


    [OneTimeSetUp]
    public void SetUp()
    {
        _dbOptions = new DbContextOptionsBuilder<ThinkElectricDbContext>()
            .UseInMemoryDatabase("ThinElectricInMemory" + Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ThinkElectricDbContext(this._dbOptions);

        _dbContext.Database.EnsureCreated();

        SeedDatabase(_dbContext);

        _productService = new ProductService(_dbContext);
    }

    [Test]
    public async Task CreateAsync_CreatesNewEntityProperly()
    {
        var testModel = new ProductCreateViewModel()
        {
            Name = "TestName",
            Price = 1,
            Quantity = 1,
        };

        var companyId = TestCompany.Id.ToString();

        var productType = ProductType.Scooter;
        var imageId = ObjectId.GenerateNewId().ToString();

        await _productService.CreateAsync(testModel, companyId!, imageId, productType);

        var actualEntity = await _dbContext
            .Products
            .Where(p => p.Name == testModel.Name)
            .Where(p => p.Price == testModel.Price)
            .Where(p => p.Quantity == testModel.Quantity)
            .Where(p => p.CompanyId.ToString() == companyId)
            .Where(p => p.ImageId == imageId)
            .Where(p => p.ProductType == productType)
            .FirstOrDefaultAsync();

        Assert.IsNotNull(actualEntity);
    }

    [Test]
    public async Task EditAsync_EditsEntityProperly()
    {
        var editModel = new ProductEditViewModel()
        {
            Name = "TestName2",
            Price = 2,
            Quantity = 2,
        };

        var productId = TestProduct.Id.ToString();

        await _productService.EditAsync(productId, editModel);

        var editedEntity = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == productId)
            .Where(p => p.Name == editModel.Name)
            .Where(p => p.Price == editModel.Price)
            .Where(p => p.Quantity == editModel.Quantity)
            .FirstOrDefaultAsync();

        Assert.IsNotNull(editedEntity);
    }

    [Test]
    public async Task DeleteAsync_DeletesEntityProperly()
    {
       var productId = TestProduct.Id.ToString();

        await _productService.DeleteAsync(productId);

        var deletedEntity = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == productId)
            .FirstAsync();

        Assert.That(deletedEntity.IsDeleted);
    }
}
