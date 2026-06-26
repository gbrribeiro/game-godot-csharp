using Godot;

public partial class HotbarGrid : GridContainer
{
    public override void _Ready()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        int columns = Columns;

        if (columns <= 0)
            return;

        float size = Size.X / columns;
        Vector2 newMin = new Vector2(size, size);

        foreach (Control child in GetChildren())
        {
            bool equalX = Mathf.IsEqualApprox(child.CustomMinimumSize.X, newMin.X);
            bool equalY = Mathf.IsEqualApprox(child.CustomMinimumSize.Y, newMin.Y);
            if (!equalX || !equalY)
                child.CustomMinimumSize = newMin;
        }

    }
}
