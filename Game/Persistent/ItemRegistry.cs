#nullable disable

using System.Collections.Generic;
using Godot;

public static class ItemRegistry
{
    private static readonly Dictionary<int, Item> _ItemsById = new();

    public static void RegisterItem(Item item)
    {
        if (_ItemsById.ContainsKey(item.Id))
        {
            GD.PrintErr($"Item with ID {item.Id} is already registered.");
            return;
        }
        _ItemsById[item.Id] = item;
    }

    public static void UpdateItem(Item item)
    {
        if (_ItemsById.ContainsKey(item.Id))
        {
            _ItemsById.Remove(item.Id);
            RegisterItem(item);
        }
        else RegisterItem(item);
    }

    public static Item GetItemById(int id)
    {
        if (_ItemsById.TryGetValue(id, out var item))
        {
            return item;
        }
        GD.PrintErr($"Item with ID {id} not found.");
        return null;
    }

    public static void RegisterAll()
    {
        var wood = new Item
        {
            Id = 1,
            DisplayName = "Wood",
            MaxStackSize = 10,
            Name= "Wood",
            PathToScene="res://Game/Models/Items/resource-wood.glb"
        };
        RegisterItem(wood);
    }

    public static Dictionary<int, Item> GetAll()
    {
        return _ItemsById;
    }
}