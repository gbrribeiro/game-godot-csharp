using System.Collections.Generic;
using Godot;
public partial class KillableProperty : Node
{
    [Export] public float BaseHealth { get; set; } = 1f;
    [Export] public float Health { get; set; } = 1f;

    public override void _Process(double delta)
    {
        if (Health > BaseHealth)
            Health = BaseHealth;
        if (Health <= 0)
            Kill();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Kill();
    }

    public void Kill()
    {
        DropItems();
        this.GetParent().QueueFree();
    }

    
    public void DropItems()
    {
        var children = this.GetChildren();
        foreach (var child in children)
        {
            if(child is DropItemProperty dropItemNode)
            {
                var parent = (Node3D) this.GetParent();
                dropItemNode.Drop(dropItemNode.GetDropQuantity(), parent.Position);
            }
        }
    }
}