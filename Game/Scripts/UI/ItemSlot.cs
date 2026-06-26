using Godot;

[GlobalClass]
public partial class ItemSlot : Button
{
    [Export] public Item Item {get;set;}
    [Export] public int Quantity {get;set;}
    public bool IsFull => Item.MaxStackSize == Quantity;

    public override void _Ready()
    {
        if(Item != null && !string.IsNullOrEmpty(Item.PathToIcon))
        {
            Icon = GD.Load<Texture2D>(Item.PathToIcon);
        }
    }
}