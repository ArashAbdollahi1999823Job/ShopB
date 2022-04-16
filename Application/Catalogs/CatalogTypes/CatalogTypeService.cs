using System.Collections.Generic;
using System.Linq;
using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
using Domain.Catalogs;

namespace Application.Catalogs.CatalogTypes;

public class CatalogTypeService : ICatalogTypeService
{

    private readonly IDataBaseContext _dataBaseContext;
    private readonly IMapper _mapper;

    public CatalogTypeService(IDataBaseContext dataBaseContext, IMapper mapper)
    {
        _dataBaseContext = dataBaseContext;
        _mapper = mapper;
    }


    public BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType)
    {
        var model = _mapper.Map<CatalogType>(catalogType);
         
        _dataBaseContext.CatalogTypes.Add(model);
        _dataBaseContext.SaveChanges();

        return new BaseDto<CatalogTypeDto>(
            _mapper.Map<CatalogTypeDto>(model)
            , new List<string> {$"{model.Type}با موقیت در سیستم ثیت شد"}
            ,  true);


    }

    public BaseDto Remove(int id)
    {
        var catalogType = _dataBaseContext.CatalogTypes.Find(id);
        _dataBaseContext.CatalogTypes.Remove(catalogType);
        _dataBaseContext.SaveChanges();

        return new BaseDto(
            new List<string> {$"عملیات با موقیت انجام شد"},
            true
        );
    }

    public BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogTypeDto)
    {
        var model = _dataBaseContext.CatalogTypes.FirstOrDefault(x => x.Id == catalogTypeDto.Id);

        _mapper.Map(catalogTypeDto,model);

        _dataBaseContext.SaveChanges();

        return new BaseDto<CatalogTypeDto>(
            _mapper.Map<CatalogTypeDto>(model)
            , new List<string> { $"{model.Type}با موقیت در سیستم ویرایش شد" }
            , true);
    }

    public BaseDto<CatalogTypeDto> FindById(int id)
    {
        var data = _dataBaseContext.CatalogTypes.Find(id);
         
        var result=_mapper.Map<CatalogTypeDto>(data);

        return new BaseDto<CatalogTypeDto>(
            result,
            null,
            true
        );

    }

    public PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
    {
        int totalCount = 0;
        var model = _dataBaseContext.CatalogTypes
            .Where(p => p.ParentCatalogTypeId == parentId)
            .PagedResult(page, pageSize, out totalCount);
        var result = _mapper.ProjectTo<CatalogTypeListDto>(model).ToList();
        return new PaginatedItemDto<CatalogTypeListDto>(page, pageSize, totalCount, result);
    }
}