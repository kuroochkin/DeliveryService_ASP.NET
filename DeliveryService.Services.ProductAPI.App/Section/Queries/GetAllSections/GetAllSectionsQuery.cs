using DeliveryService.Services.ProductAPI.App.Vm.Section;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Section.Queries.GetAllSections;

public record GetAllSectionsQuery : IRequest<ErrorOr<SectionsVm>>
{
}
