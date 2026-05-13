namespace Faradars.Shared.Result;

public sealed class Unit
{
    public static readonly Unit Value = new();
    private Unit(){}
}