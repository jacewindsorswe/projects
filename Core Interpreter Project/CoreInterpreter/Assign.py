import Id
import Exp
import sys
class Assign:
    declOver = False
    def __init__(self,tokenizer):
        self.id = None
        self.exp = None
        self.tokenizer = tokenizer
    
    def parseAssign(self):
        self.id = Id.Id(self.tokenizer)
        self.id.parseId()
        if(Assign.declOver and self.id.literal not in Id.Id.returnDeclIds()):
            print("Error on line "+str(self.tokenizer.lineNum)+": Attempt to assign undeclared variable '%s'"%self.id.literal)
            exit(0)
        token = self.tokenizer.getToken()
        if token != 14:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseAssign expected '=':14, was %d"% token)
            exit(0)
        self.tokenizer.skipToken()
        self.exp = Exp.Exp(self.tokenizer)
        self.exp.parseExp()
        token = self.tokenizer.getToken()
        if token != 12:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseAssign expected ';':12, was %d"% token)
            exit(0)
        return
    
    def printAssign(self,offset):
        for i in range(offset):
            sys.stdout.write("\t")
        self.id.printId()
        sys.stdout.write(" = ")
        self.exp.printExp()
        sys.stdout.write(";\n")
        return
    
    def execAssign(self):
        newVal = self.exp.evalExp()
        self.id.updateId(newVal)
        return
    
    def updateDeclOver():
        Assign.declOver = True
        return
