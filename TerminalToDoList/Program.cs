using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Managers;
using TerminalToDoList.Logger;
using TerminalToDoList.Managers;

class Program
{
    private static readonly ILogger _logger = new ConsoleLogger();
    private static readonly ITerminalToDoListManager _terminalToDoListManager = new TerminalToDoListManager(_logger);

    static void Main(string[] args)
    {
        _terminalToDoListManager.Start(args);
    }
}