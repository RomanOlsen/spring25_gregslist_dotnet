namespace spring25_gregslist_dotnet.Models;

public class House
{
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public int SquareFt { get; set; }
  public int Bedrooms { get; set; }
  public int Bathrooms { get; set; }
  public string ImgUrl { get; set; }
  public string Description { get; set; }
  public int? Price { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public Account Creator { get; set; }

  public int Id { get; set; }
};