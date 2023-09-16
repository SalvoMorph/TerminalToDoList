using TerminalToDoList.Interfaces.Managers;
using TerminalToDoList.Managers;

class Program
{
    private static readonly ITerminalToDoListManager _terminalToDoListManager = new TerminalToDoListManager();

    static void Main(string[] args)
    {
        _terminalToDoListManager.Start(args);
    }
}