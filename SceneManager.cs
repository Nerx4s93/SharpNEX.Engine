namespace SharpNEX.Engine;

public class SceneManager(List<Scene> scenes)
{
    private Scene? _currentScene;

    public Scene? CurrentScene => _currentScene;

    public void LoadScene(int index)
    {
        if (index < 0 || index >= scenes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }
        _currentScene = scenes[index];
    }

    public void LoadScene(string name)
    {
        var scene = scenes.FirstOrDefault(s => s.Name == name);
        _currentScene = scene ?? throw new ArgumentException($"Сцена с именем '{name}' не найдена", nameof(name));
    }
}