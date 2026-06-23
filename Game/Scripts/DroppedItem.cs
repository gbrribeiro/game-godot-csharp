using Godot;

public partial class DroppedItem : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export] public float RotationsPerSecond { get; set; } = 0.3f;
	[Export] public int ItemId {get;set;} = 0;
	public Item Item { get; set; }
	public override void _Ready()
	{
		if(ItemId != 0)
		{
			Item = ItemRegistry.GetItemById(ItemId);
			LoadModel();
		}
	}
	private void LoadModel()
    {
        if (Item == null || string.IsNullOrEmpty(Item.PathToScene))
            return;

        try
        {
            // Carrega o mesh do arquivo GLB
            var packedScene = GD.Load<PackedScene>(Item.PathToScene);
            if (packedScene != null)
            {
				var instance = packedScene.Instantiate();
                AddChild(instance);
            }
        }
        catch (System.Exception e)
        {
            GD.PrintErr($"Erro ao carregar modelo {Item.PathToScene}: {e.Message}");
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateY(Mathf.Pi * 2 * RotationsPerSecond * (float)delta);
	}
}
