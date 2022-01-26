from typing_extensions import Self

class MagicMethod :
    def __new__(cls:type[Self]) :
        print("__new__ magic method invoked")
        instance = object.__new__(cls)
        return instance
    def __init__(self) -> None:
        self.name = "Jake"
    def getName(self) : 
        return self.name
    def setName(self, newName) : 
        self.name = newName

# Create an class instance
myMagic = MagicMethod()

# Check attribute
print(myMagic.name)

# Setter
myMagic.setName("Not Jake")

# Getter
print(myMagic.getName())