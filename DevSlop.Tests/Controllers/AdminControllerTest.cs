using DevSlop.Slop.Data.Entities;
using DevSlop.Slop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevSlop.Controllers
{
    public class AdminControllerTest
    {
        private readonly AdminController sut;
        private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;
        public AdminControllerTest()
        {
            _scheduleRepositoryMock = new Mock<IScheduleRepository>();
            sut = new AdminController(_scheduleRepositoryMock.Object);
        }

        public class ViewEvent : AdminControllerTest
        {
            [Fact]
            public void Should_return_a_ViewResult()
            {
                // Arrange
                _scheduleRepositoryMock
                    .Setup(x => x.GetEventSchedule(It.IsAny<int>()))
                    .Returns(new Schedule());

                // Act
                var result = sut.ViewEvent(123);

                // Assert
                Assert.IsType<ViewResult>(result);
            }
        }
    }
}
