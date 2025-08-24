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

    public static void Run(IPlatform platform, int width, int height, Scene scene)
    {
        CurrentScene = scene;

        _platform = platform;
        IsGameRun = true;

        _window = _platform.CreateWindow("Game", width, height);

        _renderer = _platform.CreateRenderer(_window, "GDIRenderer");
        _renderer.Init(_window.Hwnd, width, height);

        _gameThread = new Thread(GameLoop);
        _gameThread.Start();

        _window.Show();
    }

    private static void GameLoop()
    {
        var texture = _renderer!.CreateTexture(@"C:\Users\Никита\Desktop\1631e421-7b40-44fc-8854-de43748294a9.jpg");

        var fps = new UPS();
        var stopwatch = new Stopwatch();
        while (IsGameRun)
        {
            stopwatch.Restart();

            _renderer.BeginFrame();
            _renderer.DrawTexture(texture, 50, 50, texture.Width, texture.Height);
            CurrentScene!.Update();
            _renderer.EndFrame();

            stopwatch.Stop();
            var frameTimeMs = (int)Math.Round(stopwatch.Elapsed.TotalMilliseconds);
            fps.AddTik(frameTimeMs);

            Console.WriteLine(fps.GetUPS());
            Thread.Sleep(1);
        }
    }
}