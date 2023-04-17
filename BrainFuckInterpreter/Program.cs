
using System.Security;
using System.Text;
using BrainFuckInterpreter;

string code;

if (args.Length >= 1)
{
    try
    {
        code = await File.ReadAllTextAsync(args[0]);
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
    
}
else
{
    string? s;
    var partialCode = new StringBuilder();
    while (string.IsNullOrEmpty(s = Console.ReadLine()) == false)
    {
        partialCode.AppendLine(s);
    }

    code = partialCode.ToString();
}

if (Validator.IsValidCode(code, out var validationError) == false)
{
    Console.Error.WriteLine($"Validation Warning: {validationError}");
}

var memoryLengthStr = Environment.GetEnvironmentVariable("MEMORY_STRIP_LENGTH")?.Trim() ?? "40000";

if (int.TryParse(memoryLengthStr, out var memLength) == false)
{
    Console.Error.WriteLine($"{memoryLengthStr} is not a valid value for memory length");
    return 3;
}

new Runtime(memLength).Execute(code);

return 0;