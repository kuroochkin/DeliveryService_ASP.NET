using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Product.Queries;
using ErrorOr;
using MediatR;
using static DeliveryService.App.Common.Errors.Errors;

namespace DeliveryService.App.Section.Queries.GetAllSection;

public class GetAllSectionQueryHandler
	: IRequestHandler<GetAllSectionQuery, ErrorOr<SectionsVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllSectionQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<SectionsVm>> Handle(
		GetAllSectionQuery request, 
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
