using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpNEX.Engine;

[Serializable]
public class GameObject(string name, List<Script> scripts)
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

    #region Создание и удаление

    public void Instantiate()
    {
        Game.Scene.Instantiate(this);
    }

    public void Instantiate(Script script)
    {
        Game.Scene.Instantiate(script, this);
    }

    public void Destroy()
    {
        Game.Scene.Destroy(this);
    }

    internal void AddScript(Script script)
    {
        _scripts.Add(script);
    }

    internal void RemoveScript(Script script)
    {
        _scripts.Remove(script);
    }

    #endregion

    #region Копирование

    public IEnumerable<GameObject> GetAllTree()
    {
        yield return this;
        foreach (var child in _children)
        {
            foreach (var descendant in child.GetAllTree())
            {
                yield return descendant;
            }
        }
    }


    public List<GameObject> GetAllTreeList()
    {
        return GetAllTree().ToList();
    }

    public GameObject Copy()
    {
        using var memoryStream = new MemoryStream();
#pragma warning disable SYSLIB0011
        var formatter = new BinaryFormatter();
        formatter.Serialize(memoryStream, this);
        memoryStream.Position = 0;
        return (GameObject)formatter.Deserialize(memoryStream);
#pragma warning restore SYSLIB0011
    }

    #endregion
}
