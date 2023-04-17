namespace BrainFuckInterpreter;

public static class Validator
{
    public static bool IsValidCode(string code, out string? error)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            error = "Empty input. Nothing to execute!";
            return false;
        }

        if (code.IsAllLoopsMatched(Tokens.LOOP_START, Tokens.LOOP_END) == false)
        {
            error = "Unmatched brackets! Code might behave unexpectedly";
            return false;
        }

        
        error = null;
        return true;


    }

    private static bool IsAllLoopsMatched(this string code, char open, char close)
    {
        var stack = new Stack<byte>();

        foreach (var c in code)
        {
            if(c == open)
                stack.Push(1); // Push a dummy data for opening brackets
            else if (c == close)
            {
                if (stack.TryPop(out _) == false)
                    return false; // Nothing in stack. Meaning there was no opening bracket for this closed bracket
            }
        }

        if (stack.TryPop(out _))
        {
            return false; // There was still items left meaning there was one or more unclosed opening bracket
        }

        return true;
    }
}