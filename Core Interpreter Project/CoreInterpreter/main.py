import sys
import os
import TokenScanner
import Prog
import Read

def main(argv):

    # This removes whitespace lines from the input. Because of the way
    # Python parses files, there is no way of distinguishing an EOF
    # from a line of whitespace. Both whitespace and EOF read as "".
    # This is particularly an issue when trying to determine if you are
    # actually at the EOF or if there is just repeating lines of whitespace.
    # While an arbitrary counter could work (e.g. incrementing once per
    # empty line up to X times before adding the EOF) this would only allow X
    # blank lines in a program before an automatic EOF triggers, regardless of
    # whether or not the file is actually at EOF. If a program had X+1 lines between
    # its valid statements,(disregarding good practices, it would still be valid)
    # it would terminate before it is actually complete.
    # Therefore, I just remove any fully blank whitespace lines here. The scanner
    # is still able to handle whitespace characters within a line with text, so
    # I see no reason that this shouldn't be an acceptable solution given
    # the constraints of the language.
    with open(argv[0],"r") as fIn, open("fOut.txt","w") as fOut:
        for i in fIn.readlines():
            if not i.strip():
                continue
            if i:
                fOut.write(i)
    fIn.close()
    fOut.close()
    Read.Read.generateArgs(argv[1])

    ts = TokenScanner.TokenScanner("fOut.txt")
    EOF = False
    parseTree = Prog.Prog(ts)
    while(not EOF):
        parseTree.parseProg()
        #print(ts.getToken())
        #ts.skipToken()
        EOF = ts.atEOF()
    sys.stdout.write("=============================================\n")
    parseTree.printProg()
    sys.stdout.write("=============================================\n")
    print("Input: "+str(Read.Read.args))
    print("Output:")
    parseTree.execProg()
    if not __debug__:
        for i in range(len(ts.completeTokenArr)):
            print(ts.completeTokenArr[i].returnLiteral() + " is ID: " + str(ts.completeTokenArr[i].returnID()))
        
    os.remove("fOut.txt")



if __name__=="__main__":
    main(sys.argv[1:])

