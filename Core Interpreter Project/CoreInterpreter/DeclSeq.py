import Decl
class DeclSeq:

    def __init__(self,tokenizer):
        self.decl = None
        self.declSeq = None
        self.tokenizer = tokenizer
    
    def parseDeclSeq(self):
        token = self.tokenizer.getToken()
        if token != 4:
            print("Error: parser expected 'int':4, was %s"%token)
            exit(0)
        self.decl = Decl.Decl(self.tokenizer)
        self.decl.parseDecl()
        self.tokenizer.skipToken()
        token = self.tokenizer.getToken()
        if token != 2:
            self.declSeq = DeclSeq(self.tokenizer)
            self.declSeq.parseDeclSeq()
        return
    
    def printDeclSeq(self):
        self.decl.printDecl()
        if self.declSeq != None:
            self.declSeq.printDeclSeq()
        return
    
        
