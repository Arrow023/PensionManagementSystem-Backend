using JWTAuthentication.Controllers;
using JWTAuthentication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using JWTAuthentication;
using PensionManagementSystem.Data;
using PensionManagementSystem.Models;
using NUnit.Framework;
using AutoMapper;
using PensionManagementSystem.Mapper;

namespace PensionManagementSystem.Test
{
    public class JWTAuthentication_Test
    {
        Mock<DbSet<User>> mockSet;
        //auto mapper configuration
        
        private static DbContextOptions<ApplicationDbContext> dbContextOpt = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "User")
            .Options;
        private AuthRepository authRepository;
        public static JwtAuthenticationController jwt;

        [SetUp]
        public void Setup()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PmsMappings()); 
            });
            var mapper = mockMapper.CreateMapper();

            var userData = new List<User>()
            {
                new User{ Id = 1,
                Username = "user1",
                Password = "dXNlcjE="
            },new User{
                Id=2,
                Username = "user2",
                Password = "dXNlcjI=" }

            }.AsQueryable();

            mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>(dbContextOpt);
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var appSettingsOption = Options.Create(new AppSettings()
            { Secret = "VGhpcyBzZWNyZXQga2V5IGlzIG5vIGxvbmdlciBzZWNyZXQsIGJlY2F1c2UgeW91IGRlY3J5cHRlZCBpdA==" });

            authRepository = new AuthRepository(mockContext.Object, appSettingsOption);
            jwt = new JwtAuthenticationController(authRepository,mapper);
        }

        [Test]
        public void Test_Authenticate_JWTAuthentication_ReturnsOK()
        {
            var result = jwt.Authenticate(new Authentication() { Username = "user1", Password = "user1" });
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Test_Authenticate_JWTAuthentication_Returns_BADRequest()
        {
            var result = jwt.Authenticate(new Authentication() { Username = "invalid123", Password = "invalidpassword" });
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
        [Test]
        public void Test_Authenticate_JWTAuthentication_Invalid_BAD_Request_Model_state()
        {
            jwt.ModelState.AddModelError("test", "Test");

            var result = jwt.Authenticate(new Authentication() { Username = "invalid123", Password = null });
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());

        }
        [Test]
        public void Test_Register_JWTAuthentication_ReturnsOK()
        {

            var result = jwt.Register(new Authentication() { Username = "user3", Password = "user3" });
            Assert.That(result, Is.TypeOf<OkResult>());

        }
        [Test]
        public void Test_Register_JWTAuthentication_CheckUniqueUser()
        {

            var result = jwt.Register(new Authentication() { Username = "user1", Password = "user1" });
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());

        }
        [Test]
        public void Test_Register_JWTAuthentication_Invalid_BadRequest_ModelState()
        {
            jwt.ModelState.AddModelError("test", "Test");

            var result = jwt.Register(new Authentication() { Username = "string12345", Password = null });
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());

        }

    }
}
