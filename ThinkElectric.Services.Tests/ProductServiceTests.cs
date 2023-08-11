#pragma warning disable NUnit2005
namespace ThinkElectric.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models.Enums.Product;
using MongoDB.Bson;
using Web.ViewModels.Product;

using static DatabaseSeeder;

public class ProductServiceTests
{
    private DbContextOptions<ThinkElectricDbContext> _dbOptions;
    private ThinkElectricDbContext _dbContext;

    private IProductService _productService;


    [SetUp]
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
    public async Task EditAsync_ThrowsWhenEntityDoesNotExist()
    {
        var editModel = new ProductEditViewModel()
        {
            Name = "TestName2",
            Price = 2,
            Quantity = 2,
        };

        var productId = Guid.NewGuid().ToString();

        Assert.That(async () => await _productService.EditAsync(productId, editModel),
                                  Throws.InvalidOperationException);
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

    [Test]
    public async Task DeleteAsync_ThrowsWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.That(async () => await _productService.DeleteAsync(productId),
                                             Throws.InvalidOperationException);
    }

    [Test]
    public async Task GetProductDetailsByIdAsync_ReturnsCorrectEntity()
    {
        var productId = TestProduct.Id.ToString();

        var resultEntity = await _productService.GetProductDetailsByIdAsync(productId);

        Assert.AreEqual(TestProduct.Name, resultEntity.Name);
        Assert.AreEqual(TestProduct.Price.ToString("f2"), resultEntity.Price);
        Assert.AreEqual(TestProduct.Quantity, resultEntity.Quantity);
        Assert.AreEqual(TestProduct.CompanyId.ToString(), resultEntity.CompanyId);
        Assert.AreEqual(TestProduct.Company.Name, resultEntity.CompanyName);
        Assert.AreEqual(TestProduct.ImageId, resultEntity.ImageId);
        Assert.AreEqual("0", resultEntity.Rating);
    }

