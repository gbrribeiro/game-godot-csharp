using System.Collections.Generic;
using Godot;
public partial class InventoryController : Node
{

	public static InventoryController Instance;
	
	[Export] public int HotbarSlotsQuantity {get;set;}
	[Export] public PackedScene HotbarSlotScene {get;set;}
	[Export] public HotbarGrid HotbarGrid {get;set;}
	public ItemSlot[] HotbarSlots {get;set;} = [];

	public override void _Ready()
	{

		var grid = HotbarGrid;
		grid.Columns = HotbarSlotsQuantity;
		for (int i = 1; i < HotbarSlotsQuantity; i++)
		{
			var slotInstance = HotbarSlotScene.Instantiate();
			grid.AddChild(slotInstance);
			grid.UpdateSlots();
		}
		
		Instance = this;
	}

	public override void _Process(double delta)
	{
	}

	public void AddItemToInventory(Item item, int quantity)
	{
		// procura um slot vazio ou um slot com o mesmo item
		foreach (var slot in HotbarSlots)
		{
			if (slot.Item == null || (slot.Item.Id == item.Id && !slot.IsFull))
			{
				slot.Item = item;
				slot.Quantity += quantity;
				slot.Icon = GD.Load<Texture2D>(item.PathToIcon);
				return;
			}
		}
	}

	public bool IsFull()
	{
		foreach (var slot in HotbarSlots)
		{
			if (slot.Item == null || !slot.IsFull)
			{
				return false;
			}
		}
		return true;
	}
}
