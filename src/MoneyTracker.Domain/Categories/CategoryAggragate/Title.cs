using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Domain.Categories.CategoryAggragate;

[ComplexType]
public record Title(string Value);