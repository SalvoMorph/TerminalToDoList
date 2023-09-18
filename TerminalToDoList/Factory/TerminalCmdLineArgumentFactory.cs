using TerminalToDoList.Interfaces.Factory;
using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Logger;
using TerminalToDoList.Models;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList
{
    /// <inheritdoc cref="ITerminalCmdLineArgumentFactory"/>
	public class TerminalCmdLineArgumentFactory : ITerminalCmdLineArgumentFactory
    {
        private readonly ILogger _logger;

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="TerminalCmdLineArgumentFactory"/>.
        /// </summary>
        public TerminalCmdLineArgumentFactory()
        {
            _logger = new ConsoleLogger();
        }

        /// <summary>
        /// Ctor of <see cref="TerminalCmdLineArgumentFactory"/>.
        /// </summary>
        /// <param name="logger">The Logger interface.</param>
        public TerminalCmdLineArgumentFactory(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        /// <inheritdoc cref="ITerminalCmdLineArgumentFactory.Create(string[])"/>
        public TerminalCmdLineArgument? Create(string[] args)
        {
            if (args == null || args.Count() > 2)
            {
                _logger.Log(LogLevel.Error, "Insufficient or incorrect arguments..");
                return null;
            }

            return CreateArgument(args);
        }

        private TerminalCmdLineArgument? CreateArgument(string[] args)
        {
            var result = new TerminalCmdLineArgument();

            switch (args[0].ToLowerInvariant())
            {
                case CmdLineArgs.Help:
                case CmdLineArgs.HelpExt:
                    _logger.ShowHelpMenu();
                    return result;
                case CmdLineArgs.AddNote:
                    result.CmdLineArg = UserChoice.Add;
                    break;
                case CmdLineArgs.DeleteNote:
                    if (args.Length < 2 || args[1] == CmdLineArgs.All)
                    {
                        if (_logger.ReadLine("Are you sure (Y/N) ?").ToUpper() == "Y")
                        {
                            result.CmdLineArg = UserChoice.DeleteAll;
                            result.CmdLineValue = CmdLineArgs.All;
                        }
                        return result;
                    }
                    result.CmdLineArg = UserChoice.Delete;
                    break;
                case CmdLineArgs.CompleteNote:
                    result.CmdLineArg = UserChoice.Complete;
                    break;
                case CmdLineArgs.ViewNote:
                    if (args.Length < 2 || args[1] == CmdLineArgs.All)
                    {
                        result.CmdLineArg = UserChoice.ViewAll;
                        result.CmdLineValue = CmdLineArgs.All;
                        return result;
                    }
                    result.CmdLineArg = UserChoice.View;
                    break;
                case CmdLineArgs.ViewCompletedNote:
                    if (args.Length < 2 || args[1] == CmdLineArgs.All)
                    {
                        result.CmdLineArg = UserChoice.ViewAllCompleted;
                        result.CmdLineValue = CmdLineArgs.All;
                        return result;
                    }
                    result.CmdLineArg = UserChoice.ViewCompleted;
                    break;
                default:
                    _logger.Log(LogLevel.Error, $"Command {args[0]} is not valid.");
                    return null;
            }

            if (args.Length < 2)
            {
                _logger.Log(LogLevel.Error, $"Command {args[0]} has no option. Please see -help.");
                return new TerminalCmdLineArgument();
            }

            result.CmdLineValue = args[1];

            return result;
        }
    }
}