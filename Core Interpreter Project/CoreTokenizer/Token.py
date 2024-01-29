import enum

class TokenType(enum.Enum):
    ReservedWord = 1
    UInt = 2
    SpecialSymbol = 3
    ID = 4
    Empty = 5

class Token:
    def __init__(self):
        self.str = ""
        self.id = -1
        self.TokenType = TokenType.Empty
    
    def displayLiteral(self):
        print(self.str)

    def returnLiteral(self):
        return self.str
    
    def displayID(self):
        print(self.id)
    
    def returnID(self):
        return(self.id)
    
    def getType(self):
        return(self.TokenType)