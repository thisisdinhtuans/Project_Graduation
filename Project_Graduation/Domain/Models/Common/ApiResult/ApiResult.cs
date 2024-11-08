using System;

namespace Domain.Models.Common.ApiResult;

public class ApiResult<T>
{
    public bool IsSuccessed { get; set; }
    public string Message { get; set; }
    public T ResultObj { get; set; }
}
