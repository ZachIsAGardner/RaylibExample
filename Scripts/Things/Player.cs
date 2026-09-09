using System.Numerics;
using Raylib_cs;

namespace RaylibExample;

public class Player : Thing
{
    public Sprite Sprite;
    public Collider Collider;

    AnimationState idle = new AnimationState() { Start = 0 };
    AnimationState walk = new AnimationState() { Start = 1, End = 4 };

    public Player()
    {
        Collider = (Collider)AddChild(new Collider()
        {
            Bounds = new Vector2(16)
        });

        Sprite = (Sprite)AddChild(new Sprite()
        {
            TileSize = 32,
            Texture = Raylib.LoadTexture($"{Game.ROOT}/Sprites/Garbanzo.png")
        });
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        float speed = 150f;

        Vector2 input = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) input.Y--;
        if (Raylib.IsKeyDown(KeyboardKey.A)) input.X--;
        if (Raylib.IsKeyDown(KeyboardKey.S)) input.Y++;
        if (Raylib.IsKeyDown(KeyboardKey.D)) input.X++;

        if (input.X != 0) Sprite.FlipHorizontally = input.X == -1;

        if (input == Vector2.Zero) Sprite.State = idle;
        else Sprite.State = walk;

        Position += input * speed * Time.Delta;

        var coins = Game.GetThings<Coin>();
        foreach (var coin in coins)
        {
            if (Collider.IsTouching(coin.Collider))
            {
                coin.Destroy();
            }
        }
    }
}