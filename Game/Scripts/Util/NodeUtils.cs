using Godot;

public static class NodeUtils
{
    public static T? GetNodeInChildren<T>(this Node parent) where T : Node
    {
        foreach (var child in parent.GetChildren())
        {
            if (child is T node)
                return node;
        }
        return null;
    }
}