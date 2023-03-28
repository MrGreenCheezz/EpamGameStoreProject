using CapstoneProjectAPI.Controllers;
using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreTests
{
    public class Tests
    {
        private GameController _controller;
        private Mock<IRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository>();
            _controller = new GameController(_mockRepository.Object);
        }

        [Test]
        public void GetGamesWithPagination_ReturnsListOfGameItems()
        {
            // Arrange
            var amount = 10;
            var offset = 0;
            var expected = new List<GameItem> { new GameItem(), new GameItem() };
            _mockRepository.Setup(repo => repo.GetItemsWithPagination(amount, offset)).Returns(expected);

            // Act
            var result = _controller.GetGamesWithPagination(amount, offset);

            // Assert
            Assert.That(expected == result);
        }

        [Test]
        public async Task PostEditGame_ReturnsOk()
        {
            // Arrange
            var dummyId = 1;
            var dummyName = "Game 1";
            var dummyDescription = "Description 1";
            var dummyPrice = 19.99f;
            var dummyGenres = "Action";
            _mockRepository.Setup(repo => repo.EditGame(dummyId, dummyName, dummyDescription, dummyPrice, dummyGenres, null)).Verifiable();

            // Act
            var result = await _controller.PostEditGame(dummyId, dummyName, dummyDescription, dummyPrice, dummyGenres);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            _mockRepository.Verify();
        }

        [Test]
        public void AddGame_ReturnsGameId()
        {
            // Arrange
            var dummyName = "Game 1";
            var dummyDecription = "Description 1";
            var dummyPrice = 19.99f;
            var dummyGenres = "Action";
            var expectedId = 1;
            _mockRepository.Setup(repo => repo.AddGame(It.Is<GameItem>(i => i.Name == dummyName && i.Description == dummyDecription), null)).Returns(expectedId);

            // Act
            var result = _controller.AddGame(dummyName, dummyDecription, dummyPrice, dummyGenres, null);

            // Assert
            Assert.That(expectedId == result);
        }

        [Test]
        public void GetCount_ReturnsGameQuantity()
        {
            // Arrange
            var expectedCount = 10;
            _mockRepository.Setup(repo => repo.GetQuantity()).Returns(expectedCount);

            // Act
            var result = _controller.GetCount();

            // Assert
            Assert.That(expectedCount == result);
        }

        [Test]
        public async Task DeleteGame_ReturnsOk()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.DeleteGame(id));

            // Act
            var result = await _controller.DeleteGame(id);

            // Assert
            var typeName = result.GetType().Name;

            Assert.That(typeName == "OkResult");
        }

        [Test]
        public void GetGame_ReturnsGameItem()
        {
            // Arrange
            var id = 1;
            var expected = new GameItem();
            _mockRepository.Setup(repo => repo.GetGame(id)).Returns(expected);

            // Act
            var result = _controller.GetGame(id);

            // Assert
            Assert.That(expected == result);
        }
    }
}
