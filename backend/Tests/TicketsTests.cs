using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using FluentAssertions;
using NUnit.Framework;

[TestFixture]
public class TicketServiceTests
{
    private Mock<ITicketRepository> _mockTicketRepository;
    private Mock<IStatusRepository> _mockStatusRepository;
    private TicketService _ticketService;
    private TicketMapper _ticketMapper;
    private LinkMapper _linkMapper;

    [SetUp]
    public void Setup()
    {
        _mockTicketRepository = new Mock<ITicketRepository>();
        _mockStatusRepository = new Mock<IStatusRepository>();
        _ticketService = new TicketService(_mockTicketRepository.Object);
        _linkMapper = new LinkMapper();
        _ticketMapper = new TicketMapper();
    }

    [Test]
    public async Task Create_ValidTicketRequestModel_CallsRepositoryCreate()
    {
        // Arrange
        var links = new List<LinkRequestModel>
        {
            new LinkRequestModel { Url = "https://mail.google.com" }
        };
        var validRequestModel = new TicketRequestModel
        {
            Name = "Valid Name",
            Description = "Description",
            Deadline = DateTime.UtcNow.AddDays(1), // Future date
            Links = links
        };
        _mockStatusRepository.Setup(r => r.GetById(new Guid())).ReturnsAsync(new Status { Name = "In progress", Order = 1 });
        // Act
        await _ticketService.Create(MapTicketFromRequestModel(validRequestModel));

        // Assert
        _mockTicketRepository.Verify(r => r.Create(It.IsAny<Ticket>()), Times.Once);
    }

    [Test]
    public async Task Create_InvalidTicketRequestModel_ThrowsValidationException()
    {
        var links = new List<LinkRequestModel>
        {
            new LinkRequestModel { Url = "https://mail.google.com" }
        };
        // Arrange
        var invalidRequestModel = new TicketRequestModel
        {
            Name = "ss", // Invalid name
            Description = "De",
            Deadline = DateTime.UtcNow,
            Links = links
        };

        // Act
        Func<Task> createAction = async () => await _ticketService.Create(MapTicketFromRequestModel(invalidRequestModel));

        // Assert
        await createAction.Should().ThrowAsync<ValidationException>().WithMessage("Task names have to be at least 3 characters long\nDeadline must be in the future");
    }

    [Test]
    public async Task Update_ExistingTicket_ValidTicketRequestModel_CallsRepositoryUpdate()
    {

        // Arrange
        var existingTicketId = Guid.NewGuid();
        var existingTicket = new Ticket
        {
            Name = "Old name"
            // Set existing ticket properties here
        };
        _mockTicketRepository.Setup(r => r.GetById(existingTicketId)).ReturnsAsync(existingTicket);

        var newRequestModel = new TicketRequestModel
        {
            Name = "Updated Name",
            Description = "Updated Description",
            Deadline = DateTime.UtcNow.AddDays(2), // Future date
                                                   // Set other valid properties for update here
        };

        // Act
        await _ticketService.Update(existingTicketId, newRequestModel);

        // Assert
        _mockTicketRepository.Verify(r => r.Update(It.Is<Ticket>(ticket => ticket.Name == newRequestModel.Name)), Times.Once);
    }

    [Test]
    public async Task Update_InvalidTicketRequestModel_ThrowsValidationException()
    {
        // Arrange
        var existingTicketId = Guid.NewGuid();
        var invalidRequestModel = new TicketRequestModel
        {
            Name = "asd", // Invalid name
            Description = "Updated Description",
            Deadline = DateTime.UtcNow,
        };
        _mockTicketRepository.Setup(r => r.GetById(existingTicketId)).ReturnsAsync(new Ticket
        {
            Name = "ShortreqrwWW", // Invalid name
            Description = "Updated Description",
            Deadline = DateTime.UtcNow.AddDays(2),
        });

        // Act
        Func<Task> updateAction = async () => await _ticketService.Update(existingTicketId, invalidRequestModel);

        // Assert
        await updateAction.Should().ThrowAsync<ValidationException>().WithMessage("Task names have to be at least 3 characters long\nDeadline must be in the future");
    }

    private TicketResponseModel MapTicketToResponseModel(Ticket ticket)
    {
        var mappedTicket = _ticketMapper.MapToResponseModel(ticket);
        var ticketLinks = ticket?.Links?.Select(link => _linkMapper.MapToResponseModel(link))?.ToList();
        mappedTicket.links = ticketLinks;
        return mappedTicket;
    }
    private Ticket MapTicketFromRequestModel(TicketRequestModel ticket)
    {
        var mappedTicket = _ticketMapper.MapFromRequestModel(ticket);
        mappedTicket.Links = ticket.Links.Select(x => _linkMapper.MapFromRequestModel(x)).ToList();
        mappedTicket.Validate();
        return mappedTicket;
    }

    // Similar tests for status and deadline scenarios can be written here
}
