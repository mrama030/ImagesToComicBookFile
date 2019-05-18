using System;

namespace ImagesToComicBookArchiveConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string newLine = "\n";

            Console.WriteLine("Welcome to the free Images To Comic Book File Converter" + newLine);

            if (args.Length != 2 || string.IsNullOrWhiteSpace(args[0]) || string.IsNullOrWhiteSpace(args[1]))
            {
                Console.WriteLine("This program requires a path to a folder of images, or a path to multiple folders containing images.");

                Console.WriteLine("Press enter to start...");
                Console.ReadLine();

                // Browse for a Directory prompt...
                Console.WriteLine("Please enter the path to process:");
                string path = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Optional: Please enter a substring to exclude from comic file names followed by the enter key, or press enter to skip:");
                string subToExclude = Console.ReadLine();

                Console.WriteLine(newLine + "processing...");

                ImagesToComicBookArchiveConverter.ProcessRequest(path, subToExclude);
            }
            else
            {
                ImagesToComicBookArchiveConverter.ProcessRequest(args[0], args[1]);
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
