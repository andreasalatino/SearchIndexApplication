using System.Text.Json.Serialization;

namespace SearchIndexApplication
{
	public class Item
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; }
		[JsonPropertyName("link")]
		public string Link { get; set; }
		//[JsonPropertyName("condition")]
		//public string Condition { get; set; }
		[JsonPropertyName("price")]
		public double Price { get; set; }
		[JsonPropertyName("brand")]
		public string Brand { get; set; }
		[JsonPropertyName("size")]
		public string Size { get; set; }
		[JsonPropertyName("availability")]
		public string Availability { get; set; }

		public override string ToString()
		{
			return $"ID:{Id},\n Title:{Title},\n Description:{Description},\n Link:{Link},\n Price:{Price},\n Brand:{Brand},\n Size:{Size},\n Availability:{Availability} ";
		}
	}
}
