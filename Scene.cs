using System.Collections.ObjectModel;

namespace SharpNEX.Engine;

public class Scene(string name, List<GameObject> gameObjects)
{
    public readonly string Name = name;
    private readonly List<GameObject> _gameObjects = gameObjects;

    private readonly List<GameObject> _loadGameObjects = [];
    private readonly List<Script> _loadScripts = [];
    private readonly List<GameObject> _destroyGameObjects = [];
    private readonly List<Script> _destroyScripts = [];

    public Scene(string name) : this(name, []) { }

    public ReadOnlyCollection<GameObject> GetGameObjects()
    {
        return _gameObjects.AsReadOnly();
    }

    public void Instantiate(Script script, GameObject gameObject)
    {
        script.GameObject = gameObject;
        _loadScripts.Add(script);
        script.Awake();

        if (!Game.IsGameRun)
        {
            Instantiate();
        }
    }

    public GameObject Instantiate(GameObject gameObject)
    {
        var copyGameObjects = gameObject.Copy().GetAllTreeList();
        foreach (var script in copyGameObjects.SelectMany(copyGameObject => copyGameObject.GetScripts()))
        {
            script.Awake();
        }

        _loadGameObjects.AddRange(copyGameObjects);

        if (!Game.IsGameRun)
        {
            Instantiate();
        }

        return copyGameObjects[0];
    }

    public GameObject Instantiate(GameObject gameObject, GameObject parent)
    {
        var copyGameObjects = gameObject.Copy().GetAllTreeList();
        foreach (var script in copyGameObjects.SelectMany(copyGameObject => copyGameObject.GetScripts()))
        {
            script.Awake();
        }

        copyGameObjects[0].SetParent(parent);
        _loadGameObjects.AddRange(copyGameObjects);

        if (!Game.IsGameRun)
        {
            Instantiate();
        }

        return copyGameObjects[0];
    }

    public void Destroy(Script script)
    {
        _destroyScripts.Add(script);

        if (!Game.IsGameRun)
        {
            Destroy();
        }
    }

    public void Destroy(GameObject gameObject)
    {
        var destroy = gameObject.GetAllTreeList();
        _destroyGameObjects.AddRange(destroy);

        if (!Game.IsGameRun)
        {
            Destroy();
        }
    }

    internal void Update()
    {
        Instantiate();
        Destroy();
        StartScripts();

        foreach (var gameObject in _gameObjects)
        {
            var scripts = gameObject.GetScripts();

            foreach (var script in scripts)
            {
                script.Update();
            }
        }
    }

    private void StartScripts()
    {
        foreach (var script in _loadScripts)
        {
            script.Start();
        }
        _loadScripts.Clear();
    }

    private void Instantiate()
    {
        _gameObjects.AddRange(_loadGameObjects);
        foreach (var script in _loadGameObjects.SelectMany(copyGameObject => copyGameObject.GetScripts()))
        {
            script.OnEnable();
        }
        _loadGameObjects.Clear();

        foreach (var script in _loadScripts)
        {
            var gameObject = script.GameObject;
            gameObject.AddScript(script);
            script.OnEnable();
        }
    }

    private void Destroy()
    {
        foreach (var gameObject in _destroyGameObjects)
        {
            _gameObjects.Remove(gameObject);
        }

        foreach (var script in _destroyScripts)
        {
            var gameObject = script.GameObject;
            gameObject?.RemoveScript(script);
        }
    }
}