using System.Drawing;
using System.Numerics;

namespace RaylibExample;

public class Collider : Thing
{
    public Vector2 Bounds;

    public static bool CheckRectangleOverlap(Collider a, Collider b) => CheckRectangleOverlap(
            a: new Rectangle((int)a.GlobalPosition.X, (int)a.GlobalPosition.Y, (int)a.Bounds.X, (int)a.Bounds.Y),
            b: new Rectangle((int)b.GlobalPosition.X, (int)b.GlobalPosition.Y, (int)b.Bounds.X, (int)b.Bounds.Y)
        );
    public static bool CheckRectangleOverlap(Rectangle a, Rectangle b)
    {
        // If one Collider is on left side of other  
        if (a.Left >= b.Right || b.Left >= a.Right)
        {
            return false;
        }

        // If one Collider is above other  
        if (a.Top >= b.Bottom || b.Top >= a.Bottom)
        {
            return false;
        }

        return true;
    }

    public bool IsTouching(Collider other)
    {
        return CheckRectangleOverlap(this, other);
    }
}