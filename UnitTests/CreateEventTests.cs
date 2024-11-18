using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCraft.ViewModels.Windows;
using TimeCraft.ViewModels;

namespace TimeCraft.UnitTests
{
    [TestClass]
    public class CreateEventTests
    {
        private DataBaseContent _dbContext;

        [TestInitialize]
        public void Setup()
        {
            _dbContext = new DataBaseContent();

            User.ActiveUser = UserViewModel.Get(1);
        }

        [TestMethod]
        public void TestCreateEvent_ValidData_ShouldSaveToDatabase()
        {
            // Arrange
            var viewModel = new CreateEditEventWindowViewModel();
            var eventModel = viewModel._event;

            eventModel.Title = "Event5";
            eventModel.Description = "";
            eventModel.StartDate = DateTime.Now.AddDays(1);
            eventModel.EndDate = DateTime.Now.AddDays(1).AddHours(2);
            eventModel.StartTime = TimeSpan.Parse("10:00");
            eventModel.EndTime = TimeSpan.Parse("12:00");
            eventModel.CategoryId = 1;
            eventModel.Priority = PriorityEnum.Низкий;
            eventModel.DressCode = DressCodeEnum.Пляжный;

            // Act
            viewModel.CreateCommand.Execute(null);

            // Assert
            var savedEvent = _dbContext.Event.FirstOrDefault(e => e.Title == eventModel.Title);
            Assert.IsNotNull(savedEvent, "Событие не сохранено.");
            Assert.AreEqual("Event5", savedEvent.Title, "Event5 не найден.");
        }

        [TestMethod]
        public void TestCreateEvent_InvalidTitle_ShouldSetErrorMessage()
        {
            // Arrange
            var viewModel = new CreateEditEventWindowViewModel();
            viewModel.StartTime = "10:00";
            viewModel.EndTime = "11:00";
            viewModel.Title = "";  // Пустое название

            // Act
            var canExecute = viewModel.CanCreateExecute();

            // Assert
            Assert.IsFalse(canExecute);
            Assert.AreEqual("Заполните поле названия", viewModel.ErrorMessage);
        }

        [TestMethod]
        public void TestCreateEvent_InvalidTime_ShouldSetErrorMessage()
        {
            // Arrange
            var viewModel = new CreateEditEventWindowViewModel();
            viewModel.Title = "Test Event";
            viewModel.StartTime = "03:00";  // Некорректное время
            viewModel.StartDate = DateTime.Now;
            viewModel.EndTime = "01:00";    // Некорректное время
            viewModel.EndDate = DateTime.Now;

            // Act
            var canExecute = viewModel.CanCreateExecute();

            // Assert
            Assert.IsFalse(canExecute);
            Assert.AreEqual("Неверный формат времени", viewModel.ErrorMessage);
        }

        [TestMethod]
        public void TestCreateEvent_WithDuration_ShouldSaveToDatabase()
        {
            // Arrange
            var viewModel = new CreateEditEventWindowViewModel();
            var eventModel = viewModel._event;

            eventModel.Title = "Поход";
            eventModel.Description = "В горы";
            eventModel.StartDate = DateTime.Now.AddDays(1);
            eventModel.EndDate = DateTime.Now.AddDays(2); // Событие длится 2 дня
            eventModel.StartTime = TimeSpan.Parse("10:00");
            eventModel.EndTime = TimeSpan.Parse("12:00");
            eventModel.CategoryId = 1; 

            // act
            viewModel.CreateExecute();

            // Assert
            var savedEvent = _dbContext.Event.FirstOrDefault(e => e.Title == eventModel.Title);
            Assert.IsNotNull(savedEvent, "Событие не сохранено.");
            Assert.AreEqual("Поход", savedEvent.Title, "Событие не нейдено.");
            Assert.AreEqual(TimeSpan.FromDays(1), savedEvent.EndDate - savedEvent.StartDate, "Длительность не соотвуствует.");
        }

        [TestMethod]
        public void TestCreateEvent_ValidData_ShouldEnableCreateCommand()
        {
            // Arrange
            var viewModel = new CreateEditEventWindowViewModel();
            viewModel.Title = "Valid Event1";
            viewModel.StartTime = "20:00";
            viewModel.EndTime = "22:00";
            viewModel.StartDate = DateTime.Now.AddDays(1);
            viewModel.EndDate = DateTime.Now.AddDays(1).AddHours(2);
            viewModel._event.CategoryId = 1;

            // act
            var canExecute = viewModel.CanCreateExecute();

            // Assert
            Assert.IsTrue(canExecute);
        }
    }
}
