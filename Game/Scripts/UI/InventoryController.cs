using System.Collections.Generic;
using Godot;
public partial class InventoryController : Node
{

	public static InventoryController Instance;
	
	[Export] public int HotbarSlotsQuantity {get;set;}
	[Export] public PackedScene HotbarSlotScene {get;set;}
	public ItemSlot[] HotbarSlots {get;set;} = [];

	public override void _Ready()
	{

		var grid = this.FindChild("HotbarGrid",true) as GridContainer;
		grid.Columns = HotbarSlotsQuantity;
		for (int i = 0; i < HotbarSlotsQuantity; i++)
		{
			var slotInstance = HotbarSlotScene.Instantiate();
			grid.AddChild(slotInstance);
		}
		
		Instance = this;
	}

	public override void _Process(double delta)
	{
	}
}
