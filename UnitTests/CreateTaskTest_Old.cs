using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeCraft.ViewModels;

namespace TimeCraft.Tests
{
    [TestClass]
    public class CreateTaskTest_Old
    {
        private UserViewModel _userViewModel;


        

      

        //Проверяет успешное добавление задачи с валидными данными
        [TestMethod]
        public void Add_ValidTask_AddsTaskSuccessfully()
        {
            // Arrange: Создание уникальной задачи для теста
            var task = new Task(
                taskId: TaskViewModel.GetNewId(),
                title: "Test Task " + DateTime.Now.Ticks, // Уникальный заголовок
                userId: 1,
                description: "test",
                startDate: DateTime.Now,
                startTime: TimeSpan.FromHours(9),
                endDate: DateTime.Now.AddHours(1),
                endTime: TimeSpan.FromHours(10),
                repeat: 0,
                priority: PriorityEnum.Средний,
                isDone: false,
                categoryId: 1
            );

            var taskViewModel = new TaskViewModel(task);

            // Act: Добавление задачи в базу данных
            taskViewModel.Add();

            // Assert: Проверка, что задача была добавлена
            var addedTask = TaskViewModel.Get(task.TaskId);
            Assert.IsNotNull(addedTask);
            Assert.AreEqual(task.Title, addedTask.Title);

            
        }

        // Проверяет, что при добавлении задачи с некорректным categoryId выбрасывается InvalidOperationException
        [TestMethod]
        public void Add_InvalidTaskData_ThrowsInvalidOperationException()
        {
            // Arrange: Создание задачи с некорректным идентификатором категории
            var taskWithInvalidCategoryId = new Task(
                taskId: TaskViewModel.GetNewId(),
                title: "Test Task " + DateTime.Now.Ticks,
                userId: 1,
                description: "test",
                startDate: DateTime.Now,
                startTime: TimeSpan.FromHours(9),
                endDate: DateTime.Now.AddHours(1),
                endTime: TimeSpan.FromHours(10),
                repeat: 0,
                priority: PriorityEnum.Средний,
                isDone: false,
                categoryId: -1 
            );

            var taskViewModelInvalidCategoryId = new TaskViewModel(taskWithInvalidCategoryId);

            // Act: Попытка добавить задачу с некорректным идентификатором категории
            // Assert: Ожидаем, что будет выброшено исключение
            Assert.ThrowsException<InvalidOperationException>(() => taskViewModelInvalidCategoryId.Add());
        }

        //Проверяет, что пользователь с уникальным логином и валидными данными успешно добавлен
        [TestMethod]
        public void Register_ValidUser_Success()
        {
            // Arrange: Создание нового пользователя с уникальным логином
            var uniqueLogin = "validuser" + DateTime.Now.Ticks + "@example.com";
            var newUser = new User(
                userId: UserViewModel.GetNewId(),
                login: uniqueLogin, // уникальный логин
                password: "Valid123!", // корректный пароль
                age: 25, // корректный возраст
                name: "Артур", // Пример других параметров
                surname: "Doe",
                patronymic: "qd"
            );

            _userViewModel = new UserViewModel(newUser);

            // Act: Попытка регистрации пользователя
            _userViewModel.Add();

            // Assert: Проверка, что пользователь был добавлен в базу
            var addedUser = UserViewModel.Get(newUser.Login);
            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Login, addedUser.Login);
            Assert.AreEqual(newUser.Age, addedUser.Age);
        }

        // Проверяет, что при регистрации с некорректным логином выбрасывается исключение

        [TestMethod]
        public void Register_InvalidLogin_ThrowsException()
        {
            // Arrange: Создание пользователя с некорректным логином
           
            var newUser = new User(
                userId: UserViewModel.GetNewId(),  // Получение нового userId
                login: "invaliduser", // корректный логин
                password: "Valid123!", // корректный пароль
                age: 25, // корректный возраст
                name: "John", // Пример других параметров
                surname: "Doe",
                patronymic: "qd");

            _userViewModel = new UserViewModel(newUser);

            // Act & Assert: Проверка, что попытка регистрации с некорректным логином вызывает исключение
            Assert.ThrowsException<InvalidOperationException>(() => _userViewModel.Add(),
                "Ожидалось исключение при попытке регистрации с некорректным логином.");
        }

        // Проверяет, что метод IsLoginCorrect возвращает false для логина в неверном формате
        [TestMethod]
        public void Login_IncorrectFormat_ReturnsFalse()
        {
            // Arrange
            var user = new User(1, "invalidemail", "Password123!", 30, "John", "Doe", "Faza");
            var viewModel = new UserViewModel(user);

            // Act
            bool result = viewModel.IsLoginCorrect();

            // Assert
            Assert.IsFalse(result);
        }

        //Проверяет, что метод IsLoginCorrect возвращает true для валидного логина
        [TestMethod]
        public void IsLoginCorrect_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var user = new User(1, "test@example.com", "Password123!", 25, "Faza", "Doe", "john.doe@example.com");
            var viewModel = new UserViewModel(user);

            // Act
            bool result = viewModel.IsLoginCorrect();

            // Assert
            Assert.IsTrue(result);
        }

        // Проверяет, что метод IsPasswordCorrect возвращает true для достаточно сложного пароля
        [TestMethod]
        public void Password_StrongEnough_ReturnsTrue()
        {
            // Arrange
            var user = new User(1, "test@example.com", "StrongP@ssw0rd", 30, "John", "Doe", "Faza");
            var viewModel = new UserViewModel(user);

            // Act
            bool result = viewModel.IsPasswordCorrect();

            // Assert
            Assert.IsTrue(result);
        }

        // Проверяет, что метод IsPasswordCorrect возвращает false для слабого пароля
        [TestMethod]
        public void Password_Weak_ReturnsFalse()
        {
            // Arrange
            var user = new User(1, "test@example.com", "weakpass", 30, "John", "Doe", "Faza");
            var viewModel = new UserViewModel(user);

            // Act
            bool result = viewModel.IsPasswordCorrect();

            // Assert
            Assert.IsFalse(result);
        }

        // Проверяет, что метод IsAgeCorrect возвращает true для возраста в допустимом диапазоне
        [TestMethod]
        public void Age_Valid_ReturnsTrue()
        {
            // Arrange
            var user = new User(1, "test@example.com", "Password123!", 30, "John", "Doe", "Faza");
            var viewModel = new UserViewModel(user);

            // Act
            bool result = viewModel.IsAgeCorrect();

            // Assert
            Assert.IsTrue(result);
        }

        // Проверяет, что метод IsAgeCorrect возвращает false для возраста, выходящего за допустимый диапазон
        [TestMethod]
        public void Age_Invalid_ReturnsFalse()
        {
            // Arrange
            var user = new User(1, "test@example.com", "Password123!", 150, "John", "Doe", "Faza");
            var viewModel = new UserViewModel(user);

            // Act
            bool result = viewModel.IsAgeCorrect();

            // Assert
            Assert.IsFalse(result);
        }

    }
}