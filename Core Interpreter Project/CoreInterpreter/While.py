import StmtSeq
import Cond
import sys
class While:

    def __init__(self,tokenizer):
        self.cond = None
        self.stmtSeq = None
        self.tokenizer = tokenizer

    def parseWhile(self):
        token = self.tokenizer.getToken()
        if token != 8:
            print("Error: parseWhile expected 'while':8, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.cond = Cond.Cond(self.tokenizer)
        self.cond.parseCond()
        token = self.tokenizer.getToken()
        if token != 9:
            print("Error: parseWhile expected 'loop':9, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.stmtSeq = StmtSeq.StmtSeq(self.tokenizer)
        self.stmtSeq.parseStmtSeq()
        token = self.tokenizer.getToken()
        if token != 3:
            print("Error: parseWhile expected 'end':3, was %d"% token)
            exit(0)
        self.tokenizer.skipToken()
        token = self.tokenizer.getToken()
        if token != 12:
            print("Error: parseIf expected ';':12,was %d"%token)
            exit(0)
        return
    
    def printWhile(self,offset):
        for i in range(offset):
            sys.stdout.write("\t")
        sys.stdout.write("while ")
        self.cond.printCond()
        sys.stdout.write(" loop\n")
        self.stmtSeq.printStmtSeq(offset+1)
        for i in range(offset):
            sys.stdout.write("\t")
        sys.stdout.write("end;\n")

    def execWhile(self):
        cond = self.cond.evalCond()
        while(cond):
            self.stmtSeq.execStmtSeq()
            cond = self.cond.evalCond()
        return