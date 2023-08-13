namespace DeliveryService.infrastructure.Minio;

public class MinioSettings
{
	public const string SectionName = "S3Options";

	public string ServiceUrl { get; set; } 

	public string AccessKey { get; set; }

	public string SecretKey { get; set; }
}
