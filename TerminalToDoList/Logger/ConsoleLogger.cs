using System.Text;
using TerminalToDoList.Interfaces.Logger;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Logger
{
	/// <inheritdoc cref="ILogger"/>
	public class ConsoleLogger : ILogger
    {
        /// <inheritdoc cref="ILogger.Log(LogLevel, string)"/>
        public void Log(LogLevel level, string message)
		{
            Console.ForegroundColor = GetColorForLogLevel(level);
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <inheritdoc cref="ILogger.ShowMenu"/>
        public void ShowMenu()
        {
            Log(LogLevel.Info, GetMenutext());
        }

        /// <inheritdoc cref="ILogger.ShowHelpMenu()"/>
        public void ShowHelpMenu()
        {
            Log(LogLevel.None, GetHelpMenuText());
        }

        #region Private Methods

        private static string GetMenutext()
        {
            var sb = new StringBuilder();
            sb.AppendLine("####### Terminal ToDoList menu #######");
            sb.AppendLine("Your choise?");
            sb.AppendLine("1. Add a new note.");
            sb.AppendLine("2. Show all the activities.");
            sb.AppendLine("3. Complete an activity.");
            sb.AppendLine("4. Delete an activity.");
            sb.AppendLine("5. Exit.");

            return sb.ToString();
        }

        private static ConsoleColor GetColorForLogLevel(LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => ConsoleLoggerColor.Info,
                LogLevel.Warning => ConsoleLoggerColor.Warning,
                LogLevel.Error => ConsoleLoggerColor.Error,
                _ => ConsoleLoggerColor.Default,
            };
        }

        private static string GetHelpMenuText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("######################################### Terminal ToDoList menu #########################################");
            sb.AppendLine("# TerminalToDoList [options]");
            sb.AppendLine("#");
            sb.AppendLine("# -note          Add a note.");
            sb.AppendLine("# -v             View a note. All to see all the notes.");
            sb.AppendLine("# -c             Complete an activity.");
            sb.AppendLine("# -d             Delete an activity.");
            sb.AppendLine("#########################################################################################################");
            return sb.ToString();
        }

        #endregion
    }
}