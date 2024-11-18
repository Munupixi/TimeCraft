using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCraft.ViewModels;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.UnitTests
{
    [TestClass]
    public class CreateTaskTests
    {
        private DataBaseContent _dbContext;

        [TestInitialize]
        public void Setup()
        {
            _dbContext = new DataBaseContent();
            User.ActiveUser = UserViewModel.Get(1);
        }

        [TestMethod]
        public void TestCreateNewTask_ValidData_ShouldSaveToDatabase()
        {
            // Arrange
            var taskViewModel = new CreateEditTaskWindowViewModel();
            var task = taskViewModel._task;

            task.Title = "Test Task10";
            task.Description = "Описание задачи";
            task.StartDate = DateTime.Now.AddDays(5);
            task.StartTime = TimeSpan.Parse("08:00");
            task.EndDate = DateTime.Now.AddDays(5).AddHours(2);
            task.EndTime = TimeSpan.Parse("10:00");
            task.CategoryId = 1;

            // Act
            taskViewModel.CreateCommand.Execute();

            // Assert
            var savedTask = _dbContext.Task.FirstOrDefault(t => t.Title == task.Title);
            Assert.IsNotNull(savedTask, "Задача не сохранена.");
            Assert.AreEqual("Test Task10", savedTask.Title, "Название не походит.");
            Assert.AreEqual("Описание задачи", savedTask.Description, "Описание не подходит.");
        }

        [TestMethod]
        public void TestCreateNewTask_InvalidTitle_ShouldSetErrorMessage()
        {
            // Arrange
            var viewModel = new CreateEditTaskWindowViewModel();
            viewModel.StartTime = "10:00";
            viewModel.EndTime = "11:00";
            viewModel.Title = "";  // Пустой заголовок

            // Act
            var canExecute = viewModel.CanCreateExecute();

            // Assert
            Assert.IsFalse(canExecute);
            Assert.AreEqual("Заполните поле названия", viewModel.ErrorMessage);
        }



        [TestMethod]
        public void TestCreateNewTask_RepeatTask_ShouldSaveToDatabase()
        {
            // Arrange
            var taskViewModel = new CreateEditTaskWindowViewModel();
            var task = taskViewModel._task;

            task.Title = "Уборка";
            task.Description = "Каждый день.";
            task.StartDate = DateTime.Now.AddDays(1);
            task.StartTime = TimeSpan.Parse("08:00");
            task.EndDate = DateTime.Now.AddDays(2);
            task.EndTime = TimeSpan.Parse("10:00");
            task.Repeat = 1;
            task.CategoryId = 1;

            // Act
            taskViewModel.CreateCommand.Execute(null);

            // Assert
            var savedTask = _dbContext.Task.FirstOrDefault(t => t.Title == task.Title);
            Assert.IsNotNull(savedTask, "Уборка не сохранена.");
            Assert.AreEqual("Уборка", savedTask.Title, "Уборка не найдена.");
            Assert.AreEqual("Каждый день.", savedTask.Description, "Описание не найдено");
        }

        [TestMethod]
        public void TestCreateNewTask_InvalidTime_ShouldSetErrorMessage()
        {
            // Arrange
            var viewModel = new CreateEditTaskWindowViewModel();
            viewModel.Title = "Task";
            viewModel.StartTime = "3:00";  
            viewModel.StartDate = DateTime.Now;
            viewModel.EndTime = "1:00";  
            viewModel.EndDate = DateTime.Now;

            // Act
            var canExecute = viewModel.CanCreateExecute();

            // Assert
            Assert.IsFalse(canExecute);
            Assert.AreEqual("Неверный формат времени", viewModel.ErrorMessage);
        }

    }
}