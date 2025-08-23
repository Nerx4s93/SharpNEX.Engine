namespace SharpNEX.Engine;

[Serializable]
public class Script
{
    public GameObject GameObject { get; internal set; }

    #region Описание игрового объекта

    public string Name
    {
        get => GameObject.Name;
        set => GameObject.Name = value;
    }

    public Vector Position
    {
        get => GameObject.Position;
        set => GameObject.Position = value;
    }

    public Rotation Rotation
    {
        get => GameObject.Rotation;
        set => GameObject.Rotation = value;
    }

    public Vector Size
    {
        get => GameObject.Size;
        set => GameObject.Size = value;
    }

    #endregion

    public virtual void Awake() { }
    public virtual void OnEnable() { }
    public virtual void Start() { }
    public virtual void Update() { }
}