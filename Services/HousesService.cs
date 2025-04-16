using Microsoft.AspNetCore.SignalR;
using spring25_gregslist_dotnet.Models;
using spring25_gregslist_dotnet.Repositories;

namespace spring25_gregslist_dotnet.Services;

public class HousesService
{
  public HousesService(HousesRepository housesRepository)
  {
    _housesRepository = housesRepository;
  }
  private readonly HousesRepository _housesRepository;
  internal List<House> GetAllHouses()
  {
    List<House> houses = _housesRepository.GetAllHouses();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    House house = _housesRepository.GetHouseById(houseId);
    return house;
  }
}