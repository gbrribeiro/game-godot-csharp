using Godot;

public partial class Player : CharacterBody3D
{
	[Export] public int Speed { get; set; } = 14;

	[Export] public float Gravity { get; set; } = 3f;

	private KillableProperty KillableNode { get; set; }
	private AttackerProperty AttackerNode { get; set; }

	private Vector3 _TargetVelocity = Vector3.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var children = this.GetChildren();
		foreach (var child in children)
		{
			if (child is KillableProperty killableNode)
				KillableNode = killableNode;
			else if (child is AttackerProperty attackerNode)
				AttackerNode = attackerNode;
		}
	}

	// // // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
		
	// }

	public override void _PhysicsProcess(double delta)
	{
		//FORWARD IS Z-
		
		// We create a local variable to store the input direction.
		var direction = Vector3.Zero;

		// We check for each move input and update the direction accordingly.
		if (Input.IsActionPressed("move_right"))
		{
			direction.X++;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X--;
		}
		if (Input.IsActionPressed("move_down"))
		{
			direction.Z++;
		}
		if (Input.IsActionPressed("move_up"))
		{
			direction.Z--;
		}

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			// Setting the basis property will affect the rotation of the node.
			this.Basis = Basis.LookingAt(direction);
		}

		// Ground velocity
		_TargetVelocity.X = direction.X * Speed;
		_TargetVelocity.Z = direction.Z * Speed;

		// Vertical velocity
		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		{
			_TargetVelocity.Y -= Gravity * (float)delta;
		}

		// Moving the character
		Velocity = _TargetVelocity;
		MoveAndSlide();

		if (Input.IsActionJustPressed("attack"))
		{
			AttackerNode.Attack();
		}
	}

}
