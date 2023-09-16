using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Models
{
	/// <summary>
	/// The Comand line argument.
	/// </summary>
	public class TerminalCmdLineArgument
	{
		/// <summary>
		/// The User Choice.
		/// </summary>
		public UserChoice CmdLineArg { get; set; }

		/// <summary>
		/// The value.
		/// </summary>
		public string? CmdLineValue { get; set; }
	}
}