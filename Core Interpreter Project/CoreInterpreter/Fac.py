import Op
import sys
class Fac:

    def __init__(self,tokenizer):
        self.op = None
        self.fac = None
        self.tokenizer = tokenizer

    def parseFac(self):
        self.op = Op.Op(self.tokenizer)
        self.op.parseOp()
        token = self.tokenizer.getToken()
        if token == 24:
            self.tokenizer.skipToken() #skips *
            self.fac = Fac(self.tokenizer)
            self.fac.parseFac()
        return

    def printFac(self):
        self.op.printOp()
        if self.fac != None:
            sys.stdout.print(" * ")
            self.fac.printFac()
        
    def evalFac(self):
        firstVal = self.op.evalOp()
        if self.fac != None:
            secVal = self.fac.evalFac()
            return firstVal * secVal
        return firstVal