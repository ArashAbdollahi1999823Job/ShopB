using Application.Dtos;

namespace Application.Catalogs.CatalogTypes
{
    public  interface ICatalogTypeService
    {
        BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType);
        BaseDto Remove(int id);
        BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogType);
        BaseDto<CatalogTypeDto> FindById(int id);
        PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize);
    }
}
