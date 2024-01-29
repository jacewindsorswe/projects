import enum
import Token
import io
from typing import List
class characterType(enum.Enum):
    LL = 1
    UL = 2
    DD = 3
    SS = 4
    WS = 5

charArrays = {}
charArrays[characterType.LL] = ['a','b','c','d','e','f','g','h','i','j','k','l','m'
,'n','o','p','q','r','s','t','u','v','w','x','y','z']
charArrays[characterType.UL] = ['A','B','C','D','E','F','G','H','I','J','K','L','M'
,'N','O','P','Q','R','S','T','U','V','W','X','Y','Z']
charArrays[characterType.DD] = ['1','2','3','4','5','6','7','8','9','0']
charArrays[characterType.SS] = [';',',','=','!','[',']','&','|','(',')','+','-',
'*','<','>']
charArrays[characterType.WS] = [' ','\t','\n','\r','\f','\v']

statesDict = {
"acceptableRWStates" : [
"p","pr","pro","prog","progr","progra",
"b","be","beg","begi",
"e","en",
    "el","els",
"i","in",
"t","th","the",
"l","lo","loo",
"r","re","rea",
"w","wh","whi","whil",
   "wr","wri","writ"],

"finalRWStates" : ["program","begin","end","int","if","then","else",
"while","loop","read","write"],

"acceptableSSStates" : ["=","!","&","|","<",">"],

"finalSSStates" : [";",",","[","]","(",")","+",
"-","*"],

"finalPairSSStates" : ['&&','||','==','!=','<=','>=']

}

symbolIDs = {
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
}

