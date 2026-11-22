using AsciiNicknameToBat.Core;

namespace AsciiNicknameToBat
{
    public class Handlers
    {
        public static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==========================================");
            Console.WriteLine("   ASCII Art to BAT File Generator CLI    ");
            Console.WriteLine("==========================================");
            Console.ResetColor();
            Console.WriteLine("Type 'help' to see available commands.\n");
            Console.WriteLine("Type 'gen <text>' to generate art.\n");
        }

        public static void HandleGenCommand(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Usage: gen <text>");
                Console.WriteLine("Example: gen hello world");
                Console.ResetColor();
                return;
            }

            try
            {
                text = text.Trim('"', '\'');
                text = text.ToLower(); 

                string art = BatGeneratorService.GenerateArt(text);

                Console.WriteLine();
                Console.WriteLine(art);

                string filePath = BatGeneratorService.Generate(text);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Success! File created at: {filePath}\n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error generating file: {ex.Message}\n");
                Console.ResetColor();
            }
        }

        public static void HandleHelpCommand()
        {   
            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine("  gen <text>    - Generate ASCII art and save it as a .bat file.");
            Console.WriteLine("  symbols       - Show a list of all available characters.");
            Console.WriteLine("  version       - Show the application version.");
            Console.WriteLine("  help          - Show this general help message.");
            Console.WriteLine("  instr         - Show instructions on how to add the .bat to the system PATH.");
            Console.WriteLine("  exit          - Close the application.\n");
        }

        public static void HandleInstrCommand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- How to add your .bat file to PATH ---");
            Console.ResetColor();
            Console.WriteLine("1. Create a folder for your scripts (e.g., C:\\Bats) and move your .bat file into it.");
            Console.WriteLine("2. Press Win + R, type 'sysdm.cpl', and press Enter.");
            Console.WriteLine("3. Go to the 'Advanced' tab -> 'Environment Variables'.");
            Console.WriteLine("4. In the top section ('User variables'), select 'Path' and click 'Edit'.");
            Console.WriteLine("5. Click 'New', paste the path to your folder (e.g., C:\\Bats), and click OK.");
            Console.WriteLine("6. Restart your terminal. Now you can run your file from any folder!\n");
        }

        public static void HandleCharsCommand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nAvailable Characters:");
            Console.ResetColor();
            Console.WriteLine(BatGeneratorService.GetAvailableCharacters());
        }

        public static void HandleVersionCommand()
        {
            const string AppVersion = "1.0.0";
            Console.WriteLine($"ASCII Art to BAT Generator v{AppVersion}\n");
        }
    }
}
