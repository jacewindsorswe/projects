import Op
import CompOp
import sys
class Comp:

    def __init__(self,tokenizer):
        self.op1 = None
        self.op2 = None
        self.compOp = None
        self.tokenizer = tokenizer

    def parseComp(self):
        token = self.tokenizer.getToken()
        if token != 20:
            print("Error: parseComp expected '(':20, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.op1 = Op.Op(self.tokenizer)
        self.op1.parseOp()
        self.compOp = CompOp.CompOp(self.tokenizer)
        self.compOp.parseCompOp()
        self.op2 = Op.Op(self.tokenizer)
        self.op2.parseOp()
        token = self.tokenizer.getToken()
        if token != 21:
            print("Error: parseComp expected ')':21, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        return
    
    def printComp(self):
        sys.stdout.write("(")
        self.op1.printOp()
        self.compOp.printCompOp()
        self.op2.printOp()
        sys.stdout.write(")")
        return

    def evalComp(self):
        match(self.compOp.compOpNum):
            case 25:
                return (self.op1.evalOp() != self.op2.evalOp())
            case 26:
                return (self.op1.evalOp() == self.op2.evalOp())
            case 27:
                return (self.op1.evalOp() < self.op2.evalOp())
            case 28:
                return (self.op1.evalOp() > self.op2.evalOp())
            case 29:
                return (self.op1.evalOp() <= self.op2.evalOp())
            case 30:
                return (self.op1.evalOp() >= self.op2.evalOp())
            case default:
                print("Error: could not evaluate comparison block")
                exit(0)