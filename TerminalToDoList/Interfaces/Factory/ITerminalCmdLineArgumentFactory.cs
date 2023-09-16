using TerminalToDoList.Models;

namespace TerminalToDoList.Interfaces.Factory
{
    /// <summary>
    /// The Terminal CmdLine Argument Factory.
    /// </summary>
    public interface ITerminalCmdLineArgumentFactory
	{
        /// <summary>
        /// Create the argument.
        /// </summary>
        /// <param name="args">The parameters from the cmd line.</param>
        /// <returns>A <see cref="TerminalCmdLineArgument"/>.</returns>
        TerminalCmdLineArgument? Create(string[] args);
	}
}