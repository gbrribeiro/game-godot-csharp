using Godot;

public partial class ItemPreview : Node3D
{
	public string SaveToPath {get;set;} = "res://Game/Images/";
	[Export] public Item Item {get;set;}

	private int _frameCount = 0;
	private SubViewport _viewport;

	public override void _Ready()
	{
		SaveToPath = $"{SaveToPath}{Item.Id}.png";
		RenderingServer.FramePostDraw += _OnFrameReady;
		_viewport = GetNode<SubViewport>("SubViewport");

		var itemInstance = GD.Load<PackedScene>(Item.PathToScene).Instantiate();
		_viewport.AddChild(itemInstance);
	}

	private void _OnFrameReady()
    {
        _frameCount++;
        if (_frameCount < 3) return;

        RenderingServer.FramePostDraw -= _OnFrameReady; // desconecta

        var img = _viewport.GetTexture().GetImage();
    	img.Convert(Image.Format.Rgba8);
        img.SavePng(SaveToPath);

		_RegisterItemIcon();

        QueueFree(); 
    }

	private void _RegisterItemIcon()
	{
		ItemRegistry.UpdateItem(Item);
	}
}
