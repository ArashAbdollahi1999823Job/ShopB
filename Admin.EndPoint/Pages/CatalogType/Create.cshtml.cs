using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class CreateModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly IMapper _mapper;
        public CreateModel(ICatalogTypeService catalogTypeService, IMapper mapper)
        {
            _catalogTypeService = catalogTypeService;
            _mapper = mapper;
        }

        public void OnGet(int? parentId)
        {


             
        }
 
    }
}
