﻿using Dapper;
using System.Data;

namespace ApplicationLayer.Configuration;

internal abstract class SqlTypeHandler<T>: SqlMapper.TypeHandler<T>
{
    public override void SetValue(IDbDataParameter parameter, T value) => parameter.Value = value;
}
internal class DateOnlyHandler: SqlTypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        return value switch
        {
            DateTime dt => DateOnly.FromDateTime(dt),
            _ => DateOnly.Parse((string)value)
        };
    }
}

internal class TimeOnlyHandler : SqlTypeHandler<TimeOnly>
{
    public override TimeOnly Parse(object value)
    {
        return value switch
        {
            DateTime dt => TimeOnly.FromDateTime(dt),
            TimeSpan ts => TimeOnly.FromTimeSpan(ts),
            _ => TimeOnly.Parse((string)value)
        };
    }
}