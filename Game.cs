using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine;

public static class Game
{
    private static Thread? _uiLoop;
    private static Thread? _gameThread;

    private static IPlatform? _platform;
    private static IWindow? _window;

    public static Scene? CurrentScene;
    public static bool IsGameRun { get; private set; }

    public static void Run(IPlatform platform)
    {
        _platform = platform;
        IsGameRun = true;

        _uiLoop = new Thread(UILoop);
        _gameThread = new Thread(GameLoop);

        _uiLoop.Start();
        _gameThread.Start();
    }

    private static void UILoop()
    {
        _window = _platform!.CreateWindow("Game", 720, 480);
    }

    private static void GameLoop()
    {
        while (IsGameRun)
        {
            Thread.Sleep(1);
        }
    }
}