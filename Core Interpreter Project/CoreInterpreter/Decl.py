import IdList
import sys
class Decl:
    declOver = False

    def __init__(self,tokenizer):
        self.idList = None
        self.tokenizer = tokenizer

    def parseDecl(self):
        token = self.tokenizer.getToken()
        if token != 4:
            print("Error: parser expected 'int', was '%s'"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.idList = IdList.IdList(self.tokenizer)
        self.idList.parseIdList()
        token = self.tokenizer.getToken() 
        if token != 12:
            print("Error: parser expected ';', was '%s'"%token)
            exit(0)
        return
    
    def printDecl(self):
        sys.stdout.write("int ")
        self.idList.printIdList()
        sys.stdout.write("; ")
        return

    def execDecl(self):
        self.idList.execIdList()
        return
