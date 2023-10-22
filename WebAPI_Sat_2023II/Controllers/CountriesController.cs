using API_Sat_2023II.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Sat_2023II.Domain.Interfaces;

namespace WebAPI_Sat_2023II.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//Esta es la primera parte de la URL de esta API: URL= api/countries
    public class CountriesController : Controller
    {

        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //ACCIONES SE USAN EN LOS CONTROLADORES no METODOS!
        //Si es un API es un ENDPOINT  

        //Todo endpoint retorna un actionresult, significa que retonra el resultado de una ACCIÓN.
        [HttpGet, ActionName("Get")]
        [Route("Get")]// Aquí concateno la Url inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync(); // Yendo a capa Domain 

            if(countries == null || !countries.Any()) //El metodo Any() significa si hay al menos un elemento.
            {
                return NotFound(); //NotFound = 404 HTTP status code
            }

            return Ok(countries); //Ok = 200 http Status code


        }
        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);
                if(createdCountry == null)
                {
                    return NotFound(); //Not Found = 404 HTTP status code
                }


                return Ok(createdCountry); //Retorne un 200 y el objeto Country
            }
            catch(Exception ex)
            {
                if(ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya existe.", country.Name)); // CONFLICT = 409 HTTP status code error
                }

                return Conflict(ex.Message);
            }
        }
    }
}
