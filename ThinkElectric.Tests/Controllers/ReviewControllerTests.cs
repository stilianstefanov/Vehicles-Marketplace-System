#pragma warning disable CS8602
#pragma warning disable NUnit2005
#pragma warning disable CS8618
namespace ThinkElectric.Tests.Controllers;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

using ThinkElectric.Services.Contracts;
using Web.ViewModels.Review;
using Web.Controllers;

public class ReviewControllerTests
{
    private Mock<IProductService> _productServiceMock;
    private Mock<IReviewService> _reviewServiceMock;
    private Mock<ICompanyService> _companyServiceMock;
    private ReviewController _reviewController;

    [SetUp]
    public void Setup()
    {
        _productServiceMock = new Mock<IProductService>();
        _reviewServiceMock = new Mock<IReviewService>();
        _companyServiceMock = new Mock<ICompanyService>();
        _reviewController = new ReviewController(_reviewServiceMock.Object, _productServiceMock.Object, _companyServiceMock.Object);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "user_id"),
            new Claim("cartId", "cart_id")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var controllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            }
        };

        _reviewController = new ReviewController(_reviewServiceMock.Object, _productServiceMock.Object, _companyServiceMock.Object)
        {
            ControllerContext = controllerContext
        };

        _reviewController.TempData =
            new TempDataDictionary(controllerContext.HttpContext, Mock.Of<ITempDataProvider>());
    }

    [Test]
    public async Task AddToProductGet_ProductExists_ReturnsView()
    {
        // Arrange
        const string productId = "product_id";
        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _reviewController.AddToProduct(productId) as ViewResult;

        // Assert
        Assert.IsNotNull(result);

        _productServiceMock.Verify(s => s.ProductExistsAsync(productId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task AddToProductGet_ProductDoesNotExist_RedirectsToHome()
    {
        // Arrange
        const string productId = "non_existent_product_id";
        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(false);

        // Act
        var result = await _reviewController.AddToProduct(productId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result!.ActionName);
        Assert.AreEqual("Home", result.ControllerName);

        _productServiceMock.Verify(s => s.ProductExistsAsync(productId), Times.Once);
    }

    [Test]
    public async Task AddToProductGet_ProductAlreadyReviewed_RedirectsToProductDetails()
    {
        // Arrange
        const string productId = "product_id";
        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _reviewController.AddToProduct(productId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ActionName);
        Assert.AreEqual("Product", result.ControllerName);
        Assert.AreEqual(productId, result.RouteValues!["id"]);

        _productServiceMock.Verify(s => s.ProductExistsAsync(productId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task AddToProductPost_ValidReview_RedirectsToProductDetails()
    {
        // Arrange
        const string productId = "product_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>())).ReturnsAsync(false);
        _reviewServiceMock.Setup(s => s.AddToProductAsync(reviewModel, productId, It.IsAny<string>())).Returns(Task.CompletedTask);

        // Act
        var result = await _reviewController.AddToProduct(reviewModel, productId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ActionName);
        Assert.AreEqual("Product", result.ControllerName);
        Assert.AreEqual(productId, result.RouteValues["id"]);

        _productServiceMock.Verify(s => s.ProductExistsAsync(productId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>()), Times.Once);
        _reviewServiceMock.Verify(s => s.AddToProductAsync(reviewModel, productId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task AddToProductPost_InvalidReview_ReturnsView()
    {
        // Arrange
        const string productId = "product_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "Inv",
            Rating = 12
        };

        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(true);

        _reviewController.ModelState.AddModelError("Key", "Error message");

        // Act
        var result = await _reviewController.AddToProduct(reviewModel, productId) as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        
      
        _productServiceMock.VerifyNoOtherCalls();
        _reviewServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task AddToProductPost_ProductDoesNotExist_RedirectsToHome()
    {
        // Arrange
        const string productId = "non_existent_product_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(false);

        // Act
        var result = await _reviewController.AddToProduct(reviewModel, productId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual("Home", result.ControllerName);

        _productServiceMock.Verify(s => s.ProductExistsAsync(productId), Times.Once);
        _reviewServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task AddToProductPost_ProductAlreadyReviewed_RedirectsToProductDetails()
    {
        // Arrange
        const string productId = "product_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _productServiceMock.Setup(s => s.ProductExistsAsync(productId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _reviewController.AddToProduct(reviewModel, productId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ActionName);
        Assert.AreEqual("Product", result.ControllerName);
        Assert.AreEqual(productId, result.RouteValues["id"]);

        _productServiceMock.Verify(s => s.ProductExistsAsync(productId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedProductAsync(productId, It.IsAny<string>()), Times.Once);
        
        _reviewServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task AddToCompanyGet_CompanyExists_ReturnsView()
    {
        // Arrange
        const string companyId = "company_id";
        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _reviewController.AddToCompany(companyId) as ViewResult;

        // Assert
        Assert.IsNotNull(result);

        _companyServiceMock.Verify(s => s.CompanyExistsByIdAsync(companyId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task AddToCompanyGet_CompanyDoesNotExist_RedirectsToHome()
    {
        // Arrange
        const string companyId = "non_existent_company_id";
        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(false);

        // Act
        var result = await _reviewController.AddToCompany(companyId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual("Home", result.ControllerName);

        _companyServiceMock.Verify(s => s.CompanyExistsByIdAsync(companyId), Times.Once);
    }

    [Test]
    public async Task AddToCompanyGet_CompanyAlreadyReviewed_RedirectsToCompanyDetails()
    {
        // Arrange
        const string companyId = "company_id";
        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _reviewController.AddToCompany(companyId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ActionName);
        Assert.AreEqual("Company", result.ControllerName);
        Assert.AreEqual(companyId, result.RouteValues["id"]);

        _companyServiceMock.Verify(s => s.CompanyExistsByIdAsync(companyId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task AddToCompanyPost_ValidReview_RedirectsToCompanyDetails()
    {
        // Arrange
        const string companyId = "company_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>())).ReturnsAsync(false);
        _reviewServiceMock.Setup(s => s.AddToCompanyAsync(reviewModel, companyId, It.IsAny<string>())).Returns(Task.CompletedTask);

        // Act
        var result = await _reviewController.AddToCompany(reviewModel, companyId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ActionName);
        Assert.AreEqual("Company", result.ControllerName);
        Assert.AreEqual(companyId, result.RouteValues["id"]);

        _companyServiceMock.Verify(s => s.CompanyExistsByIdAsync(companyId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>()), Times.Once);
        _reviewServiceMock.Verify(s => s.AddToCompanyAsync(reviewModel, companyId, It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task AddToCompanyPost_InvalidReview_ReturnsView()
    {
        // Arrange
        const string companyId = "company_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(true);

        _reviewController.ModelState.AddModelError("Key", "Error message");

        // Act
        var result = await _reviewController.AddToCompany(reviewModel, companyId) as ViewResult;

        // Assert
        Assert.IsNotNull(result);

        _reviewServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task AddToCompanyPost_CompanyDoesNotExist_RedirectsToHome()
    {
        // Arrange
        const string companyId = "non_existent_company_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(false);

        // Act
        var result = await _reviewController.AddToCompany(reviewModel, companyId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual("Home", result.ControllerName);

        _companyServiceMock.Verify(s => s.CompanyExistsByIdAsync(companyId), Times.Once);
        _reviewServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task AddToCompanyPost_CompanyAlreadyReviewed_RedirectsToCompanyDetails()
    {
        // Arrange
        const string companyId = "company_id";
        var reviewModel = new ReviewAddViewModel
        {
            Content = "TestReview Content",
            Rating = 5
        };

        _companyServiceMock.Setup(s => s.CompanyExistsByIdAsync(companyId)).ReturnsAsync(true);
        _reviewServiceMock.Setup(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _reviewController.AddToCompany(reviewModel, companyId) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Details", result.ActionName);
        Assert.AreEqual("Company", result.ControllerName);
        Assert.AreEqual(companyId, result.RouteValues["id"]);

        _companyServiceMock.Verify(s => s.CompanyExistsByIdAsync(companyId), Times.Once);
        _reviewServiceMock.Verify(s => s.AlreadyReviewedCompanyAsync(companyId, It.IsAny<string>()), Times.Once);
        _reviewServiceMock.VerifyNoOtherCalls();
    }
}
