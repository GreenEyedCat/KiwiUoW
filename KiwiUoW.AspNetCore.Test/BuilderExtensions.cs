using System;
using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void ResolveRepository()
        {
            var factory = new WebApplicationFactory<Program>();

            var uow = factory.Services.GetRequiredService<TestUoW>();

            var repository = factory.Services.GetRequiredService<GenericRepository<TestEntity, Guid>>();

            repository.Add(new TestEntity()
            {
                TestData = 5
            });
            repository.SaveChanges();

            Assert.AreSame(uow.TestEntity, repository);
            Assert.AreEqual(1, uow.TestEntity.All.Count());
        }
    }
}