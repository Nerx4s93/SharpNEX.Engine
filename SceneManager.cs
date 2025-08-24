namespace SharpNEX.Engine;

public static class SceneManager
{
    private static List<Scene>? _scenes;

    public static Scene? CurrentScene { get; private set; }

    public static void Init(List<Scene> scenes)
    {
        _scenes = scenes;
        LoadScene(0);
    }

    public static void LoadScene(int index)
    {
        if (index < 0 || index >= _scenes!.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }
        CurrentScene = _scenes[index];
    }

    public static void LoadScene(string name)
    {
        var scene = _scenes!.FirstOrDefault(s => s.Name == name);
        CurrentScene = scene ?? throw new ArgumentException($"Сцена с именем '{name}' не найдена", nameof(name));
    }
}