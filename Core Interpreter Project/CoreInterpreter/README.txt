This is the README for the CoreInterpreter project.

This project was written by Jace Windsor on 3/19/2023 for CSE3441, and updated on
3/27/2023 to add an error for double declaration. Another update on 4/4/2023 provided
additional documentation as well as errors for the assignment of a negative value.

This program was written and tested on an Ubuntu based Linux machine.
If your OS differs, please use any alternate commands appropriate to your OS.

To parse, print, and execute a Core program on a Linux machine, execute the following command
within the folder that the files are saved:

    "python3 ./main.py INPUTFILE1 INPUTFILE2"
    where INPUTFILE1 is the program and INPUTFILE2 is the data to be read in.
    Both files should be typed as they are saved on your machine (e.g. "input.txt").

Should your input program encounter an error with a line number, please reference fOut.txt.
The scanner preemptively removes any whitespace due to an interaction with python file scanning.
Any line number printed by an error will correspond with fOut.txt, not input.txt.
fOut.txt only appears if the parser fails to reach EOF in the input file.

Documentation of project
========================
This project is written in the object oriented approach as introduced in class.
Rather than discussing each individual class, I will provide a high level overview.
Each class consists of 3 main methods and potentially some helper methods, with a
class existing for each nonterminal and terminal in the Core grammar. The main
methods in each class are Parse, Print, and Execute. The parse method creates an
object based on the tokens that are read in from the tokenizer and generates a parse tree
whose initial declaration appears in the main program body to ensure persistance across
the execution of the program. Print methods are used to write to the stdout stream to
provide a visual representation of what was parsed by the interpreter. Finally, Exec methods
are used to actually run the Core program and execute the operations provided in the user program.
I have tried my best to make the code as clean as possible so as to not require extensive
documentation and believe that it follows a very straight forward process due to the
nature of recursive descent parsing. Method class from one class to the next are easy
to trace and errors are thrown where appropriate, which I believe allows for an easy understanding
of what went wrong and where. Comments are provided where I felt something may have needed
additional explaination but for the most part I feel that the code is very readible and digestible
for anybody familiar with recursion.

Parsing errors sometimes provide the user with the type of token that was found compared to what
is expected. Below is the table that corresponds with those error codes, as the token scanner
did not have an easy way of displaying them without a global dictionary:

"program" : 1,
"begin" : 2,
"end" : 3,
"int" : 4,
"if" : 5,
"then" : 6,
"else" : 7,
"while": 8,
"loop" : 9,
"read" : 10,
"write" : 11,
";" : 12,
"," : 13,
"=" : 14,
"!" : 15,
"[" : 16,
"]" : 17,
"&&": 18,
"||": 19,
"(" : 20,
")" : 21,
"+" : 22,
"-" : 23,
"*" : 24,
"!=": 25,
"==": 26,
"<" : 27,
">" : 28,
"<=": 29,
">=": 30
int : 31,
id : 32

A comment about the tokenizer - each copy does not require a reference as I have it,
but when writing the parser, I found it easier to keep track of when the item called
a method on itself rather than using an object that would be passed through. I don't believe
this causes any difference in functionality, but it may be a poor practice as each object
now requires an extra pointer in memory. That said, with the constraints of the language and
the limits to modern memory sizes, it would require a ludacriously large program before that became
an issue. If I were to be carrying this project forward and continuing to add to the language, it
may be something to consider to reduce the amount of space the parse tree takes up. I mentioned
a similar issue in the Stmt class about the declaration of ultimately unused None pointers, but
unfortunately didn't have time to go back and fix it. I worked very far ahead on this project due
to a 19 credit hour courseload this semester and hadn't been taught the polymorphic approach
at the time of implementation. 
