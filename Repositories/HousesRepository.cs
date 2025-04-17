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

  internal void DeleteHouse(int houseId)
  {
    string sqlCommand = @"DELETE FROM houses WHERE id = @houseId LIMIT 1;";
    int rows = _db.Execute(sqlCommand, new { houseId });
    if (rows == 0)
    {
      throw new Exception("House not found. No rows deleted.");
    }
  }

  internal House UpdateHouse(House house)
  {
    string sqlCommand = @"UPDATE houses
SET
name = @Name,
price = @Price,
WHERE id = @Id
LIMIT 1;";

    int rowsAffected = _db.Execute(sqlCommand, house);

    return house;

  }
}