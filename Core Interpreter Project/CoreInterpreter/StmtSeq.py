import Stmt
import sys
class StmtSeq:

    def __init__(self,tokenizer):
        self.stmt = None
        self.stmtSeq = None
        self.tokenizer = tokenizer
    
    def parseStmtSeq(self):
        token = self.tokenizer.getToken()
        if token not in {5,8,10,11,32}:
            print("Error on line "+str(self.tokenizer.lineNum) +": parseStmtSeq expected stmt alternative (5,8,10,11,32), was '%s'"%token)
            exit(0)
        self.stmt = Stmt.Stmt(self.tokenizer)
        self.stmt.parseStmt()
        self.tokenizer.skipToken()
        token = self.tokenizer.getToken()
        if token == 7: #found else, end SS
            return
        if token == 12:
            self.tokenizer.skipToken()
        token = self.tokenizer.getToken()
        if token != 3:
            self.stmtSeq = StmtSeq(self.tokenizer)
            self.stmtSeq.parseStmtSeq()
        return
    
    def printStmtSeq(self,offset):
        self.stmt.printStmt(offset)
        if self.stmtSeq != None:
            self.stmtSeq.printStmtSeq(offset)
        return
    
    def execStmtSeq(self):
        self.stmt.execStmt()
        if self.stmtSeq != None:
            self.stmtSeq.execStmtSeq()
        return
        
