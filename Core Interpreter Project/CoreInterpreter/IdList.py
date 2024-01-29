import Id
import Decl
import sys
class IdList:

    def __init__(self,tokenizer):
        self.id = None
        self.idList = None
        self.tokenizer = tokenizer

    def parseIdList(self):
        token = self.tokenizer.getToken()
        if token != 32:
            print("Error: parser expected an ID (32), was '%d'",token)
            exit(0)
        self.id = Id.Id(self.tokenizer)
        self.id.parseId()
        if Decl.Decl.declOver and self.id.literal not in Id.Id.returnDeclIds():
            print("Error on line "+str(self.tokenizer.lineNum) +": Attempt to operate on undeclared variable '%s'"%self.id.literal)
            exit(0)
        token = self.tokenizer.getToken()
        if token == 13:
            self.tokenizer.skipToken()
            self.idList = IdList(self.tokenizer)
            self.idList.parseIdList()
        elif token == 12:
            return
        else:
            print("Error: parser expected ', or ; (13,12)', was '%s'",token)
            exit(0)
        return

    def printIdList(self):
        self.id.printId()
        if self.idList != None:
            sys.stdout.write(", ")
            self.idList.printIdList()
        return

    def execIdList(self,args,argCounter):
        if argCounter < len(args):
            self.id.updateId(args[argCounter])
            argCounter+=1
        else:
            print("Error: Attempting to read in more values than are present in input data")
            exit(0)
        if self.idList != None:
            argCounter = self.idList.execIdList()
        return argCounter
    
    def writeIdList(self):
        sys.stdout.write(str(self.id.literal)+" = "+str(int(self.id.getVal())))
        if self.idList != None:
            sys.stdout.write("\n")
            self.idList.writeIdList()
        else:
            sys.stdout.write("\n")