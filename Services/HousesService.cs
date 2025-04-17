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
    if (house is null)
    {
      throw new Exception($"Cant find house with id of {houseId}");
    }
    return house;
  }

  internal House PostHouse(House housePayload)
  {
    House house = _housesRepository.PostHouse(housePayload);
    return house;
  }

  internal string DeleteHouse(int houseId, Account userInfo)
  {
    House house = GetHouseById(houseId);
    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception("You are forbidden to do that. Thats not your house mick!");
    }
    _housesRepository.DeleteHouse(houseId);
    return "House has been deleted!";
  }

  internal House UpdateHouse(int houseId, Account userInfo, House housePayload)
  {
    House house = GetHouseById(houseId);
    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception("You are forbidden to do that. Thats not your house mick!");
    }

house.Price = housePayload.Price ?? house.Price;
house.Name = housePayload.Name ?? house.Name;


    House updatedHouse = _housesRepository.UpdateHouse(house);
    return updatedHouse;
  }
}