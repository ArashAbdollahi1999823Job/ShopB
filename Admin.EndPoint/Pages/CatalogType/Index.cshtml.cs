using Application.Catalogs.CatalogTypes;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;

        public IndexModel(ICatalogTypeService catalogTypeService)
        {
            _catalogTypeService = catalogTypeService;
        }

        public PaginatedItemDto<CatalogTypeListDto> CatalogTypeList { get; set; }
        public void OnGet(int? parentId, int page = 1, int pageSize = 100)
        {
            CatalogTypeList = _catalogTypeService.GetList(parentId, page, pageSize);
        }
    }
}
