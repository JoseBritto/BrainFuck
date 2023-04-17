using System.Reflection;

namespace BrainFuckInterpreter;

public static class Help
{
    private static readonly string ExeName = AppDomain.CurrentDomain.FriendlyName;
    private static readonly string HelpMessage = 
        $"""
            The Brainfuck interpreter by Britto

            Usage: 
                {ExeName} [option] [file]
                Here the options and input file are optional. 
                If no file is provided the program reads the code from standard input until it encounters an empty line input.
                This means you can also pipe input text from other programs. 
                
                Example: cat hello_world.bf | {ExeName}
                The above line will read from a file called hello_world.bf and pipes it into {ExeName} as input.

            Options:
                -h, --help, help            show this help message and exit
                -v --version, version       output the current version and exit
                --validate, validate        validate the input and print any validation errors

            Environment variables:
                MEMORY_STRIP_LENGTH         Set this variable to a positive integer to set how long the memory strip needs to be.
                                            Default is {Constants.DEFAULT_MEMORY_STRIP_LENGTH}
         """;
    
    
    public static void PrintHelp()
    {
        Console.Write(HelpMessage);
    }

    public static void PrintVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0-alpha";
        Console.WriteLine(version);
    }
}