using System.Diagnostics;
using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine;

public static class Game
{
    private static Thread? _gameThread;

    private static IPlatform? _platform;
    private static IWindow? _window;
    private static IRenderer? _renderer;

    public static bool IsGameRun { get; private set; }

    public static void Run(IPlatform platform, int width, int height, List<Scene> scenes)
    {
        _platform = platform;

        _window = _platform.CreateWindow("Game", width, height);
        _renderer = _platform.CreateRenderer(_window, "GDIRenderer");

        _renderer.Init(_window.Hwnd, width, height);
        SceneManager.Init(scenes);

        _gameThread = new Thread(GameLoop);
        _gameThread.Start();
        IsGameRun = true;

        _window.Show();
    }

    private static void GameLoop()
    {
        var fps = new UPS();
        var stopwatch = new Stopwatch();
        while (IsGameRun)
        {
            stopwatch.Restart();

            _renderer!.BeginFrame();
            SceneManager.CurrentScene!.Update();
            _renderer.EndFrame();

            stopwatch.Stop();
            var frameTimeMs = (int)Math.Round(stopwatch.Elapsed.TotalMilliseconds);
            fps.AddTik(frameTimeMs);

            Console.WriteLine(fps.GetUPS());
            Thread.Sleep(1);
        }
    }
}