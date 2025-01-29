using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Stockify.Common.Infrastructure.Data.Converters;

public sealed class NullableStringConverter() 
    : ValueConverter<string, string?>(
        s => string.IsNullOrEmpty(s) ? null : s,
        s => string.IsNullOrEmpty(s) ? string.Empty : s);
