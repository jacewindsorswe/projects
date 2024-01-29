import sys
class Int:
    def __init__(self,tokenizer):
        self.literal = None
        self.tokenizer = tokenizer
    
    def parseInt(self):
        token = self.tokenizer.getToken()
        if token != 31:
            print("Error: parser expected int (31), was '%d'"%token)
            exit(0)
        self.literal = self.tokenizer.intVal()
        self.tokenizer.skipToken()
        return
    
    def printInt(self):
        sys.stdout.write(str(self.literal))
        return

    

