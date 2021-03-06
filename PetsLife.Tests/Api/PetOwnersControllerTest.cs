﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Http.Results;
using Moq;
using PetsLife.Api;
using PetsLife.DAL;
using PetsLife.Models;

namespace PetsLife.Tests.Api
{
    [TestClass]
    public class PetOwnersControllerTest
    {
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPetOwnersRepository>();
            var controller = new PetOwnersController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPetOwner(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetReturnsWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IPetOwnersRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new PetOwner { Id = 42 });

            var controller = new PetOwnersController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPetOwner(42);
            var contentResult = actionResult as OkNegotiatedContentResult<PetOwner>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsStatusCode()
        {
            // Arrange
            var mockRepository = new Mock<IPetOwnersRepository>();
            var controller = new PetOwnersController(mockRepository.Object);

            // Act
            var result = controller.PutPetOwner(10, new PetOwner { Id = 10, FirstName = "PetOwner", LastName = "PetOwner", EmailAddress = "petowner@petslife.com" }) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IPetOwnersRepository>();

            mockRepository.Setup(x => x.GetById(10))
                .Returns(new PetOwner { Id = 10 });

            var controller = new PetOwnersController(mockRepository.Object);

            // Act
            var actionResult = controller.DeletePetOwner(10);
            var contentResult = actionResult as OkNegotiatedContentResult<PetOwner>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
    }
}
