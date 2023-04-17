
using System.Security;
using System.Text;
using BrainFuckInterpreter;

string code;

if (args.Length >= 1)
{
    var validateAndExit = false;
    
    var option = args[0].ToLower();
    var encounteredOption = false;
    
    
    switch (option)
    {
        case "--validate":
        case "validate":
            validateAndExit = encounteredOption = true;
            break;
                
        case "version":
        case "-v":
        case "--version":
            Help.PrintVersion();
            encounteredOption = true;
            return 0;
                
        case "help":
        case "-h":
        case "--help":
            Help.PrintHelp();
            encounteredOption = true;
            return 0;
    }
    
    
    try
    {
        if((encounteredOption && args.Length > 1) || !encounteredOption)
            code = await File.ReadAllTextAsync(args[^1]); // Use the last argument as the file name as we can use the first as an option
        else
        {
            code = ReadFromStdIn();
        }
    }
    catch (IOException e)
    {
        Console.Error.WriteLine("I/O Exception: " + e.Message);
        return 1;
    }
    catch (SecurityException e)
    {
        Console.Error.WriteLine("Security Exception: " + e.Message);
        return 1;
    }
    catch (UnauthorizedAccessException e)
    {
        Console.Error.WriteLine("UnauthorizedAccessError: " + e.Message);
        return 1;
    }
    catch (Exception e)
    {
        Console.Error.WriteLine($"An unexpected error occured! Couldn't read file {args[0]}\n{e.Message}");
        return 1;
    }
    
    
    if(validateAndExit)
    {
        if (Validator.IsValidCode(code, out var validationError) == false)
        {
            Console.WriteLine($"Validation failed: {validationError}");
        }
        else
        {
            Console.WriteLine($"Validation passed! No errors were found!");
        }
        return 0;
    }
    
}
else
{
    code = ReadFromStdIn();
}


var memoryLengthStr = Environment.GetEnvironmentVariable(Constants.MEMORY_STRIP_LENGTH)?.Trim() 
                      ?? Constants.DEFAULT_MEMORY_STRIP_LENGTH.ToString();

if (int.TryParse(memoryLengthStr, out var memLength) == false || memLength < 2)
{
    Console.Error.WriteLine($"{memoryLengthStr} is not a valid value for memory strip length");
    return 3;
}

new Runtime(memLength).Execute(code);

return 0;

string ReadFromStdIn()
{
    string code1;
    string? s;
    var partialCode = new StringBuilder();
    while (string.IsNullOrEmpty(s = Console.ReadLine()) == false)
    {
        partialCode.AppendLine(s);
    }

    code1 = partialCode.ToString();
    return code1;
}