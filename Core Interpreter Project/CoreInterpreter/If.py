import StmtSeq
import Cond
import sys
class If:

    def __init__(self,tokenizer):
        self.cond = None
        self.stmtSeq = None
        self.stmtSeq2 = None
        self.tokenizer = tokenizer

    def parseIf(self):
        token = self.tokenizer.getToken()
        if token !=  5:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseIf expected 'if':5 token, was %s"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.cond = Cond.Cond(self.tokenizer)
        self.cond.parseCond()
        token = self.tokenizer.getToken()
        if token != 6:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseIf expected 'then':6, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.stmtSeq = StmtSeq.StmtSeq(self.tokenizer)
        self.stmtSeq.parseStmtSeq()
        token = self.tokenizer.getToken()
        if token == 7:
            self.tokenizer.skipToken()
            self.stmtSeq2 = StmtSeq.StmtSeq(self.tokenizer)
            self.stmtSeq2.parseStmtSeq()
            return
        elif token != 3:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseIf expected 'end':3, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        token = self.tokenizer.getToken()
        if token != 12:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseIf expected ';':12,was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        return
    
    def printIf(self,offset):
        for i in range(offset):
            sys.stdout.write("\t")
        sys.stdout.write("if ")
        self.cond.printCond()
        sys.stdout.write(" then\n")
        self.stmtSeq.printStmtSeq(offset+1)
        if self.stmtSeq2 != None:
            for i in range(offset):
                sys.stdout.write("\t")
            sys.stdout.write("else\n")
            self.stmtSeq2.printStmtSeq(offset+1)
        for i in range(offset):
            sys.stdout.write("\t")
        sys.stdout.write("end;\n")
        return
    
    def execIf(self):
        cond = self.cond.evalCond()
        if cond:
            self.stmtSeq.execStmtSeq()
        else:
            if self.stmtSeq2 != None:
                self.stmtSeq2.execStmtSeq()
        return