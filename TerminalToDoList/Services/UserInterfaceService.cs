﻿using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Logger;
using TerminalToDoList.Models;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Services
{
    /// <inheritdoc cref="IUserInterfaceService"/>
    public class UserInterfaceService : IUserInterfaceService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineService _commandLineService;

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="UserInterfaceService"/>.
        /// </summary>
        public UserInterfaceService()
        {
            _logger = new ConsoleLogger();
            _commandLineService = new CommandLineService();
        }

        /// <summary>
        /// Ctor of <see cref="UserInterfaceService"/>.
        /// </summary>
        /// <param name="logger">The Logger Interface.</param>
        /// <param name="commandLineService">The CommandLineService Interface.</param>
        public UserInterfaceService(ILogger logger, ICommandLineService commandLineService)
        {
            _logger = logger;
            _commandLineService = commandLineService;
        }

        #endregion

        /// <inheritdoc cref="IUserInterfaceService.ShowVisualInterface"/>
        public void ShowVisualInterface()
        {
            while (true)
            {
                _logger.ShowMenu();

                if (Enum.TryParse(_logger.ReadLine("=> "), out UserChoice choice))
                {
                    var argument = new TerminalCmdLineArgument() { CmdLineArg = choice };

                    if (IsInputRequired(choice))
                    {
                        argument.CmdLineValue = ReadFromConsole(GetInputMessage(choice));
                    }
                    else if (choice == UserChoice.Exit)
                    {
                        if (ReadFromConsole("Are you sure (Y/N) ?").ToUpper() == "Y")
                        {
                            _logger.Log(LogLevel.Info, "Goodbye!");
                            return;
                        }
                        continue;
                    }

                    _commandLineService.CallProperService(argument);
                }
                else
                {
                    _logger.Log(LogLevel.Warning, "Invalid choice.");
                }
            }
        }

        private static bool IsInputRequired(UserChoice choice)
        {
            return choice == UserChoice.View || choice == UserChoice.ViewCompleted ||
                   choice == UserChoice.Complete || choice == UserChoice.Delete || choice == UserChoice.Add;
        }

        private static string GetInputMessage(UserChoice choice)
        {
            return choice == UserChoice.Add ? "Add new activity: " : "Id note: ";
        }

        private string ReadFromConsole(string message)
        {
            string input;
            do
            {
                input = _logger.ReadLine(message);
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

    }
}