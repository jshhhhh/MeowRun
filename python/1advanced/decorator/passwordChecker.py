# Decorator exercise: decorator with arguments.
# Create a decorator
def pwChecker(first_digit):
    def outer(myFunc):
        def inner(*args, **kwargs):
            print("... Checking your password ...")
            if args[0] != first_digit or len(args) > 4 :
                raise ValueError(f"Password should be: {first_digit}xxx, four singledigits.")
            return myFunc(*args, **kwargs)
        return inner
    return outer


@pwChecker(4) # @pwCheck(first_digit)
# Create a function that checks 4 single digits as password.
def pwConfirmed(*args):
    return "Password confirmed", args

# Check @pwChecker works: 4 single digits + first digit fixed.
pw = pwConfirmed(5,8,8,8)
print(pw)