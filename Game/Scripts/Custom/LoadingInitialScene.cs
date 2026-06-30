using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class LoadingInitialScene : Node
{
	[Export] public PackedScene[] ScenesToLoad {get;set;}
	[Export] public PackedScene MainScene {get;set;}

	[Export] public PackedScene ItemPreviewScene {get;set;}

	private int _PendingPreviews;
	public override void _Ready()
	{
		foreach (PackedScene scene in ScenesToLoad)
		{
			ResourceLoader.LoadThreadedRequest(scene.ResourcePath);
		}
		_RegisterItems();
		
		if (_PendingPreviews == 0)
            GetTree().ChangeSceneToPacked(MainScene);
	}

	private void _RegisterItems()
	{
		ItemRegistry.RegisterAll();

		var items = ItemRegistry.GetAll();
		foreach (var item in items.Values)
		{
			var itemLoadInstance = ItemPreviewScene.Instantiate() as ItemPreview;
			itemLoadInstance.Item = item;
			itemLoadInstance.Connect("PreviewReady", new Callable(this, nameof(OnPreviewReady)));
			AddChild(itemLoadInstance);
			_PendingPreviews++;
		}
	}

	public void OnPreviewReady()
	{
		_PendingPreviews--;
		if (_PendingPreviews == 0)
            GetTree().ChangeSceneToPacked(MainScene);
	}
}
