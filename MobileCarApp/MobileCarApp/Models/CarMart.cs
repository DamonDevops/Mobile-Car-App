

using MobileCarApp.Models.Base;

namespace MobileCarApp.Models;

public class CarMart : BaseEntity
{
    public List<Car> Cars { get; set; } = new List<Car>();
}
