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

    // string sqlCommand = "SELECT * FROM houses WHERE id = @houseId;";

    // House house = _db.Query(sqlCommand, (House house));
    // return house;

    string sql = @"
     SELECT 
     houses.*,
     accounts.*
     FROM houses
     INNER JOIN accounts ON accounts.id = houses.creatorId
     WHERE houses.id = @houseId;";

    House house = _db.Query(sql, (House house, Account account) =>
     {
       house.Creator = account;
       return house;
     }, new { houseId }).SingleOrDefault();
    return house;
  }

  internal House PostHouse(House housePayload)
  {
    string sqlInsertAndSelect = @"
    INSERT INTO houses (creatorId, name, `squareFt`, bedrooms, bathrooms, `imgUrl`, description, price)
    VALUES (@CreatorId, @Name, @SquareFt, @Bedrooms, @Bathrooms, @ImgUrl, @Description, @Price);
    
    SELECT * FROM houses WHERE houses.id = LAST_INSERT_ID();";

    // string sqlSelect = "SELECT * FROM houses WHERE houses.id = LAST_INSERT_ID();";

    House house = _db.Query<House>(sqlInsertAndSelect, housePayload).SingleOrDefault();

    return house;

  }
}