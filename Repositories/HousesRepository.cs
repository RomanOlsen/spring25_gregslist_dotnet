using spring25_gregslist_dotnet.Models;

namespace spring25_gregslist_dotnet.Repositories;

public class HousesRepository
{

  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;
  internal List<House> GetAllHouses()
  {
    string sqlCommand = "SELECT * FROM houses";
    List<House> houses = _db.Query<House>(sqlCommand).ToList();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    string sqlCommand = "SELECT * FROM houses WHERE id = @houseId;";

    House house = _db.Query(sqlCommand, (House house)
    return house;
  }
}