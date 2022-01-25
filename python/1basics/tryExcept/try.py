# Error handling in Python: try, except statement
try : 
    x = int(input('Enter your dividing number: '))
    y = 10 / x
    print(y)
except Exception as e:
    print("error has occured: ", e)
finally : 
    print("error handling done")