namespace DeliveryService.Services.ProductAPI.Domain.Section;

public class SectionEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }

	public SectionEntity(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	public SectionEntity() { }
}