class TokenScanner():

    def __init__(self,fileName):   
        self.currentPos = 0
        file = io.open(fileName,'r')
        self.file = file
        tokens = TokenScanner.tokenizeLine(self.file)
        self.completeTokenArr = tokens

    def getToken(self):
        return ((self.completeTokenArr[self.currentPos]).returnID())    
    
    def skipToken(self):
        if(self.currentPos == len(self.completeTokenArr)-1):
            if not __debug__:
                print("scanning next line")
            tokens = TokenScanner.tokenizeLine(self.file)
            self.completeTokenArr.extend(tokens)
        if(self.currentPos == len(self.completeTokenArr)):
            self.currentPos-=1
        currID = TokenScanner.getToken(self)
        if not (currID == 33 or currID == 34):
            self.currentPos += 1
    
    def intVal(self):
        if TokenScanner.getToken(self) == 31:
            return int(self.completeTokenArr[self.currentPos].str)
        else:
            print("Error: Current token not an int")
            exit(0)
    
    def idName(self):
        if TokenScanner.getToken(self) == 32:
            return self.completeTokenArr[self.currentPos].str
        else:
            print("Error: Current token not an int")
            exit(0)
    
    def atEOF(self):
        idNum = self.completeTokenArr[self.currentPos].returnID()
        if  idNum == 33 or idNum == 34:
            print(self.getToken())
            if idNum == 34:
                print("Error: bad token found. Program Terminating.")
            return True
        return False
        
    def tokenizeLine(file):
        tokenArr = []
        currToken = Token.Token()
        currPOS = 0
        line = file.readline()
        line.rstrip()
        flagCount = 0
        if not line.isspace():
            for char in line:
                charType = TokenScanner.classifyChars(char)
                match(charType):
                    case characterType.LL:
                        TokenScanner.handleLL(currToken,char)   
                    case characterType.SS:
                        currToken,tokenArr,flagCount = TokenScanner.handleSS(currToken,tokenArr,char,line,currPOS,flagCount)
                    case characterType.UL:
                        TokenScanner.handleUL(currToken,char)
                    case characterType.DD:
                        TokenScanner.handleDD(currToken,char)
                    case characterType.WS:
                        if(len(currToken.str)>0):
                            tokenArr.append(currToken)
                            currToken = Token.Token()
                    case default:
                        print("default case, you shouldn't be here")
                if(currToken.id == 34):
                    tokenArr.append(currToken)
                    break
                currPOS+=1
            if(currPOS==len(line)):
                if(not currToken.returnID() == -1):
                    tokenArr.append(currToken)
            if(line == ""):
                EOFToken = Token.Token()
                EOFToken.str = "EOF"
                EOFToken.id = 33
                tokenArr.append(EOFToken)
        return tokenArr

    def classifyChars(c):
        if c in charArrays[characterType.LL]:
            return characterType.LL
        elif c in charArrays[characterType.UL]:
            return characterType.UL
        elif c in charArrays[characterType.SS]:
            return characterType.SS
        elif c in charArrays[characterType.DD]:
            return characterType.DD
        elif c.isspace():
            return characterType.WS

    def handleLL(currToken,char):
        tokenStr = currToken.str + char
        if len(currToken.str) == 0 and currToken.TokenType == Token.TokenType.Empty:
            currToken.TokenType = Token.TokenType.ReservedWord
        if tokenStr in statesDict["acceptableRWStates"]:
            currToken.str=tokenStr
        elif tokenStr in statesDict["finalRWStates"]:
            currToken.str=tokenStr
            currToken.id = statesDict["finalRWStates"].index(currToken.str)+1
            #add 1 to index to compensate for starting at pos0
        else:
            currToken.str = tokenStr
            currToken.id = 34

    # This is probably the hardest method to understand so I will explain it here.
    # nextSymb is used to determine if the symbol is an acceptable 2 symbol token
    # if so, it increments a flag to indicate that, so it will be handled properly
    # in the next loop.
    #
    # The first If/Elif indicates token type as a symbol or completes the token
    # if it is not a symbol.
    # The second if/elif determines if the character is a final symbol state.
    # If it is not, it checks the next character in the line and throws the
    # aforementioned flag to indicate that it can be greedy and produce an
    # accepting token.
    #
    # The if/else associate with not flagcount > 0 handles whether to check the
    # next char and flag if necessary or to complete the token in the presence of
    # a flag.
    def handleSS(currToken,tokenArr,char,line,currPOS,flagCount):
        nextSymb = ""
        if(currToken.TokenType == Token.TokenType.Empty):
            currToken.TokenType = Token.TokenType.SpecialSymbol
        elif not currToken.TokenType == Token.TokenType.SpecialSymbol:
            tokenArr.append(currToken)
            currToken = Token.Token()
        if(char in statesDict['finalSSStates']):
            currToken.str = str(char)
            currToken.id = symbolIDs[currToken.str]
            tokenArr.append(currToken)
            currToken = Token.Token()
        elif(char in statesDict['acceptableSSStates']):
            if(not flagCount > 0):
                if[currPOS < len(line)-1]:
                    nextSymb = line[currPOS+1]
                if (char+nextSymb) in statesDict['finalPairSSStates']:
                    currToken.str = str(char)
                    flagCount+=1
                else:
                    tokenStr = currToken.str + char
                    currToken.str = tokenStr
                    currToken.id = symbolIDs[currToken.str]
                    tokenArr.append(currToken)
                    currToken = Token.Token()
            else:
                tokenStr = currToken.str + char
                currToken.str = tokenStr
                currToken.id = symbolIDs[currToken.str]
                tokenArr.append(currToken)
                currToken = Token.Token()
                flagCount-=1
        return currToken,tokenArr,flagCount

    def handleUL(currToken,char):
        tokenStr = currToken.str + char
        if len(currToken.str) == 0 and currToken.TokenType == Token.TokenType.Empty:
            currToken.TokenType = Token.TokenType.ID
            currToken.id=32
        # the for loops and bool below check to confirm the current string matches
        # the RE for an ID
        isULandDigit = True
        for c in charArrays[characterType.LL]:
            if (c in currToken.str):
                isULandDigit = False
        for s in charArrays[characterType.SS]:
            if (s in currToken.str):
                isULandDigit = False

        if (not currToken.TokenType == Token.TokenType.ID) or not isULandDigit:
            currToken.str = tokenStr
            currToken.id = 34
        else:
            currToken.str = tokenStr
            currToken.id = 32

    def handleDD(currToken,char):
        tokenStr = currToken.str + char
        if len(currToken.str) == 0 and currToken.TokenType == Token.TokenType.Empty:
            currToken.TokenType = Token.TokenType.UInt
            currToken.id=31
        # Similar to handleUL, the for loops and bool make sure the currStr is 
        # in adherence to the RE for a UInt
        isDigit = True
        for c in charArrays[characterType.LL]:
            if (c in currToken.str):
                isDigit = False
        for s in charArrays[characterType.SS]:
            if (s in currToken.str):
                isDigit = False
        for ul in charArrays[characterType.UL]:
            if (ul in currToken.str):
                isDigit = False
        if (currToken.TokenType == Token.TokenType.UInt or currToken.TokenType == Token.TokenType.ID):
            currToken.str = tokenStr
        else:
            currToken.str = tokenStr
            currToken.id = 34
