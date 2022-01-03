using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace ApiExample.Core.Extensions
{
    public static class UpdateDefinitionExtension
    {
        public static UpdateDefinition<T> SetIfValue<T>(this UpdateDefinition<T> builder, Expression<Func<T, string>> setter, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                builder = builder.Set(setter, value);

            return builder;
        }

        public static UpdateDefinition<T> SetIfValue<T, T2>(this UpdateDefinition<T> builder, Expression<Func<T, T2>> setter, T2 value)
        {
            if (value != null)
                builder = builder.Set(setter, value);

            return builder;
        }
    }
}
