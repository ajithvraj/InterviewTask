using Moq;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using Xunit;

namespace TaskManagement.Tests.Services
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task CreateTask_ShouldCreateTask_WhenValidInput()
        {
            // Arrange
            var mockRepo = new Mock<ITaskRepository>();

            var createDto = new CreateTaskDto
            {
                Title = "Test Task",
                Description = "Testing",
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            var taskEntity = new TaskItem
            {
                Id = 1,
                Title = createDto.Title,
                Description = createDto.Description,
                DueDate = createDto.DueDate,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false,
                OwnerUserId = "1"
            };

            mockRepo.Setup(r => r.CreateTaskAsync(It.IsAny<TaskItem>()))
                    .ReturnsAsync(taskEntity);

            var service = new TaskService(mockRepo.Object);

            // Act
            var result = await service.CreateTaskAsync(createDto, "1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Task", result.Title);
            Assert.False(result.IsCompleted);
        }
    }
}