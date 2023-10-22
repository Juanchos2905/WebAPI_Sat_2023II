using API_Sat_2023II.DAL.Entities;

namespace WebAPI_Sat_2023II.Domain.Interfaces
{
    public interface ICountryService
    {
        //IList
        //ICollection
        //IEnumerable
        Task<IEnumerable<Country>> GetCountriesAsync(); //Una firma de método
        Task<Country> CreateCountryAsync(Country country);

    }
}
