namespace E_Commerce.Models;

public class Role(string name, string description) : BaseModel
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}