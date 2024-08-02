using System.Threading;

namespace SharpNEX.Engine
{
    public class Game
    {
        private readonly string _name;

        private Thread _gameThread;

        public Game(string Name, Scene Scene)
        {
            _name = Name;
            this.Scene = Scene;
        }

        public Scene Scene;

        public void Run()
        {
            _gameThread = new Thread(Handler);
            _gameThread.Start();
        }

        public void Stop()
        {
            _gameThread.Abort();
        }

        public void Handler()
        {
            while (true)
            {
                Scene.Update();
            }
        }
    }
}
