import DeclSeq
import StmtSeq
import Assign
import sys
class Prog:
    
    def __init__(self,tokenizer):
        self.declSeq = None
        self.stmtSeq = None
        self.tokenizer = tokenizer
    
    def parseProg(self):
        token = self.tokenizer.getToken()
        if token != 1:
            print("Error: parseProg expected 'program':1, was '%d'"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.declSeq = DeclSeq.DeclSeq(self.tokenizer)
        self.declSeq.parseDeclSeq()
        token = self.tokenizer.getToken()
        if token != 2:
            print("Error: parseProg expected 'begin':2, was '%d'"%token)
            exit(0)
        self.tokenizer.skipToken()
        Assign.Assign.updateDeclOver()
        self.stmtSeq = StmtSeq.StmtSeq(self.tokenizer)
        self.stmtSeq.parseStmtSeq()
        token = self.tokenizer.getToken()
        if token != 3:
            print("Error: parseProg expected 'end':3, was '%d'"%token)
            exit(0)
        self.tokenizer.skipToken()
        return

    def printProg(self):
        sys.stdout.write("program ")
        self.declSeq.printDeclSeq()
        sys.stdout.write("begin \n")
        self.stmtSeq.printStmtSeq(1)
        sys.stdout.write("end\n")
        return

    def execProg(self):
        self.stmtSeq.execStmtSeq()
        return