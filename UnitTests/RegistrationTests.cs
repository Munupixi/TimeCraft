using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.UnitTests
{
    [TestClass]
    public class RegistrationTests
    {
        private RegistrationPageViewModel _viewModel;

        [TestInitialize]
        public void Setup()
        {
            _viewModel = new RegistrationPageViewModel();
        }

        [TestMethod]
        public void Registration_WhenNameIsEmpty_ShouldReturnErrorMessage()
        {
            // Arrange
            _viewModel.Name = string.Empty;

            // Act
            bool canRegister = _viewModel.CanRegistrationExecute();

            // Assert
            Assert.IsFalse(canRegister);
            Assert.AreEqual("Имя не может быть пустым", _viewModel.ErrorMessage);
        }

        [TestMethod]
        public void Registration_WhenNameIsTooShort_ShouldReturnErrorMessage()
        {
            // Arrange
            _viewModel.Name = "Фаяз";

            // Act
            bool canRegister = _viewModel.CanRegistrationExecute();

            // Assert
            Assert.IsFalse(canRegister);
            Assert.AreEqual("Имя не может быть короче 3х символов", _viewModel.ErrorMessage);
        }

        [TestMethod]
        public void Registration_WhenAgeIsInvalid_ShouldReturnErrorMessage()
        {
            // Arrange
            _viewModel.AgeAsString = "150";  // Неверный возраст

            // Act
            bool canRegister = _viewModel.CanRegistrationExecute();

            // Assert
            Assert.IsFalse(canRegister);
            Assert.AreEqual("Вам должно быть не меньше 4 и не больше 120 лет", _viewModel.ErrorMessage);
        }

        [TestMethod]
        public void Registration_WhenLoginIsInvalid_ShouldReturnErrorMessage()
        {
            // Arrange
            _viewModel.Name = "Valid Name";
            _viewModel.AgeAsString = "25";
            _viewModel.Login = "invalidLogin"; // Неверный формат логина

            // Act
            bool canRegister = _viewModel.CanRegistrationExecute();

            // Assert
            Assert.IsFalse(canRegister);
            Assert.AreEqual("Логин некоректен (Формат: XX@X.X)", _viewModel.ErrorMessage);

        }

        [TestMethod]
        public void Registration_WhenPasswordsDoNotMatch_ShouldReturnErrorMessage()
        {
            // Arrange
            _viewModel.Name = "Valid Name";
            _viewModel.AgeAsString = "25";
            _viewModel.Login = "validemail@gmail.com";
            _viewModel.Password = "Password123!";
            _viewModel.AgainPassword = "Password321!";

            // Act
            bool canRegister = _viewModel.CanRegistrationExecute();

            // Assert
            Assert.IsFalse(canRegister);
            Assert.AreEqual("Пароли несовпадают", _viewModel.ErrorMessage);
        }

        [TestMethod]
        public void Registration_WhenAllFieldsAreValid_ShouldSucceed()
        {
            // Arrange
            _viewModel.Name = "Valid Name";
            _viewModel.AgeAsString = "25";
            _viewModel.Login = "validemail@gmail.com";
            _viewModel.Password = "Password1234!";
            _viewModel.AgainPassword = "Password1234!";
            _viewModel.ConfirmProgramPolicyCheck = true;

            // Act
            bool canRegister = _viewModel.CanRegistrationExecute();

            // Assert
            Assert.IsTrue(canRegister);
            Assert.AreEqual("", _viewModel.ErrorMessage);
        }

        [TestMethod]
        public void Registration_WhenRegistrationIsExecuted_ShouldNavigateToWeeklySchedulePage()
        {
            // Arrange
            _viewModel.Name = "Valid Name";
            _viewModel.AgeAsString = "25";
            _viewModel.Login = "valid5@example.com";
            _viewModel.Password = "Password1234!";
            _viewModel.AgainPassword = "Password1234!";
            _viewModel.ConfirmProgramPolicyCheck = true;

            // Инициализация Frame
            MainWindowViewModel.Frame = new Frame();

            // Act
            _viewModel.RegistrationExecute();

            // Assert
            // Проверим, что после регистрации мы переходим на страницу WeeklySchedulePage
            Assert.IsInstanceOfType(MainWindowViewModel.Frame.Content, typeof(WeeklySchedulePage));
        }

    }
}
