#pragma warning disable CS8618
namespace ThinkElectric.Tests.Services;

using Microsoft.EntityFrameworkCore;

using Data;
using ThinkElectric.Services;
using ThinkElectric.Services.Contracts;
using Web.ViewModels.CartItem;

using static DatabaseSeeder;

public class CartServiceTests
{
    private DbContextOptions<ThinkElectricDbContext> _dbOptions;
    private ThinkElectricDbContext _dbContext;

    private ICartService _cartService;

    [SetUp]
    public void SetUp()
    {
        _dbOptions = new DbContextOptionsBuilder<ThinkElectricDbContext>()
            .UseInMemoryDatabase("ThinElectricInMemory" + Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ThinkElectricDbContext(_dbOptions);

        _dbContext.Database.EnsureCreated();

        SeedDatabase(_dbContext);

        _cartService = new CartService(_dbContext);
    }

    [Test]
    public async Task CreateAsync_ShouldCreateCart()
    {
        var userId = BuyerUser.Id;

        var cartId = await _cartService.CreateAsync(userId);

        bool cartExists = await _dbContext.Carts
            .AnyAsync(c => c.Id.ToString() == cartId);

        Assert.IsNotNull(cartId);
        Assert.IsTrue(cartExists);
    }

    [Test]
    public async Task AddToCartAsync_ShouldAddCartItem()
    {
        var userId = BuyerUser.Id.ToString();

        await _cartService.AddToCartAsync(TestProduct.Id.ToString(), userId);

        bool cartItemExists = await _dbContext.CartItems
            .AnyAsync(ci => ci.ProductId == TestProduct.Id && ci.Cart.UserId.ToString() == userId);

        Assert.IsTrue(cartItemExists);
    }

    [Test]
    public async Task CartItemExistsAsync_ShouldReturnTrue()
    {
        var cartItemId = TestCartItem.Id.ToString();

        bool cartItemExists = await _cartService.CartItemExistsAsync(cartItemId);

        Assert.IsTrue(cartItemExists);
    }

    [Test]
    public async Task CartItemExistsAsync_ShouldReturnFalse()
    {
        var cartItemId = Guid.NewGuid().ToString();

        bool cartItemExists = await _cartService.CartItemExistsAsync(cartItemId);

        Assert.IsFalse(cartItemExists);
    }

    [Test]
    public async Task IsUserAuthorizedAsync_ShouldReturnTrue()
    {
        var cartItemId = TestCartItem.Id.ToString();
        var userId = BuyerUser.Id.ToString();

        bool isUserAuthorized = await _cartService.IsUserAuthorizedAsync(cartItemId, userId);

        Assert.IsTrue(isUserAuthorized);
    }

    [Test]
    public async Task IsUserAuthorizedAsync_ShouldReturnFalse()
    {
        var cartItemId = TestCartItem.Id.ToString();
        var userId = CompanyUser.Id.ToString();

        bool isUserAuthorized = await _cartService.IsUserAuthorizedAsync(cartItemId, userId);

        Assert.IsFalse(isUserAuthorized);
    }

    [Test]
    public async Task RemoveFromCartAsync_ShouldRemoveCartItem()
    {
        var cartItemId = TestCartItem.Id.ToString();

        await _cartService.RemoveFromCartAsync(cartItemId);

        bool cartItemExists = await _dbContext.CartItems
            .AnyAsync(ci => ci.Id.ToString() == cartItemId);

        Assert.IsFalse(cartItemExists);
    }

    [Test]
    public void RemoveFromCartAsync_ShouldThrowException()
    {
        var cartItemId = Guid.NewGuid().ToString();

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _cartService.RemoveFromCartAsync(cartItemId));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnCartItems()
    {
        var userId = BuyerUser.Id.ToString();

        var cartItems = await _cartService.GetAllAsync(userId);

        bool testCartItemExists = cartItems.Any(ci => ci.Id == TestCartItem.Id.ToString());
        bool testCartItem2Exists = cartItems.Any(ci => ci.Id == TestCartItem2.Id.ToString());

        Assert.IsNotNull(cartItems);
        Assert.IsTrue(cartItems.Any());
        Assert.IsTrue(testCartItemExists);
        Assert.IsTrue(testCartItem2Exists);
    }

    [Test]
    public async Task ProductAlreadyAdded_ShouldReturnTrue()
    {
        var userId = BuyerUser.Id.ToString();

        bool productAlreadyAdded = await _cartService.ProductAlreadyAdded(TestProduct.Id.ToString(), userId);

        Assert.IsTrue(productAlreadyAdded);
    }

    [Test]
    public async Task ProductAlreadyAdded_ShouldReturnFalse()
    {
        var userId = BuyerUser.Id.ToString();

        bool productAlreadyAdded = await _cartService.ProductAlreadyAdded(TestProduct3.Id.ToString(), userId);

        Assert.IsFalse(productAlreadyAdded);
    }

    [Test]
    public async Task AreCartItemsValidAsync_ShouldReturnTrue()
    {
        var cartItems = new List<CartItemViewModel>();

        var cartItem = new CartItemViewModel
        {
            ProductId = TestProduct.Id.ToString(),
            Quantity = TestProduct.Quantity
        };

        var cartItem2 = new CartItemViewModel
        {
            ProductId = TestProduct2.Id.ToString(),
            Quantity = TestProduct2.Quantity
        };

        cartItems.Add(cartItem);
        cartItems.Add(cartItem2);

        bool areCartItemsValid = await _cartService.AreCartItemsValidAsync(cartItems);

        Assert.IsTrue(areCartItemsValid);
    }


    [Test]
    public async Task AreCartItemsValidAsync_ShouldReturnFalseWhenInvalidProductId()
    {
        var cartItems = new List<CartItemViewModel>();

        var cartItem = new CartItemViewModel
        {
            ProductId = Guid.NewGuid().ToString(),
            Quantity = TestProduct.Quantity
        };

        var cartItem2 = new CartItemViewModel
        {
            ProductId = TestProduct2.Id.ToString(),
            Quantity = TestProduct2.Quantity
        };

        cartItems.Add(cartItem);
        cartItems.Add(cartItem2);

        bool areCartItemsValid = await _cartService.AreCartItemsValidAsync(cartItems);

        Assert.IsFalse(areCartItemsValid);
    }

    [Test]
    public async Task AreCartItemsValidAsync_ShouldReturnFalseWhenInvalidQuantity()
    {
        var cartItems = new List<CartItemViewModel>();

        var cartItem = new CartItemViewModel
        {
            ProductId = TestProduct.Id.ToString(),
            Quantity = TestProduct.Quantity
        };

        var cartItem2 = new CartItemViewModel
        {
            ProductId = TestProduct2.Id.ToString(),
            Quantity = TestProduct2.Quantity + 1
        };

        cartItems.Add(cartItem);
        cartItems.Add(cartItem2);

        bool areCartItemsValid = await _cartService.AreCartItemsValidAsync(cartItems);

        Assert.IsFalse(areCartItemsValid);
    }

    [Test]
    public async Task ClearAsync_ShouldClearCart()
    {
        var userId = BuyerUser.Id.ToString();

        await _cartService.ClearAsync(userId);

        bool cartExists = await _dbContext.Carts
            .AnyAsync(c => c.UserId.ToString() == userId);

        bool cartItemsExist = await _dbContext.CartItems
            .AnyAsync(ci => ci.Cart.UserId.ToString() == userId);

        Assert.IsTrue(cartExists);
        Assert.IsFalse(cartItemsExist);
    }
}