    [Test]
    public async Task GetProductDetailsByIdAsync_ThrowsWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.That(async () => await _productService.GetProductDetailsByIdAsync(productId),
                       Throws.InvalidOperationException);
    }

    [Test]
    public async Task GetProductByIdAsync_ReturnsCorrectEntity()
    {
        var resultEntity = await _productService.GetProductByIdAsync(TestProduct.Id.ToString());

        Assert.AreEqual(TestProduct.Id, resultEntity!.Id);
    }

    [Test]
    public async Task GetProductByIdAsync_ReturnsNullWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.IsNull(await _productService.GetProductByIdAsync(productId));
    }

    [Test]
    public async Task GetProductEditViewModelByIdAsync_ReturnsCorrectEntity()
    {
        var productId = TestProduct.Id.ToString();

        var resultEntity = await _productService.GetProductEditViewModelByIdAsync(productId);

        Assert.AreEqual(TestProduct.Name, resultEntity.Name);
        Assert.AreEqual(TestProduct.Price, resultEntity.Price);
        Assert.AreEqual(TestProduct.Quantity, resultEntity.Quantity);
        Assert.AreEqual(TestProduct.ImageId, resultEntity.ImageId);
    }

    [Test]
    public async Task GetProductEditViewModelByIdAsync_ThrowsWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.That(async () => await _productService.GetProductEditViewModelByIdAsync(productId),
                                  Throws.InvalidOperationException);
    }

    [Test]
    public async Task GetImageIdByProductIdAsync_ReturnsCorrectImageId()
    {
        var resultImageId = await _productService.GetImageIdByProductIdAsync(TestProduct.Id.ToString());

        Assert.AreEqual(TestProduct.ImageId, resultImageId);
    }

    [Test]
    public async Task GetImageIdByProductIdAsync_ThrowsWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.That(async () => await _productService.GetImageIdByProductIdAsync(productId),
                                             Throws.InvalidOperationException);
    }

    [Test]
    public async Task ProductExistsAsync_ReturnsTrueWhenEntityExists()
    {
        var productId = TestProduct.Id.ToString();

        Assert.IsTrue(await _productService.ProductExistsAsync(productId));
    }

    [Test]
    public async Task ProductExistsAsync_ReturnsFalseWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.IsFalse(await _productService.ProductExistsAsync(productId));
    }

    [Test]
    public async Task DecreaseQuantityAsync_DecreasesProductsQuantityWithOrderItemsQuantityCorrectly()
    {
        var expectedTestProductQuantity = TestProduct.Quantity - TestOrderItem1.Quantity;

        var expectedTestProduct2Quantity = TestProduct2.Quantity - TestOrderItem2.Quantity;

        await _productService.DecreaseQuantityAsync(TestOrder.Id.ToString());

        var testProduct = await _dbContext
            .Products
            .Where(p => p.Id == TestProduct.Id)
            .Select(p => p.Quantity)
            .FirstOrDefaultAsync();

        var testProduct2 = await _dbContext
            .Products
            .Where(p => p.Id == TestProduct2.Id)
            .Select(p => p.Quantity)
            .FirstOrDefaultAsync();

        Assert.AreEqual(expectedTestProductQuantity, testProduct);
        Assert.AreEqual(expectedTestProduct2Quantity, testProduct2);
    }

    [Test]
    public async Task HasProductQuantityAsync_ReturnsTrueWhenProductHasQuantity()
    {
        var productId = TestProduct.Id.ToString();

        Assert.IsTrue(await _productService.HasProductQuantityAsync(productId));
    }

    [Test]
    public async Task HasProductQuantityAsync_ReturnsFalseWhenProductDoesNotHaveQuantity()
    {
        var productId = TestProduct.Id.ToString();

        var testProduct = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == productId)
            .FirstAsync();

        testProduct.Quantity = 0;

        await _dbContext.SaveChangesAsync();

        Assert.IsFalse(await _productService.HasProductQuantityAsync(productId));
    }

    [Test]
    public async Task IsUserAuthorizedAsync_ReturnsTrueWhenUserIsAuthorized()
    {
        var productId = TestProduct.Id.ToString();

        var companyId = TestProduct.CompanyId.ToString();

        Assert.IsTrue(await _productService.IsUserAuthorizedAsync(productId, companyId));
    }

    [Test]
    public async Task IsUserAuthorizedAsync_ReturnsFalseWhenUserIsNotAuthorized()
    {
        var productId = TestProduct.Id.ToString();

        var companyId = Guid.NewGuid().ToString();

        Assert.IsFalse(await _productService.IsUserAuthorizedAsync(productId, companyId));
    }

    [Test]
    public async Task DeleteAllProductsByCompanyIdAsync_DeletesAllProductsByCompanyId()
    {
        var companyId = TestProduct.CompanyId.ToString();

        await _productService.DeleteAllProductsByCompanyIdAsync(companyId);

        var deletedProducts = await _dbContext
            .Products
            .Where(p => p.CompanyId.ToString() == companyId)
            .ToListAsync();

        foreach (var product in deletedProducts)
        {
            Assert.That(product.IsDeleted);
        }
    }

    [Test]
    public async Task RestoreAllProductsByCompanyIdAsync_RestoresAllProductsByCompanyId()
    {
        var companyId = TestProduct.CompanyId.ToString();

        await _productService.DeleteAllProductsByCompanyIdAsync(companyId);

        await _productService.RestoreAllProductsByCompanyIdAsync(companyId);

        var restoredProducts = await _dbContext
            .Products
            .Where(p => p.CompanyId.ToString() == companyId)
            .ToListAsync();

        foreach (var product in restoredProducts)
        {
            Assert.That(!product.IsDeleted);
        }
    }

    [Test]
    public async Task GetAllProductsForAdminAsync_ReturnsCorrectEntities()
    {
        var resultEntities = await _productService.GetAllProductsForAdminAsync();

        var expectedCount = await _dbContext
            .Products
            .CountAsync();

        Assert.AreEqual(expectedCount, resultEntities.Count());

        if (resultEntities.Any())
        {

            var containsTestProduct = resultEntities.Any(entity => entity.Id == TestProduct.Id.ToString());
            var containsTestProduct2 = resultEntities.Any(entity => entity.Id == TestProduct2.Id.ToString());
            var containsTestProduct3 = resultEntities.Any(entity => entity.Id == TestProduct3.Id.ToString());

            Assert.IsTrue(containsTestProduct);
            Assert.IsTrue(containsTestProduct2);
            Assert.IsTrue(containsTestProduct3);
        }
    }

    [Test]
    public async Task ProductExistsForAdminAsync_ReturnsTrueWhenEntityExists()
    {
        var productId = TestProduct.Id.ToString();

        Assert.IsTrue(await _productService.ProductExistsForAdminAsync(productId));
    }

    [Test]
    public async Task ProductExistsForAdminAsync_ReturnsFalseWhenEntityDoesNotExist()
    {
        var productId = Guid.NewGuid().ToString();

        Assert.IsFalse(await _productService.ProductExistsForAdminAsync(productId));
    }

    [Test]
    public async Task RestoreProductAsync_RestoresProductCorrectly()
    {
        var productId = TestProduct.Id.ToString();

        await _productService.DeleteAsync(productId);

        await _productService.RestoreProductAsync(productId);

        var product = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == productId)
            .FirstAsync();

        Assert.That(!product.IsDeleted);
    }
}
