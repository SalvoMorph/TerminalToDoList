namespace TerminalToDoList.Models
{
    /// <summary>
    /// The TerminalToDoList Constants.
    /// </summary>
    public class TerminalToDoListConstants
    {
        /// <summary>
        /// The Log Level.
        /// </summary>
        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            ReadLine,
            None
        }

        /// <summary>
        /// The Colors to use in the console.
        /// </summary>
        public class ConsoleLoggerColor
        {
            /// <summary>
            /// Dark Green to use to log Info.
            /// </summary>
            public const ConsoleColor Info = ConsoleColor.DarkGreen;

            /// <summary>
            /// Yellow to use to log Info.
            /// </summary>
            public const ConsoleColor Warning = ConsoleColor.Yellow;

            /// <summary>
            /// Red to use to log Info.
            /// </summary>
            public const ConsoleColor Error = ConsoleColor.Red;

            /// <summary>
            /// White for tdefault.
            /// </summary>
            public const ConsoleColor ReadLine = ConsoleColor.Magenta;

            /// <summary>
            /// White for tdefault.
            /// </summary>
            public const ConsoleColor Default = ConsoleColor.White;
        }

        /// <summary>
        /// The CmdLine arguments.
        /// </summary>
        public class CmdLineArgs
        {
            public const string AddNote = "-a";
            public const string DeleteNote = "-d";
            public const string CompleteNote = "-c";
            public const string ViewNote = "-v";
            public const string ViewCompletedNote = "-vc";
            public const string Help = "-h";
            public const string HelpExt = "-help";

            public const string All = "ALL";
        }

        /// <summary>
        /// The User choice.
        /// </summary>
        public enum UserChoice
        {
            Add = 1,
            Complete = 2,
            Delete = 3,
            View = 4,
            ViewCompleted = 5,
            ViewAll = 6,
            ViewAllCompleted = 7,
            DeleteAll = 8,
            Exit = 9
        }
    }
}