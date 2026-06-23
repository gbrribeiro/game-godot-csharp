using System.Collections.Generic;
using Godot;

public partial class AttackerProperty : Node
{
    [Export] public float Damage { get; set; } = 1f;
    [Export] public float AttackSpeed { get; set; } = 1f; //Attacks per second
    [Export] public ShapeCast3D ShapeCast3D { get; set; }

    private bool _CanAttack = true;
    private float _AttackCooldown = 0f;
    private readonly HashSet<KillableProperty> _KillableNodesInRange = [];

    public override void _Process(double delta)
    {
        var attackInterval = 1f / AttackSpeed;
        if (_CanAttack)
        {
            _AttackCooldown = attackInterval;
        }
        else
        {
            _AttackCooldown -= (float)delta;
            if (_AttackCooldown <= 0f)
            {
                _CanAttack = true;
                _AttackCooldown = 0f;
            }
        }
    }
    public void Attack()
    {
        _KillableNodesInRange.Clear();
        if (!_CanAttack)
            return;

        _CanAttack = false;

        ShapeCast3D.ForceUpdateTransform();

        var collisions = ShapeCast3D.GetCollisionCount();
        for (int i = 0; i < collisions; i++)
        {
            var collider = ShapeCast3D.GetCollider(i);
            if (collider is Node node)
            {
                var killableNode = node.GetNodeInChildren<KillableProperty>();
                if (killableNode != null && !_KillableNodesInRange.Contains(killableNode))
                {
                    _KillableNodesInRange.Add(killableNode);
                    killableNode.TakeDamage(Damage);
                }
            }
        }
    }
}