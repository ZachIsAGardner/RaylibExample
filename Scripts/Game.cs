using System.Numerics;
using Raylib_cs;

namespace RaylibExample;

public static class Game
{
    public static string ROOT => $"{AppDomain.CurrentDomain.BaseDirectory}../../../Content";

    public static List<Thing> Things = new List<Thing>() { };

    static List<Thing> thingsToDraw = new List<Thing>() { };
    static List<Thing> thingsToUpdate = new List<Thing>() { };

    public static List<T> GetThings<T>() where T : Thing
    {
        return Things.Where(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))).Select(c => c as T).ToList();
    }

    public static void Process()
    {
        Start();

        Raylib.SetExitKey(Raylib_cs.KeyboardKey.Null);
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    public static void Start()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.VSyncHint);
        Raylib.InitWindow(480, 270, "RaylibExample");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        Example();
    }

    public static void Update()
    {
        Time.Update();

        // NOT performant. Should only update this when something changes, not every frame
        thingsToUpdate = Things
            .Where(t => t.Active && !t.Removed)
            .OrderByDescending(t => t.UpdateOrder)
            .ToList();
        foreach (Thing thing in thingsToUpdate)
        {
            thing.Update();
        }
    }

    public static void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SkyBlue);

        // NOT performant. Should only update this when something changes, not every frame
        thingsToDraw = Things
            .Where(t => t.Visible && !t.Removed)
            .OrderByDescending(t => t.DrawOrder)
            .ToList();
        foreach (Thing thing in thingsToDraw)
        {
            thing.Draw();
        }

        Raylib.EndDrawing();
    }

    // ...

    static void Example()
    {
        new Player()
        {
            Position = new Vector2(32)
        };

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                new Coin()
                {
                    Position = new Vector2(64) + new Vector2(x * 64, y * 64)
                };
            }
        }
    }
}