using System.Collections.ObjectModel;

namespace SharpNEX.Engine;

[Serializable]
internal class GameObject(string name, List<Script> scripts)
{
    private readonly List<Script> _scripts = scripts;
    private readonly HashSet<GameObject> _children = [];

    private Vector _position;
    private Rotation _rotation;
    private Vector _size;

    public string Name = name;

    public GameObject? Parent { get; private set; }

    public GameObject(string name) : this(name, []) { }

    #region Описание игрового объекта

    public Vector Position
    {
        get => _position;
        set => _position = value;
    }

    public Rotation Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }

    public Vector Size
    {
        get => _size;
        set => _size = value;
    }

    #endregion

    #region Скрипты

    public ReadOnlyCollection<Script> GetScripts() => _scripts.AsReadOnly();

    public bool HasScript<T>() => _scripts.Any(s => s.GetType() == typeof(T));

    public bool HasScriptFromBaseType<T>() => _scripts.Any(s => s is T);

    public T GetScript<T>() where T : Script
    {
        var script = _scripts.FirstOrDefault(s => s.GetType() == typeof(T))
                     ?? throw new InvalidOperationException($"Script \"{typeof(T).Name}\" is not found.");
        return (T)script;
    }

    public T GetScriptFromBaseType<T>() where T : Script
    {
        var script = _scripts.FirstOrDefault(s => s is T)
                     ?? throw new InvalidOperationException($"Script \"{typeof(T).Name}\" is not found.");
        return (T)script;
    }

    #endregion

    #region Зависимости

    public IReadOnlyCollection<GameObject> Children => _children;

    public void AddChild(GameObject? child)
    {
        if (child == null || child == this)
        {
            return;
        }

        child.Parent?.RemoveChild(child);
        child.Parent = this;
        _children.Add(child);
    }

    public void RemoveChild(GameObject? child)
    {
        if (child == null || child.Parent != this)
        {
            return;
        }

        child.Parent = null;
        _children.Remove(child);
    }

    public void SetParent(GameObject? newParent)
    {
        Parent?.RemoveChild(this);
        newParent?.AddChild(this);
    }

    #endregion
}
