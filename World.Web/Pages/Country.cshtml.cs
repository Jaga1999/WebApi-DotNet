using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using World.Web.DTO;

namespace World.Web.Pages
{
    public class CountryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<CountryDto> CountriesDetails { get; set; }

        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("WorldWebAPI");
            CountriesDetails = await httpClient.GetFromJsonAsync<List<CountryDto>>("api/Country");
        }
    }
}