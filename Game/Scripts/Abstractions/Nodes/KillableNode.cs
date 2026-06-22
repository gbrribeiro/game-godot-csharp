using System.Collections.Generic;
using Godot;
public partial class KillableNode : Node3D
{
    [Export] public float BaseHealth { get; set; } = 1f;
    [Export] public float Health { get; set; } = 1f;
    // public List<Item> Drops { get; set; } = [];
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Kill();
    }

    public void Kill()
    {
        this.GetParent().QueueFree();
    }

    public override void _Process(double delta)
    {
        if (Health > BaseHealth)
            Health = BaseHealth;
        if (Health <= 0)
            Kill();
    }
}