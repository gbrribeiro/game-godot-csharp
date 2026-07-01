using Godot;
using System;

public partial class PickupItemProperty : Area3D
{
	public override void _Ready()
	{
		//AreaEntered += OnAreaEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		foreach (var area in GetOverlappingAreas())
		{
			if (area.GetParent() is not DroppedItem3D droppedItem)
				continue;

			if (!droppedItem.CanPickup)
				continue;

			var inventory = GetParent()?.FindChild("InventoryController", true) as InventoryController;
			if (inventory == null || inventory.IsFull())
				continue;

			inventory.AddItemToInventory(droppedItem.Item, 1);
			droppedItem.QueueFree();
		}

	}
	private void OnAreaEntered(Area3D area)
    {
        GD.Print($"Area entrou: {area.Name}");

        if (area.GetParent() is not DroppedItem3D droppedItem)
            return;

        GD.Print($"Item detectado: {droppedItem.Name}");

        if (!droppedItem.CanPickup)
            return;

        var inventory = GetParent()?.FindChild("InventoryController", true) as InventoryController;
        if (inventory == null || inventory.IsFull())
            return;

        inventory.AddItemToInventory(droppedItem.Item, 1);
        droppedItem.QueueFree();
    }


}
