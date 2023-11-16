using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Category : AppEntityBase
{
	[StringLength(50)] public string Name { get; set; }
}
