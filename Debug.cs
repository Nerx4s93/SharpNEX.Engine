using System;
using System.Collections.Generic;

namespace SharpNEX.Engine
{
    // TODO:
    //
    // public static void LogError()
    // public static void Save()
    // Log handle
    public static class Debug
    {
        private static readonly List<Message> messages = new List<Message>();
        private static string stringFormat;

        public static void Log(string text)
        {
            var message = new Message(DateTime.Now, text);
            messages.Add(message);
            stringFormat += "\n" + message.ToString();
        }

        public static void Clear()
        {
            messages.Clear();
            stringFormat = string.Empty;
        }

        public static string GetAllText()
        {
            return stringFormat;
        }

        public static string GetLine(int index)
        {
            return messages[index].ToString();
        }

        public class Message
        {
            public Message(DateTime dateTime, string text)
            {
                DateTime = dateTime;
                Text = text;
            }

            public readonly DateTime DateTime;
            public readonly string Text;

            public override string ToString()
            {
                return $"[{DateTime.ToString("T")}] {Text}";
            }
        }
    }
}
