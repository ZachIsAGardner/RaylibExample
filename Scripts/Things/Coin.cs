using System.Numerics;
using Raylib_cs;
using RaylibExample;

public class Coin : Thing
{
    public Sprite Sprite;
    public Collider Collider;

    Sound sound;

    public Coin()
    {
        Collider = (Collider)AddChild(new Collider()
        {
            Bounds = new Vector2(16)
        });

        Sprite = (Sprite)AddChild(new Sprite()
        {
            TileSize = 16,
            Texture = Raylib.LoadTexture($"{Game.ROOT}/Sprites/Coin.png")
        });

        sound = Raylib.LoadSound($"{Game.ROOT}/SoundEffects/Coin.ogg");
    }

    public override void Destroy()
    {
        Raylib.PlaySound(sound);

        base.Destroy();
    }
}