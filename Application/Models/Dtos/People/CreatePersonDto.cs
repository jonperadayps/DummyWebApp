using Application.Models.Common;

namespace Application.Models.Dtos.People;

public class CreatePersonDto : IPersonDto
{
    public string Name { get; set; }
}