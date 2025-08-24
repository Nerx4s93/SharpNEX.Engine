using System.Diagnostics;
using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine;

public static class Game
{
    private static Thread? _gameThread;

    private static IPlatform? _platform;
    private static IWindow? _window;
    private static IRenderer? _renderer;

    public static Scene? CurrentScene { get; internal set; }
    public static bool IsGameRun { get; private set; }

    public static void Run(IPlatform platform, Scene scene)
    {
        CurrentScene = scene;

        _platform = platform;
        IsGameRun = true;

        _gameThread = new Thread(GameLoop);
        _gameThread.Start();

        _window = _platform.CreateWindow("Game", 720, 480);
        _renderer = _platform.CreateRenderer(_window, "GDIRenderer");

        _window.Show();
    }

    private static void GameLoop()
    {
        var fps = new UPS();
        var stopwatch = new Stopwatch();
        while (IsGameRun)
        {
            stopwatch.Restart();

            CurrentScene!.Update();

            stopwatch.Stop();
            var frameTimeMs = (int)Math.Round(stopwatch.Elapsed.TotalMilliseconds);
            fps.AddTik(frameTimeMs);

            Console.WriteLine(fps.GetUPS());
            Thread.Sleep(1);
        }
    }
}