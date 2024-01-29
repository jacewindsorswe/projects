import sys
class CompOp:
    compOpNums = {
        25:"!=",
        26:"==",
        27:"<",
        28:">",
        29:"<=",
        30:">="}
    def __init__(self,tokenizer):
        self.compOpNum = None
        self.tokenizer = tokenizer

    def parseCompOp(self):
        token = self.tokenizer.getToken()
        if token not in {25,26,27,28,29,30}:
            print("Error: parseCompOp expected a CompOp (25-30), was %d"%token)
            exit(0)
        self.compOpNum = token
        self.tokenizer.skipToken()
        return
    
    def printCompOp(self):
        sys.stdout.write(CompOp.compOpNums[self.compOpNum])