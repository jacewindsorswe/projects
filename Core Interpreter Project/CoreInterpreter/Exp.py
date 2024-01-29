import Fac
import sys
class Exp:

    def __init__(self,tokenizer):
        self.fac = None
        self.exp = None
        self.manipulator = None
        self.tokenizer = tokenizer
    
    def parseExp(self):
        self.fac = Fac.Fac(self.tokenizer)
        self.fac.parseFac()
        token = self.tokenizer.getToken()
        match(token):
            case 22:
                self.manipulator = 1 # positive for addition
            case 23:
                self.manipulator = -1 # negative for subtraction
            case default:
                return
        self.tokenizer.skipToken()
        self.exp = Exp(self.tokenizer)
        self.exp.parseExp()
        return
    
    def printExp(self):
        self.fac.printFac()
        if self.manipulator != None:
            if self.manipulator > 0:
                sys.stdout.write(" + ")
            else:
                sys.stdout.write(" - ")
            self.exp.printExp()

    def evalExp(self):
        if self.exp == None:
            return self.fac.evalFac()     
        if self.manipulator > 0:
            return self.fac.evalFac() + self.exp.evalExp()
        if self.manipulator < 0:
            return self.fac.evalFac() - self.exp.evalExp()
        print("Execution Error: Couldn't evalute expression")
        exit(0)
        

        
        