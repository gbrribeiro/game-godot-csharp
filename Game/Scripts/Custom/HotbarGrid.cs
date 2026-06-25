using Godot;

public partial class HotbarGrid : GridContainer
{
    private Vector2 _CurrentSize;
    private bool _updatingSlots = false;

    public override void _Ready()
    {
        _CurrentSize = this.Size;
        UpdateSlots();
        _CurrentSize = this.Size;
    }

    public void OnRezised()
    {
        if (_updatingSlots)
            return;

        bool sameX = Mathf.IsEqualApprox(_CurrentSize.X, Size.X);
        bool sameY = Mathf.IsEqualApprox(_CurrentSize.Y, Size.Y);
        if (sameX && sameY)
            return;

        _updatingSlots = true;
        UpdateSlots();
        _CurrentSize = this.Size;
        _updatingSlots = false;
    }


    private void UpdateSlots()
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
