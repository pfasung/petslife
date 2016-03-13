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
    public class PetWalkersControllerTest
    {
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();
            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPetWalker(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetReturnsWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new PetWalker { Id = 42 });

            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPetWalker(42);
            var contentResult = actionResult as OkNegotiatedContentResult<PetWalker>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsStatusCode()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();
            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var result = controller.PutPetWalker(10, new PetWalker { Id = 10, FirstName = "PetWalker", LastName = "PetWalker", EmailAddress = "petowner@petslife.com" }) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();

            mockRepository.Setup(x => x.GetById(10))
                .Returns(new PetWalker { Id = 10 });

            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var actionResult = controller.DeletePetWalker(10);
            var contentResult = actionResult as OkNegotiatedContentResult<PetWalker>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }

        [TestMethod]
        public void IsWalkerApprovedReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new PetWalker { Id = 42, Approvals = new[] { new PetApproval { Pet = new Pet { OwnerId = 23 } } } });

            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var actionResult = controller.IsApprovedByPetOwner(42, 23);
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(contentResult.Content);
        }

        [TestMethod]
        public void IsWalkerApprovedReturnsFalse()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new PetWalker { Id = 42, Approvals = new PetApproval[] { } });

            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var actionResult = controller.IsApprovedByPetOwner(42, 23);
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsFalse(contentResult.Content);
        }

        [TestMethod]
        public void GetReturnsNotFoundIsWalkerApprovedReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPetWalkersRepository>();
            var controller = new PetWalkersController(mockRepository.Object);

            // Act
            var actionResult = controller.IsApprovedByPetOwner(42, 23);
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
