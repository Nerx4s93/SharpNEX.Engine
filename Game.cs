using System.Diagnostics;
using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine;

public static class Game
{
    private static Thread? _gameThread;

    private static IPlatform? _platform;
    private static IWindow? _window;
    private static IRenderer? _renderer;

    private static SceneManager? _sceneManager;
    public static bool IsGameRun { get; private set; }

    public static void Run(IPlatform platform, int width, int height, List<Scene> scenes)
    {
        _platform = platform;

        _window = _platform.CreateWindow("Game", width, height);
        _renderer = _platform.CreateRenderer(_window, "GDIRenderer");
        _renderer.Init(_window.Hwnd, width, height);

        _sceneManager = new SceneManager(scenes);
        _sceneManager.LoadScene(0);

        _gameThread = new Thread(GameLoop);
        _gameThread.Start();
        IsGameRun = true;

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
            _sceneManager!.CurrentScene!.Update();
            _renderer.EndFrame();

            stopwatch.Stop();
            var frameTimeMs = (int)Math.Round(stopwatch.Elapsed.TotalMilliseconds);
            fps.AddTik(frameTimeMs);

            Console.WriteLine(fps.GetUPS());
            Thread.Sleep(1);
        }
    }
}