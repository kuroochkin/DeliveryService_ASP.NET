using ErrorOr;
using MediatR;

namespace DeliveryService.App.Section.Queries.GetAllSection;

public record GetAllSectionQuery : IRequest<ErrorOr<SectionsVm>>
{
}
