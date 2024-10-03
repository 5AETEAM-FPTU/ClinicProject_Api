using Clinic.Application.Features.Schedules.GetSchedulesByDate;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Moq;

namespace Clinic.Application.UnitTests.Features.Schedules.GetSchedulesByDate;

public class GetSchedulesByDateHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly GetSchedulesByDateHandler _handler;

    public GetSchedulesByDateHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new GetSchedulesByDateHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_Schedules_When_Schedules_Exist()
    {
        var request = new GetSchedulesByDateRequest { Date = DateTime.UtcNow };

        var schedules = new List<Schedule>
        {
            new Schedule
            {
                StartDate = DateTime.UtcNow.AddHours(1),
                EndDate = DateTime.UtcNow.AddHours(2)
            },
            new Schedule
            {
                StartDate = DateTime.UtcNow.AddHours(3),
                EndDate = DateTime.UtcNow.AddHours(4)
            }
        };

        _unitOfWorkMock
            .Setup(repo =>
                repo.GetSchedulesByDateRepository.GetSchedulesByDateQueryAsync(
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(schedules);

        var response = await _handler.ExecuteAsync(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(GetSchedulesByDateResponseStatusCode.OPERATION_SUCCESS, response.StatusCode);
        Assert.NotNull(response.ResponseBody);
        Assert.Equal(2, response.ResponseBody.TimeSlots.Count);

        for (int i = 0; i < schedules.Count; i++)
        {
            Assert.Equal(schedules[i].StartDate, response.ResponseBody.TimeSlots[i].StartTime);
            Assert.Equal(schedules[i].EndDate, response.ResponseBody.TimeSlots[i].EndTime);
        }
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_Empty_TimeSlots_When_No_Schedules_Exist()
    {
        var request = new GetSchedulesByDateRequest { Date = DateTime.UtcNow };

        _unitOfWorkMock
            .Setup(repo =>
                repo.GetSchedulesByDateRepository.GetSchedulesByDateQueryAsync(
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new List<Schedule>());

        var response = await _handler.ExecuteAsync(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(GetSchedulesByDateResponseStatusCode.OPERATION_SUCCESS, response.StatusCode);
        Assert.NotNull(response.ResponseBody);
        Assert.Empty(response.ResponseBody.TimeSlots);
    }
}
