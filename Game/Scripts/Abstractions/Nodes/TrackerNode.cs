using Godot;

public partial class TrackerNode : Node3D
{
    [Export]
	public Node3D TrackedTarget { get; set; }

	private Vector3 _Offset;
	public override void _Ready()
	{
		_Offset = this.Position - TrackedTarget.Position;
	}

	public override void _Process(double delta)
	{
		this.Position = TrackedTarget.Position + _Offset;
	}
}
