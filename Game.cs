using System.Diagnostics;
using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine;

public static class Game
{
    private static Thread? _gameThread;

    public static IPlatform? Platform { get; private set; }
    public static IWindow? Window { get; private set; }
    public static IRenderer? Renderer { get; private set; }

    public static bool IsGameRun { get; private set; }

    public static void Run(IPlatform platform, string renderType, int width, int height, List<Scene> scenes)
    {
        Platform = platform;

        Window = Platform.CreateWindow("Game", width, height);
        Renderer = Platform.CreateRenderer(Window, renderType);

        Renderer.Init(Window.Hwnd, width, height);
        SceneManager.Init(scenes);

        _gameThread = new Thread(GameLoop);
        _gameThread.Start();
        IsGameRun = true;

        Window.Show();
    }

    private static void GameLoop()
    {
        var fps = new UPS();
        var stopwatch = new Stopwatch();
        while (IsGameRun)
        {
            stopwatch.Restart();

            Renderer!.BeginFrame();
            SceneManager.CurrentScene!.Update();
            Renderer.EndFrame();

            stopwatch.Stop();
            var frameTimeMs = (int)Math.Round(stopwatch.Elapsed.TotalMilliseconds);
            fps.AddTik(frameTimeMs);

            Console.WriteLine(fps.GetUPS());
            Thread.Sleep(1);
        }
    }
}