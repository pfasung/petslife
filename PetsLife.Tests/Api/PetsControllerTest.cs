using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Http.Results;
using Moq;
using PetsLife.Api;
using PetsLife.DAL;
using PetsLife.Models;

namespace PetsLife.Tests.Api
{
    [TestClass]
    public class PetsControllerTest
    {
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPetsRepository>();
            var controller = new PetsController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPet(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetReturnsWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IPetsRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new Pet { Id = 42 });

            var controller = new PetsController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPet(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Pet>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsStatusCode()
        {
            // Arrange
            var mockRepository = new Mock<IPetsRepository>();
            var controller = new PetsController(mockRepository.Object);

            // Act
            var result = controller.PutPet(10, new Pet { Id = 10, Name = "Pet" }) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IPetsRepository>();

            mockRepository.Setup(x => x.GetById(10))
                .Returns(new Pet { Id = 10 });

            var controller = new PetsController(mockRepository.Object);

            // Act
            var actionResult = controller.DeletePet(10);
            var contentResult = actionResult as OkNegotiatedContentResult<Pet>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
    }
}
