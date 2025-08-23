namespace SharpNEX.Engine;

[Serializable]
internal class GameObject(string name, List<Script> scripts)
{
    private readonly List<Script> _scripts = scripts;

    private Vector _position;
    private Rotation _rotation;
    private Vector _size;

    public string Name = name;

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

    public bool HasScript<T>()
    {
        return _scripts.Select(script => script.GetType()).
            Any(scriptType => typeof(T) == scriptType);
    }

    public bool HasScriptFromBaseType<T>()
    {
        return _scripts.Any(s => s is T);
    }

    public T GetScript<T>() where T : Script
    {
        var script = _scripts.FirstOrDefault(s => s.GetType() == typeof(T));
        if (script == null)
        {
            throw new InvalidOperationException($"Script \"{typeof(T).Name}\" is not found.");
        }

        return (T)script;
    }

    public T GetScriptFromBaseType<T>() where T : Script
    {
        var script = _scripts.FirstOrDefault(s => s is T);
        if (script == null)
        {
            throw new InvalidOperationException($"Script \"{typeof(T).Name}\" is not found.");
        }

        return (T)script;
    }

    #endregion
}