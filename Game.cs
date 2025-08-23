using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine;

public static class Game
{
    private static Thread? _uiLoop;
    private static Thread? _gameThread;

    private static IPlatform? _platform;
    private static IWindow? _window;
    private static IRenderer? _renderer;
    private static IInput? _input;
    private static IGraphics? _graphics;

    public static Scene? CurrentScene;
    public static bool IsGameRun { get; private set; }

    public static void Run(IPlatform platform, IGraphics graphics)
    {
        _platform = platform;
        _graphics = graphics;
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
        _renderer = _platform!.CreateRenderer(_window!, _graphics!);
        _input = _platform!.CreateInput();

        while (IsGameRun)
        {
            _input.Update();
            Thread.Sleep(1);
        }
    }
}