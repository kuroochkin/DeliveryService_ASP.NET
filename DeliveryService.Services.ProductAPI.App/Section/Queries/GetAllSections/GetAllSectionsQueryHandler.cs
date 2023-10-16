using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.App.Vm.Section;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Section.Queries.GetAllSections;

public class GetAllSectionsQueryHandler
	: IRequestHandler<GetAllSectionsQuery, ErrorOr<SectionsVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllSectionsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<SectionsVm>> Handle(
		GetAllSectionsQuery request,
		CancellationToken cancellationToken)
	{
		var sections = await _unitOfWork.Sections.GetAll();
		if (sections is null)
		{
			return new ErrorOr<SectionsVm>();
		}

		var allSections = sections.Select(section => new SectionVm(
			section.Id.ToString(),
			section.Name
			)
		).ToList();

		return new SectionsVm(allSections);
	}
}