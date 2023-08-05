using AutoMapper;

public abstract class BaseMapper<TEntity, TRequestModel, TResponseModel>
                       where TEntity : BaseEntity
                       where TRequestModel : IRequestModel
                       where TResponseModel : IResponseModel
{
    protected IMapper _mapper;

    public TResponseModel MapToResponseModel(TEntity modelToMap)
    {
        return _mapper.Map<TResponseModel>(modelToMap);
    }

    public TEntity MapFromRequestModel(TRequestModel modelToMap)
    {
        var mapped = _mapper.Map<TEntity>(modelToMap);
        mapped.Validate();
        return mapped;
    }
}
