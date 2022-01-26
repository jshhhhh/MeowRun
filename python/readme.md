# Learning Python Essentials
Learn Python. Life is short, so is Python. 

## Basics
- namespace, 
- iterator, 
- property, 
- wrap, 
- staticmethod, classmethod
- variable argument, keyword argument 
- decorator, 

### Magic method
> Magic methods in Python are the special methods that **start and end with the double underscores**.
> Magic methods are not meant to be invoked directly by you, but **the invocation happens internally from the class on a certain action**. For example, when you add two numbers using the + operator, internally, the __add__() method will be called.

> __new__() method
Languages such as Java and C# use the new operator to create a new instance of a class. In Python the __new__() magic method is implicitly called before the __init__() method. The __new__() method returns a new object, which is then initialized by __init__().

> Magic methods are most frequently **used to define overloaded behaviours of predefined operators** in Python. For instance, arithmetic operators by default operate upon numeric operands. This means that numeric objects must be used along with operators like +, -, *, /, etc. The + operator is also defined as a concatenation operator in string, list and tuple classes. We can say that the + operator is overloaded.

### Paradiams
#### Object oriented 
- Inheritance, 
- Descriptor

#### Function
- factorial 
- recursion
- lambda
- reduce

## Reference
- [Python - Magic or Dunder Methods](https://www.tutorialsteacher.com/python/magic-methods-in-python)