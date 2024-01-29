import Comp
import sys
class Cond:

    def __init__(self,tokenizer):
        self.comp = None
        self.cond1 = None
        self.cond2 = None
        self.andOr = None #andOr determines how to evaluate in exec
        self.tokenizer = tokenizer
    
    def parseCond(self):
        token = self.tokenizer.getToken()
        match(token):
            case(20):
                self.comp = Comp.Comp(self.tokenizer)
                self.comp.parseComp()
            case(15):
                self.tokenizer.skipToken() #skips !
                self.cond1 = Cond(self.tokenizer)
                self.cond1.parseCond
            case(16):
                self.tokenizer.skipToken() #skips [
                self.cond1 = Cond(self.tokenizer)
                self.cond1.parseCond()
                token = self.tokenizer.getToken()
                if token not in {18,19}:
                    print("Error: parseCond expected &&/|| (18,19),was %d"%token)
                    exit(0)
                if token == 18:
                    self.andOr = 0
                else:
                    self.andOr = 1
                self.tokenizer.skipToken()
                self.cond2 = Cond(self.tokenizer)
                self.cond2.parseCond()
                token = self.tokenizer.getToken()
                if token != 17:
                    print("Error: parseCond expected ']:17',was %d"%token)
                    exit(0)
                self.tokenizer.skipToken()
            case default:
                print("Error: parseCond expected '(,!,[:20,15,16', was %d"%token)
                exit(0)
        return
    
    def printCond(self):
        if self.cond1 == None and self.cond2 == None:
            self.comp.printComp()
        elif self.cond1 != None and self.cond2 == None:
            sys.stdout.write("!")
            self.cond1.printCond()
        else:
            sys.stdout.write("[ ")
            self.cond1.printCond()
            if self.andOr == 0:
                sys.stdout.write(" && ")
            else:
                sys.stdout.write(" || ")
            self.cond2.printCond()
            sys.stdout.write(" ]")
        return

    def evalCond(self):
        if self.cond1 == None:
            return self.comp.evalComp()
        else:
            if self.cond2 == None:
                return not (self.cond1.evalCond())
            else:
                if self.andOr == 0:
                    return (self.cond1.evalCond() and self.cond2.evalCond())
                else:
                    return (self.cond1.evalCond() or self.cond2.evalCond())


