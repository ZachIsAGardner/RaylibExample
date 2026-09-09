
using System.Numerics;
using Newtonsoft.Json;
using Raylib_cs;

namespace RaylibExample;

public class Thing
{
    static int id;

    public int Id;
    public string Name;
    public HashSet<string> Tags = new HashSet<string>() { };

    public Vector2 Position;
    public Vector2 GlobalPosition => (Parent?.GlobalPosition ?? new Vector2()) + Position;

    public Thing Parent;
    public List<Thing> Children = new List<Thing>() { };

    // Order
    public float UpdateOrder;
    public float DrawOrder;

    // Status
    public bool Active { get; private set; } = true;
    public bool Visible { get; private set; } = true;
    public bool Removed { get; private set; } = false;

    bool didStart;

    public Thing() : this(null) { }
    public Thing(string name = null, Vector2? position = null, float drawOrder = 0, float updateOrder = 0)
    {
        Id = id;
        id++;
        Game.Things.Add(this);

        Name = name;
        Position = position ?? Vector2.Zero;
        DrawOrder = drawOrder;
        UpdateOrder = updateOrder;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (!didStart)
        {
            Start();
            didStart = true;
        }
    }

    public virtual void Draw()
    {

    }

    public virtual void Destroy()
    {
        Removed = true;
        if (Parent != null)
        {
            Parent.RemoveChild(this);
        }
        Children.ToList().ForEach(c => c.Destroy());
        Game.Things.Remove(this);
    }

    public virtual Thing AddChild(Thing child)
    {
        if (child.Parent != null) child.Parent.RemoveChild(child);
        Children.Add(child);
        child.Parent = this;
        return child;
    }

    public virtual Thing RemoveChild(Thing child)
    {
        Children.Remove(child);
        child.Parent = null;
        return child;
    }

    public T GetThing<T>() where T : Thing
    {
        return this as T ?? Children.Find(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))) as T;
    }

    public List<T> GetThings<T>() where T : Thing
    {
        return Children.Where(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))).Select(c => c as T).ToList();
    }

    public T GetThingWithTag<T>(string tag) where T : Thing
    {
        T result = null;
        if (Tags.Contains(tag)) result = this as T;
        if (result != null) return result;
        result = Children.Find(c => c.Tags.Contains(tag) && c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))) as T;
        return result;
    }

    public void AddTag(string tag)
    {
        Tags.Add(tag);
    }

    public void RemoveTag(string tag)
    {
        Tags.Remove(tag);
    }
}
