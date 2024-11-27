using System;
using System.Collections.Generic;

namespace SharpNEX.Engine
{
    // TODO:
    //
    // public static void Save()
    public static class Debug
    {
        public class Message
        {
            public Message(MessageType messageType, DateTime dateTime, string text)
            {
                MessageType = messageType;
                DateTime = dateTime;
                Text = text;
            }

            public readonly MessageType MessageType;
            public readonly DateTime DateTime;
            public readonly string Text;

            public override string ToString()
            {
                return $"[{nameof(MessageType)}] [{DateTime.ToString("T")}] {Text}";
            }
        }

        public enum MessageType
        {
            Info,
            Warning,
            Error
        }

        private static readonly List<Message> messages = new List<Message>();
        private static string stringFormat;

        public static void Log(string text)
        {
            Log(MessageType.Info, text);
        }

        public static void Log(MessageType messageType, string text)
        {
            var message = new Message(messageType, DateTime.Now, text);
            messages.Add(message);
            stringFormat += "\n" + message.ToString();
            OnGetLog?.Invoke(message);
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

        public delegate void GetLog(Message message);

        public static event GetLog OnGetLog;
    }
}
