class MyCalc : 
    def __init__(self, name) -> None:
        self.name = name
        
    # overriding magic method __add__
    def __add__(self, *args) : 
        print("__add__ invoked")
        print(args)
        sum = 0
        for element in args : 
            sum += element
        print(sum)
        
doCalc = MyCalc("Jake")
doCalc.__add__(2,3,4)