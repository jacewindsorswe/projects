import Id
import Int
import Exp
import sys
class Op:

    def __init__(self, tokenizer):
        self.int = None
        self.id = None
        self.exp = None
        self.tokenizer = tokenizer

    def parseOp(self):
        token = self.tokenizer.getToken()
        match(token):
            case 31:
                self.int = Int.Int(self.tokenizer)
                self.int.parseInt()
            case 32:
                self.id = Id.Id(self.tokenizer)
                self.id.parseId()
                if self.id.literal not in Id.Id.returnDeclIds():
                    print("Error on line "+str(self.tokenizer.lineNum) +": Attempted to operate on undeclared variable '%s'"%self.id.literal)
                    exit(0)
            case 20:
                self.skipToken()
                self.exp = Exp.Exp(self.tokenizer)
                self.exp.parseExp()
                token = self.tokenizer.getToken()
                if token != 21:
                    print("Error: parseOp expected ')':21, was %d"%token)
                    
                    exit(0)
                self.tokenizer.skipToken()
            case default:
                print("Error: parseOp expected int/id/'(' (31,32,20), was %d"%token)
                print("Are you attempting to assign a negative integer to a variable?")
                exit(0)
        return
    
    def printOp(self):
        if self.id != None:
            self.id.printId()
        elif self.int != None:
            self.int.printInt()
        elif self.exp != None:
            sys.stdout.write("(")
            self.exp.printExp()
            sys.stdout.write(")")
        return
    
    def evalOp(self):
        if self.int != None:
            return self.int.literal
        elif self.id != None:
            if self.id.isInstantiated():
                return self.id.getVal()
            else:
                print("Error: Attempting to operate on uninitialized variable "+ str(self.id.literal))
                exit(0)
        elif self.exp != None:
            return self.exp.evalExp()
        