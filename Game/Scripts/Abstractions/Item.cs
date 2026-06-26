#nullable disable

using Godot;

[GlobalClass]
public partial class Item : Resource
{
    [Export] public int Id { get; set; }
    [Export] public string Name { get; set; }
    [Export] public string DisplayName { get; set; }
    [Export] public int MaxStackSize { get; set; }
    public bool IsStackable => MaxStackSize > 1;
    [Export] public string PathToScene { get; set; }
    [Export] public string PathToIcon {get;set;}

}