import IdList
import sys
class Read:
    args = []
    argCounter = 0
    def generateArgs(file):
        with open(file,"r") as fIn:
            Read.args = fIn.read().splitlines()
        fIn.close()

    def __init__(self,tokenizer):
        self.idList = None
        self.tokenizer = tokenizer

    def parseRead(self):
        token = self.tokenizer.getToken()
        if token != 10:
            print("Error: parseRead expected 'read':10, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.idList = IdList.IdList(self.tokenizer)
        self.idList.parseIdList()
        return
    
    def printRead(self,offset):
        for i in range(offset):
            sys.stdout.write("\t")
        sys.stdout.write("read ")
        self.idList.printIdList()
        sys.stdout.write(";\n")
        return

    def execRead(self):
        Read.argCounter = self.idList.execIdList(Read.args,Read.argCounter)
        