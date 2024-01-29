This is the README for the CoreTokenizer project.

This project was written by Jace Windsor on 2/7-2/8 2023.

The following entries are the files submitted and a brief description of their contents:
    
    main.py - the main method to call for execution. It accepts 
    an arg vector of any size, but will only use the first argument provided
    for the scanner.

    Token.py - A class to represent a token object containing the ID, string
    literal, and classification (uint, identifier, symbol, reserved word) along
    with methods to access this information (mostly used for debugging purposes)

    TokenScanner.py - A file containing the TokenScanner class.

        TokenScanner contains the driving force for the scanner - getToken, 
    skipToken, and tokenizeLine. It also contains intVal and idName for "runtime
    access" to these values. (By runtime access, I am referring to the ability 
    to access the associated information of a token while parsing is occuring. 
    For post-parsing access, use the methods defined in the Token.py class).
    The class also contains methods to help improve readbility and functionality
    of the tokenizeLine method.


This program was written and tested on an Ubuntu based Linux machine.
I am not entirely sure if anything changes on Windows/Mac, but use the
commands corresponding to your OS if something doesn't work as described below.

To execute the program and receive the token stream, execute the following command
within the folder that the files are saved:

    "python3 ./main.py INPUTFILE"
    where INPUTFILE is the exact name of the 
    file you wish to scan (e.g. input.txt)

To access an output used to display extra information that helps
with human visualization and debugging, execute the following command within
the folder in which the files are saved:

    "python3 -O ./main.py INPUTFILE"
    where INPUTFILE is the exact name of the 
    file you wish to scan (e.g. input.txt)

There is reportedly an issue in which these commands are reversed depending on
system settings, but this is how I achieved proper functionality on my machine.


