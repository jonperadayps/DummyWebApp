using Application.Contracts.Infrastructure.Persistence;
using Application.Models.Dtos.People;
using Domain.Entities;
using Domain.Notifications;
using MediatR;

namespace Application.Data.People.Commands;

public class CreatePersonCommand : IRequest<int>
{
    public CreatePersonCommand(CreatePersonDto personDto)
    {
        PersonDto = personDto;
    }

    public CreatePersonDto PersonDto { get; }

    public class Handler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = new Person
            {
                Name = request.PersonDto.Name
            };

            await _context.People.AddAsync(entity, cancellationToken);

            entity.PublishNotification(new OnPersonCreated(request.PersonDto.Name));

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}