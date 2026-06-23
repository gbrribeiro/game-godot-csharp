using System;
using Godot;

public partial class Game : Node3D
{
	[Export] public PackedScene MobScene { get; set; }
	[Export] public PathFollow3D MobSpawnLocation { get; set; }
	[Export] public Player Player { get; set; }

    public override void _EnterTree()
    {
        ItemRegistry.RegisterAll();
    }


	private void OnTimeout()
	{
		// Create a new instance of the Mob scene.
		Mob mob = MobScene.Instantiate<Mob>();

		// Choose a random location on the SpawnPath.
		// We store the reference to the SpawnLocation node.
		// var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		// And give it a random offset.
		MobSpawnLocation.ProgressRatio = GD.Randf();

		Vector3 playerPosition = Player.Position;
		mob.Initialize(MobSpawnLocation.Position, playerPosition);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}
}
