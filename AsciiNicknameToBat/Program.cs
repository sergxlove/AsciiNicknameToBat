namespace AsciiNicknameToBat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Handlers.PrintBanner();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("ascii-gen> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                input = input.Trim();
                string[] parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0].ToLower();
                string argument = parts.Length > 1 ? parts[1].Trim() : "";

                switch (command)
                {
                    case "symbols":
                        Handlers.HandleCharsCommand();
                        break;
                    case "gen":
                        Handlers.HandleGenCommand(argument);
                        break;
                    case "help":
                        Handlers.HandleHelpCommand();
                        break;
                    case "instr":
                        Handlers.HandleInstrCommand();
                        break;
                    case "exit":
                        Console.WriteLine("Exiting...");
                        return;
                    case "version":
                        Handlers.HandleVersionCommand();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unknown command: '{command}'. Type 'help' to see available commands.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}