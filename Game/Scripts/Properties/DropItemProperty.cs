#nullable disable

using Godot;

// Child of KillableNode
public partial class DropItemProperty : Node
{
    [Export] public int ItemId { get; set; }
    [Export] public int ItemMinDrop { get; set; } = 1;
    [Export] public int ItemMaxDrop { get; set; } = 1;
    [Export] public PackedScene DroppedItemScene { get; set; }

    public int GetDropQuantity()
    {
        var rng = new RandomNumberGenerator();
        return rng.RandiRange(ItemMinDrop, ItemMaxDrop);
    }

    public void Drop(int quantity, Vector3 position)
    {
        var droppedItem = DroppedItemScene.Instantiate<DroppedItem3D>();
        droppedItem.ItemId = ItemId;
        droppedItem.Position = position;
        GetTree().CurrentScene.AddChild(droppedItem);
    }

}