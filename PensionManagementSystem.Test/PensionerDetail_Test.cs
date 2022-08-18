using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Pensioner_Detail_Module.Controllers;
using Pensioner_Detail_Module.Repository;
using PensionManagementSystem.Data;
using PensionManagementSystem.Dtos;
using PensionManagementSystem.Mapper;
using PensionManagementSystem.Models;

namespace PensionManagementSystem.Test
{
    public class PensionerDetail_Test
    {
        Mock<DbSet<PensionerDetail>> mockSet;
        private static DbContextOptions<ApplicationDbContext> dbContextOpt = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PensionerDetailTEST")
            .Options;

        private PensionerDetailRepository pensionerDetailRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        Mock<ApplicationDbContext> mockContext;
        public static PensionerDetailController pdController;


        [SetUp]
        public void Setup()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PmsMappings()); 
            });
            var mapper = mockMapper.CreateMapper();

            var PensionerDetailData = new List<PensionerDetail>()
            {
               new PensionerDetail{

                    UserId = 1,
                    Name = "User One",
                    DateOfBirth = DateTime.Now.AddDays(34),
                    PAN = "PAN 123",
                    AadhaarNumber = 123456789101,
                    SalaryEarned = 35000,
                    Allowances = 4500,
                    Type = 0,
                    BankId = 1
               },
               new PensionerDetail{
                    UserId = 2,
                    Name = "User Two",
                    DateOfBirth = DateTime.Now,
                    PAN = "PAN 234",
                    AadhaarNumber = 123456789111,
                    SalaryEarned = 40000,
                    Allowances = 5000,
                    Type = 0,
                    BankId = 2
               }

            }.AsQueryable();

            mockSet = new Mock<DbSet<PensionerDetail>>();

            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.Provider).Returns(PensionerDetailData.Provider);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.Expression).Returns(PensionerDetailData.Expression);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.ElementType).Returns(PensionerDetailData.ElementType);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.GetEnumerator()).Returns(PensionerDetailData.GetEnumerator());

            mockContext = new Mock<ApplicationDbContext>(dbContextOpt);
            mockContext.Setup(c => c.PensionerDetails).Returns(mockSet.Object);

            pensionerDetailRepository = new PensionerDetailRepository(mockContext.Object, _httpContextAccessor);
            pdController = new PensionerDetailController(pensionerDetailRepository, mapper);
        }

        [Test]
        public void Test_Get_PensionerDetail_ReturnsOK()
        {
            var result = pdController.Get(123456789101);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }


        [Test]
        public void Test_Get_PensionerDetail_NameCheck()
        {
            var actionresult = pdController.Get(123456789101);
            var result = actionresult as OkObjectResult;
            var output = (PensionerDetailDto)result.Value;
            Assert.AreEqual("User One", output.Name);
        }

        
        [Test]
        public void Test_Get_PensionerDetail_NotFoundStatus()
        {
            var result = pdController.Get(13245236263136);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Test_Get_PensionerDetail_AadhaarCheck()
        {
            var actionresult = pdController.Get(123456789101);
            var result = actionresult as OkObjectResult;
            var output = (PensionerDetailDto)result.Value;
            Assert.AreEqual(123456789101, output.AadhaarNumber);
        }

        [Test]
        public void Test_Get_PensionerDetail_PanCheck()
        {
            var actionresult = pdController.Get(123456789101);

            var result = actionresult as OkObjectResult;
            var output = (PensionerDetailDto)result.Value;
            Assert.AreEqual("PAN 123", output.PAN);
        }


        [TearDown]
        public void CleanUp()
        {
            
        }

    }
}
