using System.Runtime.CompilerServices;

namespace Domain.Entities.Simple;

public class CarsImage:BaseEntity<int>
{
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
}
