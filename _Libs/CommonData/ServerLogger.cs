using System;

namespace CommonData {

    public static class ServerLogger {

        public static void WriteError(string msg) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}]" + msg, DateTime.Now);
            Console.ResetColor();
        }

        public static void WriteDebug(string msg) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[{0}]" + msg, DateTime.Now);
            Console.ResetColor();
        }

        public static void WriteLog(string msg) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[{0}]" + msg, DateTime.Now);
            Console.ResetColor();
        }
    }
}