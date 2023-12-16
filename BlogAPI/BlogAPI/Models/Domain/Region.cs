namespace BlogAPI.Models.Domain
{
	public class Region
	{
		public Guid Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		// Nullable string type
		public string? RegionImageUrl { get; set; }	


	}
}
