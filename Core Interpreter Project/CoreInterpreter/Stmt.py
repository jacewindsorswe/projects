import If
import Assign
import While
import Read
import Write
class Stmt:

    #The stmt class doesn't require a seperate declaration for each object
    #but I covered the reasoning I didn't implement it polymorphically at the bottom
    #of this file. At the time of writing the interpreter, we had yet to cover that topic
    #and this was the way I wrote it to help keep it organized in my own head.
    def __init__(self,tokenizer):
        #"Alt" appended to python reserved words
        self.assign = None
        self.ifAlt = None
        self.whileAlt = None
        self.read = None
        self.write = None
        
        self.tokenizer = tokenizer

    def parseStmt(self):
        token = self.tokenizer.getToken()
        match(token):
            case 5:
                self.ifAlt = If.If(self.tokenizer)
                self.ifAlt.parseIf()
            case 8:
                self.whileAlt = While.While(self.tokenizer)
                self.whileAlt.parseWhile()
            case 10:
                self.read = Read.Read(self.tokenizer)
                self.read.parseRead()
            case 11:
                self.write = Write.Write(self.tokenizer)
                self.write.parseWrite()
            case 32:
                self.assign = Assign.Assign(self.tokenizer)
                self.assign.parseAssign()
            case default:
                print("Error: parser expected a stmt alternative (5,8,10,11,32), was %d"%token)
                exit(0)

        return
    
    def printStmt(self,offset):
        if self.assign!=None:
            self.assign.printAssign(offset)
        elif self.ifAlt!=None:
            self.ifAlt.printIf(offset)
        elif self.whileAlt!=None:
            self.whileAlt.printWhile(offset)
        elif self.read != None:
            self.read.printRead(offset)
        elif self.write != None:
            self.write.printWrite(offset)
        return

    def execStmt(self):
        if self.assign!=None:
            self.assign.execAssign()
        elif self.ifAlt!=None:
            self.ifAlt.execIf()
        elif self.whileAlt!=None:
            self.whileAlt.execWhile()
        elif self.read != None:
            self.read.execRead()
        elif self.write != None:
            self.write.execWrite()
        return
    
    #I wrote this prior to the lecture covering
    #polymorphism and unfortunately lacked the time in my schedule to do a refactor.
    #The 5 statement classes would be updated to implement the abstract Stmt class
    #and have the corresponding method names Parse(), Print(), and Exec().
    #ParseSS would call obj.Parse() after creating an object obj of appropriate 
    #type as its member variable. Similarly, PrintSS and ExecSS would just
    #call obj.Print() and obj.Exec(). 