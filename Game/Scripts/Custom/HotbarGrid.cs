using Godot;

public partial class HotbarGrid : GridContainer
{
	 public override void _Ready()
    {
        // Resized += UpdateSlots;
        UpdateSlots();
    }


    private void UpdateSlots()
    {
        int columns = Columns;

        if (columns <= 0)
            return;

        float size = Size.X / columns;

        foreach (Control child in GetChildren())
        {
            child.CustomMinimumSize = new Vector2(size, size);
        }
    }
}
