using Faradars.Application.Interfaces.General;

namespace Faradars.Application.DTOs;

public abstract class BaseDto<TKey>: IDto
{
    public TKey Id { get; set; }
}

public abstract class BaseDto : BaseDto<int>
{
    
}