using System;
using System.Collections.Generic;
using KiwiUoW.Test.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace KiwiUoW.AspNetCore.Test
{
    public class BuilderExtensions
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UoWInitialization()
        {
            var factory = new WebApplicationFactory<Program>();

            var uow = factory.Services.GetRequiredService<TestUoW>();

            Assert.IsNotNull(uow);
            Assert.IsNotNull(uow.TestEntity);
            Assert.IsNotNull(uow.TestEntity2);
            Assert.IsNotNull(uow.TestEntity3);
        }
    }
}