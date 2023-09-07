# BrainFuck
My shot at creating a brainfuck interpreter in C#


## Installation

   You can find the pre-built binaries in the releases page: `bf` (for linux x86_64) and `bf.exe` (windows x86_64)
   For use on other platforms please build from source.
   
   
## Building from source

 ### Dependencies
 This project was built using [.Net 7](https://dotnet.microsoft.com/) Download the [.Net 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) from from microsoft's site or for linux use 
 your OS package manager. There are no additional dependencies.
 
 To build the project from source, navigate to the root folder of the project (the one that contains the *.sln file) execute the dotnet build command:
 ```
 dotnet build -c Release
 ```
 For further information about the various options that the build command supports refer to the [documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build) 

 ## Usage   
 `bf [option] [file]`
 
 Here the options and input file are optional. 
 If no file is provided the program reads the code from standard input until it encounters an empty line input.
 This means you can also pipe input text from other programs. 
    
 Example: 
    
   `cat hello_world.bf | bf`
   
   The above line will read from a file called hello_world.bf and pipes it into BrainFuckInterpreter as input.

   The following can be the contents of hello_world.bf:
   ```
   >++++++++[<+++++++++>-]<.>++++[<+++++++>-]<+.+++++++..+++.>>++++++[<+++++++>-]<+
   +.------------.>++++++[<+++++++++>-]<+.<.+++.------.--------.>>>++++[<++++++++>-
   ]<+.
   ```

   Options:
   
       -h, --help, help            show this help message and exit
       -v --version, version       output the current version and exit
       --validate, validate        validate the input and print any validation errors

   Environment variables:
   
       MEMORY_STRIP_LENGTH         Set this variable to a positive integer to set how long the memory strip needs to be.
                                   Default is 40000

