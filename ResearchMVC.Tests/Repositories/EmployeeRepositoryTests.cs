using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ResearchMVC.DataLayer.Models;
using System.Data.Entity;
using ResearchMVC.DataLayer.Repositories;
using FluentAssertions;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace ResearchMVC.Tests.Repositories
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public void InsertEmployee_via_context()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            var mockContext = new Mock<AdventureWorksModel>();
            

            mockContext.Setup(m => m.Employees).Returns(mockSet.Object);
            mockContext.Setup(m => m.Set<Employee>()).Returns(mockSet.Object);

            var repository = new EmployeesRepository(mockContext.Object);
            repository.Insert(new Employee() { BusinessEntityID = 1 });

            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public void UpdateEmployee_via_context()
        {
            var employee = new Employee() { BusinessEntityID = 1 };

            var mockSet = new Mock<DbSet<Employee>>();
            var mockContext = new Mock<AdventureWorksModel>();
            var mockEntry = new Mock<DbEntityEntry<Employee>>();

            mockContext.Setup(m => m.Employees).Returns(mockSet.Object);
            mockContext.Setup(m => m.Set<Employee>()).Returns(mockSet.Object);

            var repository = new Mock<EmployeesRepository>(mockContext.Object);
            repository.Setup(m => m.SetEntityStateModified(It.IsAny<Employee>()));

            repository.Object.Insert(employee);
            employee.BirthDate = DateTime.Now;
            repository.Object.Update(employee);

            mockSet.Verify(m => m.Attach(It.IsAny<Employee>()), Times.Once);
        }


        [Fact]
        public void DeleteEmployeeById_via_context()
        {
            var employee = new Employee() { BusinessEntityID = 1 };

            var mockSet = new Mock<DbSet<Employee>>();
            var mockContext = new Mock<AdventureWorksModel>();
            var mockEntry = new Mock<DbEntityEntry<Employee>>();

            mockContext.Setup(m => m.Employees).Returns(mockSet.Object);
            mockContext.Setup(m => m.Set<Employee>()).Returns(mockSet.Object);
            mockSet.Setup(m => m.Attach(It.IsAny<Employee>()));
            mockSet.Setup(m => m.Remove(It.IsAny<Employee>()));

            var repository = new Mock<EmployeesRepository>(mockContext.Object);
            repository.Setup(m => m.SetEntityStateModified(It.IsAny<Employee>()));
            repository.Setup(m => m.Find(It.IsAny<int>())).Returns(employee);
            repository.Setup(m => m.IsDetached(It.IsAny<Employee>())).Returns(true);

            repository.Object.Insert(employee);
            repository.Object.Delete(employee);

            
            //mockSet.Verify(m => m.Attach(It.IsAny<Employee>()), Times.Once);
            mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once);
        }

        //TODO Create Test Database 
        // This is just a sample
        [Fact]
        public void Check_Entities()
        {
            AdventureWorksModel context = new AdventureWorksModel("data source=DESKTOP-HTUAB81;initial catalog=AdventureWorks2019;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

            context.BusinessEntities.FirstOrDefault().Should().NotBeNull();
        }
        
    }
}
