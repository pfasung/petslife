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
    public class PetApprovalsControllerTest
    {
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPetApprovalsRepository>();
            var controller = new PetApprovalsController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPetApproval(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetReturnsWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IPetApprovalsRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new PetApproval { Id = 42 });

            var controller = new PetApprovalsController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPetApproval(42);
            var contentResult = actionResult as OkNegotiatedContentResult<PetApproval>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsStatusCode()
        {
            // Arrange
            var mockRepository = new Mock<IPetApprovalsRepository>();
            var controller = new PetApprovalsController(mockRepository.Object);

            // Act
            var result = controller.PutPetApproval(10, new PetApproval { Id = 10}) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IPetApprovalsRepository>();

            mockRepository.Setup(x => x.GetById(10))
                .Returns(new PetApproval { Id = 10 });

            var controller = new PetApprovalsController(mockRepository.Object);

            // Act
            var actionResult = controller.DeletePetApproval(10);
            var contentResult = actionResult as OkNegotiatedContentResult<PetApproval>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
    }
}
