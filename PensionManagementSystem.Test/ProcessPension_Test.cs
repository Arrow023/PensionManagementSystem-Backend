using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Pension_Management_System.Controllers;
using PensionManagementSystem.Data;
using PensionManagementSystem.Mapper;
using PensionManagementSystem.Models;
using Process_Pension_Module.Repository;

namespace PensionManagementSystem.Test
{
    public class ProcessPension_Test
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ProcessPensionRepository processPensionRepository;

        public static ProcessPensionController ppController;

        [SetUp]
        public void Setup()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PmsMappings()); 
            });
            var mapper = mockMapper.CreateMapper();

            processPensionRepository = new ProcessPensionRepository(_httpContextAccessor);
            ppController = new ProcessPensionController(processPensionRepository, mapper);
        }

        [Test]
        public void Test_ProcessPension_Post_NotFound()
        {
            var result = Result_NotFoundResult();
            Assert.That(result, Is.TypeOf<NotFoundResult>());

            Assert.That(result, Is.InstanceOf<NotFoundResult>());

        }

        public IActionResult Result_NotFoundResult()
        {

            var result = ppController.Post(new ProcessPensionInput { AadhaarNumber = "13245236263136" }).Result;
            return result;
        }


    }
}
