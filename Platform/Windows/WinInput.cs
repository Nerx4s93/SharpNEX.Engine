using System.Runtime.InteropServices;

namespace SharpNEX.Engine.Platform.Windows
{
    internal class WinInput : IInput
    {
        private readonly short[] _keyStates = new short[256];

        public void Update()
        {
            for (var i = 0; i < 256; i++)
            {
                _keyStates[i] = GetAsyncKeyState(i);
            }
        }

        public bool IsKeyPressed(Keys key)
        {
            return (_keyStates[(int)key] & 0x8000) != 0;
        }

        public bool IsKeyDown(Keys key)
        {
            return IsKeyPressed(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return !IsKeyPressed(key);
        }

        public (int X, int Y) GetMousePosition()
        {
            GetCursorPos(out var pos);
            return (pos.X, pos.Y);
        }

        public bool IsMouseButtonPressed(int button)
        {
            var vk = button switch
            {
                0 => 0x01,
                1 => 0x02,
                2 => 0x04,
                _ => 0
            };
            return (GetAsyncKeyState(vk) & 0x8000) != 0;
        }

        public int GetMouseWheelDelta()
        {
            throw new NotImplementedException();
        }

        public float GetAxis(string axisName)
        {
            throw new NotImplementedException();
        }

        #region WinAPI

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            public int X;
            public int Y;
        }

        #endregion
    }
}
