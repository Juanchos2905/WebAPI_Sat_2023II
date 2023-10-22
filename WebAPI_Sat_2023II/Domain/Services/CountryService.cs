using API_Sat_2023II.DAL;
using API_Sat_2023II.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI_Sat_2023II.Domain.Interfaces;

namespace WebAPI_Sat_2023II.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataBaseContext _context;
        public CountryService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
            
        }
        public async Task<Country> CreateCountryAsync(Country country)
        {
            try
            {
                country.Id = Guid.NewGuid(); // Asi se asigna automaticamente un ID a un nuevo registro.
                country.CreatedDate = DateTime.Now;
                _context.Countries.Add(country); // Aquí estoy creando el objeto Country en el contexto de mi BD
                await _context.SaveChangesAsync(); // Aquí ya estoy yendo a la BD para hacer el INSERT en la tabla Countries

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Me captura un mensaje cunado el pais ya existe.
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
