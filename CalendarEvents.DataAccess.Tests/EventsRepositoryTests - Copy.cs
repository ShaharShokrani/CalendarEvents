using Autofac.Extras.Moq;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalendarEvents.Repository.Tests
{
    public class EventsServiceTests
    {
        private AutoMock _mock = null;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
        }

        #region Insert
        [Test]
        public async Task Insert_WhenCalled_ShouldCallDbSetContextAdd()
        {
            //Arrange
            var store = new Mock<IUserPasswordStore<User>>();

            var validator = new UserValidator<User>();
            var passValidator = new PasswordValidator<User>();

            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(validator);
            mgr.Object.PasswordValidators.Add(passValidator);
            mgr.Object.PasswordHasher = new PasswordHasher<User>();
            mgr.Object.Options = DefaultAuthenticationRules();

            List<User> users = new List<User>();

            mgr.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), "s")).ReturnsAsync(IdentityResult.Success)
                .Callback<User, string>(
                    (x, y) => 
                    {
                        users.Add(x);
                    }
                );
            mgr.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            MyClass myClass = new MyClass(mgr.Object);
            var s = await myClass.MyMethod();
            //Assert
            //dbSetMock.Verify(x => x.Add(It.Is<EventModel>(y => y == expectedItem)), Times.Once, "Add failed");
            //context.Verify(x => x.SaveChanges(), Times.Once, "Save failed");
        }
        #endregion

        public class MyClass
        {
            private UserManager<User> _userManager;
            public MyClass(UserManager<User> userManager)
            {
                this._userManager = userManager;
            }

            public async Task<bool> MyMethod()
            {
                User user = new User();
                var creationResult = await this._userManager.CreateAsync(user, "s");

                if (!creationResult.Succeeded)
                    return false;

                return true;
            }
        }

        public static IdentityOptions DefaultAuthenticationRules(IdentityOptions identityOptions = null)
        {
            if (identityOptions == null)
                identityOptions = new IdentityOptions();

            identityOptions.User.RequireUniqueEmail = true;
            identityOptions.Password.RequireNonAlphanumeric = false;
            identityOptions.Password.RequiredUniqueChars = 0;

            return identityOptions;
        }

        [TearDown]
        public void CleanUp()
        {
            if (_mock != null)
                _mock.Dispose();
        }

        public class User
        {
            public string Password { get; set; }
        }
    }
}