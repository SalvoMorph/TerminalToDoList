﻿using System.Text;
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

        /// <inheritdoc cref="ILogger.ReadLine(string)"/>
        public string ReadLine(string message)
        {
            Console.ForegroundColor = GetColorForLogLevel(LogLevel.ReadLine);
            Console.WriteLine();
            Console.Write(message);
            var input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        #region Private Methods

        private static string GetMenutext()
        {
            var sb = new StringBuilder();
            sb.AppendLine("####### Terminal ToDoList menu #######");
            sb.AppendLine("Your choise?");
            sb.AppendLine();
            sb.AppendLine($"{(int)UserChoice.Add} => Add a new note.");
            sb.AppendLine($"{(int)UserChoice.Complete} => Complete a note.");
            sb.AppendLine($"{(int)UserChoice.Delete} => Delete a note.");
            sb.AppendLine();
            sb.AppendLine($"{(int)UserChoice.View} => Show a specific note.");
            sb.AppendLine($"{(int)UserChoice.ViewCompleted} => Show a specific completed note.");
            sb.AppendLine($"{(int)UserChoice.ViewAll} => Show all notes.");
            sb.AppendLine($"{(int)UserChoice.ViewAllCompleted} => Show all completed note.");
            sb.AppendLine();
            sb.AppendLine($"{(int)UserChoice.DeleteAll} => Delete All.");
            sb.AppendLine($"{(int)UserChoice.Exit} => Exit.");
            sb.AppendLine();
            return sb.ToString();
        }

        private static ConsoleColor GetColorForLogLevel(LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => ConsoleLoggerColor.Info,
                LogLevel.Warning => ConsoleLoggerColor.Warning,
                LogLevel.Error => ConsoleLoggerColor.Error,
                LogLevel.ReadLine => ConsoleLoggerColor.ReadLine,
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