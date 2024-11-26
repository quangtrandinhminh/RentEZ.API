using Repository.Models.Base;

namespace Repository.Models;

public class SystemConfig : BaseEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    public bool IsReadOnly { get; set; }
}