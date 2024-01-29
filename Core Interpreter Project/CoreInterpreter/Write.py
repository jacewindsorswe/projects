import IdList
import sys
class Write:

    def __init__(self,tokenizer):
        self.idList = None
        self.tokenizer = tokenizer

    def parseWrite(self):
        token = self.tokenizer.getToken()
        if token != 11:
            print("Error: parseRead expected 'write':11, was %d"%token)
            exit(0)
        self.tokenizer.skipToken()
        self.idList = IdList.IdList(self.tokenizer)
        self.idList.parseIdList()
        return
    
    def printWrite(self,offset):
        for i in range(offset):
            sys.stdout.write("\t")
        sys.stdout.write("write ")
        self.idList.printIdList()
        sys.stdout.write(";\n")

    def execWrite(self):
        self.idList.writeIdList()