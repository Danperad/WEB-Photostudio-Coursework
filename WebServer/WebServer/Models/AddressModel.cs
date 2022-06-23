using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.Models;

public class AddressModel
{
    private AddressModel(int id, string street, string houseNumber)
    {
        Id = id;
        Street = street;
        HouseNumber = houseNumber;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("street")] public string Street { get; set; }
    [JsonPropertyName("houseNumber")] public string HouseNumber { get; set; }

    public static AddressModel GetAddressModel(Address address)
    {
        return new AddressModel(address.Id, address.Street, address.HouseNumber);
    }

    public static IEnumerable<AddressModel> GetAddressModel(IEnumerable<Address> addresses)
    {
        return addresses.Select(GetAddressModel);
    }
}