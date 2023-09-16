using TerminalToDoList.Models;

namespace TerminalToDoList.Interfaces.Services
{
	/// <summary>
	/// UserChoice Service.
	/// </summary>
	public interface IUserChoiceService
    {
        /// <summary>
        /// Get the proper <see cref="TerminalToDoListService"/> method.
        /// </summary>
        /// <param name="argument">The <see cref="TerminalCmdLineArgument"/></param>
        /// <returns>A <see cref="TerminalToDoListService"/> Action.</returns>
        Action<string>? GetProperTerminalServiceMethod(TerminalCmdLineArgument argument);
	}
}