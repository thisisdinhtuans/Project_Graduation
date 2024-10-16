using System;

namespace Domain.Models.Common.ApiResult;

public class ApiSuccessResult<T> : ApiResult<T>
{
    public ApiSuccessResult(T resultObj)
    {
        IsSuccessed=true;
        ResultObj=resultObj;
    }
    public ApiSuccessResult()
    {
        IsSuccessed=true;
    }
}
