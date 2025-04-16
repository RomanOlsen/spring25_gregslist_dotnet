using System.Threading.Tasks;
using spring25_gregslist_dotnet.Models;
using spring25_gregslist_dotnet.Services;

namespace spring25_gregslist_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HousesController : ControllerBase
{
  public HousesController(HousesService housesService, Auth0Provider auth0)
  {
    _housesService = housesService;
    _auth0Provider = auth0;
  }
  private readonly HousesService _housesService;
  private readonly Auth0Provider _auth0Provider;


  [HttpGet]
  public ActionResult<List<House>> GetAllHouses()
  {
    try
    {
      List<House> houses = _housesService.GetAllHouses();
      return Ok(houses);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
  [HttpGet("{houseId}")]
  public ActionResult<House> GetHouseById(int houseId)
  {
    try
    {
      House house = _housesService.GetHouseById(houseId);
      return Ok(house);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<House>> PostHouse([FromBody] House housePayload)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      housePayload.CreatorId = userInfo.Id;
      House house = _housesService.PostHouse(housePayload);
      return Ok(house);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}