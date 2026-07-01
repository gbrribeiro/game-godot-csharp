using Godot;

[GlobalClass]
public partial class ItemSlot : Button
{
    [Export] public Item Item {get;set;}
    [Export] public int Quantity {get;set;}
    public bool IsFull => Item.MaxStackSize == Quantity;

    public override void _Ready()
    {
        if(Item != null && !string.IsNullOrEmpty(Item.PathToIcon))
        {
            Icon = GD.Load<Texture2D>(Item.PathToIcon);
        }
    }

        public override Variant _GetDragData(Vector2 atPosition)
        {
            // bloqueia drag de slot vazio
            if (Item == null) return default;
            
            var preview = new TextureRect
            {
                Texture = this.Icon,
                CustomMinimumSize = new Vector2(64, 64),
                ExpandMode = TextureRect.ExpandModeEnum.FitHeightProportional,
                MouseFilter = MouseFilterEnum.Ignore
            };

            SetDragPreview(preview);

            return this;
        }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.AsGodotObject() is ItemSlot;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var origem = data.AsGodotObject() as ItemSlot;

        // troca os dados entre os dois slots
        (Item,     origem.Item)     = (origem.Item,     Item);
        (Quantity, origem.Quantity) = (origem.Quantity, Quantity);

        // atualiza os ícones dos dois slots
        Icon        = Item != null ? GD.Load<Texture2D>(Item.PathToIcon) : null;
        origem.Icon = origem.Item != null ? GD.Load<Texture2D>(origem.Item.PathToIcon) : null;
    }
}