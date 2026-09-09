using System.Numerics;
using Raylib_cs;

namespace RaylibExample;

public class AnimationState
{
    public int Start;
    public int End;
    public bool Loop = true;
    public float Speed = 10;
}

public class Sprite : Thing
{
    public Texture2D Texture;
    public float Rotation = 0f;
    public Vector2 Scale = new Vector2(1, 1);
    public Color Color = Color.White;
    public int TileNumber = 0;
    public int TileSize = 0;
    public bool FlipHorizontally = false;
    public bool FlipVertically = false;
    public Vector2? Origin = null;

    // Animation
    public AnimationState State;
    float animatedTileNumber;
    AnimationState lastState;

    public override void Update()
    {
        base.Update();

        if (State != null)
        {
            UpdateAnimationState();
        }
    }

    void UpdateAnimationState()
    {
        if (lastState != State)
        {
            animatedTileNumber = State.Start;
        }
        lastState = State;

        animatedTileNumber += State.Speed * Time.Delta;
        if (animatedTileNumber >= State.End + 1)
        {
            if (State.Loop) animatedTileNumber = State.Start;
            else animatedTileNumber = State.End;
        }

        TileNumber = (int)Math.Floor(animatedTileNumber);
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public Vector2 CoordinatesFromNumber(int tileNumber, Texture2D texture, int tileSize)
    {
        if (tileSize > texture.Width || tileSize > texture.Height) return Vector2.Zero;

        var tilesPerRow = texture.Width / tileSize;
        int row = 0;

        while (tileNumber >= tilesPerRow)
        {
            tileNumber -= tilesPerRow;
            row++;
        }

        return new Vector2(tileNumber * tileSize, row * tileSize);
    }

    public override void Draw()
    {
        base.Draw();

        Vector2 coord = CoordinatesFromNumber(TileNumber, Texture, TileSize);

        Rectangle source = new Rectangle(coord.X, coord.Y, TileSize * (FlipHorizontally ? -1 : 1), TileSize * (FlipVertically ? -1 : 1));
        Rectangle destination = new Rectangle(GlobalPosition.X, GlobalPosition.Y, TileSize * Scale.X, TileSize * Scale.Y);

        Raylib.DrawTexturePro(
            texture: Texture,
            source: source,
            dest: destination,
            origin: Origin ?? new Vector2(TileSize / 2f),
            rotation: Rotation,
            tint: Color
        );
    }
}
