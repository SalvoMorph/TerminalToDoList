using TerminalToDoList.Models;

namespace TerminalToDoList.Interfaces.Services
{
    /// <summary>
    /// The CommandLine Service.
    /// </summary>
	public interface ICommandLineService
	{
        /// <summary>
        /// Call the proper service.
        /// </summary>
        /// <param name="argument">The <see cref="TerminalCmdLineArgument"/>.</param>
        void CallProperService(TerminalCmdLineArgument argument);
    }
}