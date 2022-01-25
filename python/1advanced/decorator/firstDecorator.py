# Create a closure to make a function decorator. 
def myDecorator(myFunc): 
    def msgPrint(): 
        print("... myDecorator is working ...")
    
    def whoMadeThis(): 
        who = "Jonghyun Sung wrote this code at: "
        when = 20210710
        print(who, when)

    def inner(*args, **kwargs): 
        msgPrint()
        whoMadeThis()
        return myFunc(*args, **kwargs)
    return inner

# Implement the decorator. 
@myDecorator
def myFunc(*args, **kwargs): 
    print(list(args) + list(kwargs.keys()) + list(kwargs.values()))

# Create a "hello decorator" phrase using parameters. 
myFunc("hello", decorator=True)

print(list("hello"))