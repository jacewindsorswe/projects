import Assign
import sys
class Id:
    #currentIDs stores the declared and instantiated variables in a size 3 tuple.
    currentIDs = []

    def __init__(self,tokenizer):
        self.literal = None
        self.tokenizer = tokenizer
    
    def parseId(self):
        token = self.tokenizer.getToken()
        if token != 32:
            print("Error: parser expected ID (32), was '%d'"%token)
            exit(0)
        self.literal = self.tokenizer.idName()
        if(not Assign.Assign.declOver and self.literal in Id.returnDeclIds()):
            print("Error on line " +str(self.tokenizer.lineNum)+": Double declaration of variable '%s'"%self.literal)
            exit(0)
        elif not Assign.Assign.declOver:
            Id.currentIDs.append((self.literal,0,None))
        self.tokenizer.skipToken()
        return
    
    def printId(self):
        sys.stdout.write(self.literal)
        return
    
    #trip denotes a tuple of size 3, or a "triple"
    def updateId(self, newVal):
        if newVal != None:
            for trip in Id.currentIDs:
                if trip[0] == (str(self.literal)):
                    name = trip[0]
                    Id.currentIDs.remove(trip)
                    Id.currentIDs.append((name,1,newVal))
        else:
            print("Error: Attempted to update/access the value of "+str(self.literal)+"before it was instantiated")
            exit(0)
    #trip denotes a tuple of size 3, or a "triple"
    def isInstantiated(self):
        for trip in Id.currentIDs:
            if trip[0] == (str(self.literal)):
                if trip[1] == 1:
                    return True
        return False
    #trip denotes a tuple of size 3, or a "triple"
    def getVal(self):
        for trip in Id.currentIDs:
            if trip[0] == (self.literal):
                if trip[1] == 1:
                    return int(trip[2])
        print("Error: Could not retrieve value for "+str(self.literal)+". Is it initialized?")

    #trip denotes a tuple of size 3, or a "triple"
    def returnDeclIds():
        retList = []
        for trip in Id.currentIDs:
            retList.append(str(trip[0]))
        return retList
