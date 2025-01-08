using InventoryManagement.Models;
using InventoryManagement.Services;
using InventoryManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.ObjectModel;

namespace InventoryManagement.Tests
{
    [TestClass]
    public class InventoryViewModelTests
    {
        [TestMethod]
        public void AddItem_ShouldAddNewItemToInventoryAndDatabase()
        {
            // Arrange
            var mockDatabaseService = new Mock<IInventoryDatabaseService>();
            var viewModel = new InventoryViewModel(mockDatabaseService.Object);

            viewModel.NewItemName = "Test Item";
            viewModel.NewItemCategory = "Test Category";
            viewModel.NewItemStockQuantity = 10;

            var initialCount = viewModel.InventoryItems.Count;

            // Act
            viewModel.AddItem(null);

            // Assert
            Assert.AreEqual(initialCount + 1, viewModel.InventoryItems.Count);
            mockDatabaseService.Verify(
                db => db.AddInventoryItem(It.Is<InventoryItem>(item =>
                    item.Name == "Test Item" &&
                    item.Category == "Test Category" &&
                    item.StockQuantity == 10)),
                Times.Once);
        }

        [TestMethod]
        public void DeleteItem_ShouldRemoveItemFromInventoryAndDatabase()
        {
            // Arrange
            var mockDatabaseService = new Mock<IInventoryDatabaseService>();
            var viewModel = new InventoryViewModel(mockDatabaseService.Object);

            var testItem = new InventoryItem
            {
                Id = 1,
                Name = "Test Item",
                Category = "Test Category",
                StockQuantity = 5
            };

            viewModel.InventoryItems = new ObservableCollection<InventoryItem> { testItem };
            viewModel.SelectedItem = testItem;

            var initialCount = viewModel.InventoryItems.Count;

            // Act
            viewModel.DeleteItem(null);

            // Assert
            Assert.AreEqual(initialCount - 1, viewModel.InventoryItems.Count);
            mockDatabaseService.Verify(
                db => db.DeleteInventoryItem(It.Is<InventoryItem>(item => item.Id == 1)),
                Times.Once);
        }
    }
}
