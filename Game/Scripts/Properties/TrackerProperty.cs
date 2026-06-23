using Godot;

public partial class TrackerProperty : Node
{
    [Export]
	public Node3D TrackedTarget { get; set; }

	private Vector3 _Offset;
	public override void _Ready()
	{
		_Offset = this.GetParent<Node3D>().Position - TrackedTarget.Position;
	}

	public override void _Process(double delta)
	{
		this.GetParent<Node3D>().Position = TrackedTarget.Position + _Offset;
	}
}
