namespace BrainFuckInterpreter;

public class Runtime
{
    private byte[] _memory;

    public Runtime(int memoryLength = 30_000)
    {
        _memory = new byte[memoryLength];
    }
    
    public void Execute(string code)
    {
        var memoryPointer = 0;
        var codePointer = 0;

        var codeLength = code.Length;
        while (codePointer < codeLength)
        {
            var instruction = code[codePointer];

            if (instruction == Tokens.LOOP_START)
            {
                if (_memory[memoryPointer] == 0)
                    codePointer =
                        FindMatchingLoopEnd(code,
                            codePointer); // Since our code was validated we should find a closing bracket.
                else
                    codePointer++;
                continue;
            }
            
            if (instruction == Tokens.LOOP_END)
            {
                if (_memory[memoryPointer] != 0)
                    codePointer =
                        FindMatchingLoopStart(code,
                            codePointer); // Since our code was validated we should find a closing bracket.
                else
                    codePointer++;
                continue;
            }
            
            switch (instruction)
            {
                case Tokens.INCREMENT_POINTER:
                    if (_memory.Length == memoryPointer + 1)
                        memoryPointer =
                            0; // Maybe provide a warning if memory is all memory used up causing an unintentional overwrite?
                    else
                        memoryPointer++;
                    break;
                
                case Tokens.DECREMENT_POINTER:
                    if (memoryPointer == 0)
                        memoryPointer = _memory.Length - 1; // Wrap to end
                    else
                        memoryPointer--;
                    break;
                
                case Tokens.C_OUT:
                    Print(_memory[memoryPointer]);
                    break;
                
                case Tokens.C_IN:
                    _memory[memoryPointer] = GetInput();
                    break;
                
                case Tokens.DECREMENT_DATA:
                    _memory[memoryPointer]--;
                    break;
                case Tokens.INCREMENT_DATA:
                    _memory[memoryPointer]++;
                    break;
                
            }

            codePointer++;
        }
    }

    private int FindMatchingLoopEnd(string code, int codePointer)
    {
        var stack = new Stack<byte>();
        for (int i = codePointer + 1; i < code.Length; i++)
        {
            if(code[i] == Tokens.LOOP_START)
                stack.Push(1);
            else if (code[i] == Tokens.LOOP_END)
                if (stack.TryPop(out _) == false)
                    return i;
        }

        throw new Exception("Undefined behaviour: Unmatched brackets!");
    }

    private int FindMatchingLoopStart(string code, int codePointer)
    {
        var stack = new Stack<byte>();
        for (int i = codePointer - 1; i >= 0; i--)
        {
            if(code[i] == Tokens.LOOP_END)
                stack.Push(1);
            else if (code[i] == Tokens.LOOP_START)
                if (stack.TryPop(out _) == false)
                    return i;
        }

        throw new Exception("Undefined behaviour: Unmatched brackets!");
    }


    private byte GetInput()
    {
        return (byte)(char)Console.Read();
    }

    private void Print(byte b)
    {
        Console.Write((char)b);
    }
}