using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Models;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Services
{
    /// <inheritdoc cref="IUserChoiceService"/>
	public class UserChoiceService : IUserChoiceService
    {
        private readonly ITerminalToDoListService _terminalToDoListService;

        Dictionary<UserChoice, Action<string>> CommandMap = new();

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="UserChoiceService"/>.
        /// </summary>
        public UserChoiceService()
        {
            _terminalToDoListService = new TerminalToDoListService();
            CreateCommandMap();
        }

        /// <summary>
        /// Ctor of <see cref="UserChoiceService"/>.
        /// </summary>
        /// <param name="terminalToDoListService">The TerminalToDoListService Interfce.</param>
        public UserChoiceService(ITerminalToDoListService terminalToDoListService)
        {
            _terminalToDoListService = terminalToDoListService;
            CreateCommandMap();
        }

        #endregion

        /// <inheritdoc cref="IUserChoiceService.GetProperTerminalServiceMethod(TerminalCmdLineArgument)"/>
        public Action<string>? GetProperTerminalServiceMethod(TerminalCmdLineArgument argument)
        {
            if (CommandMap.TryGetValue(argument.CmdLineArg, out var action))
                return action;

            return null;
        }

        private void CreateCommandMap()
        {
            CommandMap = new Dictionary<UserChoice, Action<string>>() {
                { UserChoice.Add, message => _terminalToDoListService.AddNote(message) },
                { UserChoice.View, id => _terminalToDoListService.ViewNote(Convert.ToInt32(id)) },
                { UserChoice.ViewAll, _ => _terminalToDoListService.ViewAllNote() },
                { UserChoice.ViewCompleted, id => _terminalToDoListService.ViewCompletedNote(Convert.ToInt32(id)) },
                { UserChoice.ViewAllCompleted, _ => _terminalToDoListService.ViewAllCompletedNote() },
                { UserChoice.Complete, id => _terminalToDoListService.CompleteNote(Convert.ToInt32(id)) },
                { UserChoice.Delete, id => _terminalToDoListService.DeleteNote(Convert.ToInt32(id)) },
            };
        }
    }
}