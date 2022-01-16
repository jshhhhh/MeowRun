package main

import "fmt"

func main() {
	myX := 5

	// Function ChangeX takes myX's memory address as argument
	ChangeX(&myX)
	fmt.Println("The value of X is: ", myX)

	// Unlike pointer in C, pointer in Go does have some more constraints

	a := int(5) // type assertion
	p := &a
	*p = 555
	// p++ : No mathematic operations allowed on pointer.
	fmt.Print("updated value of a is : ", a)

	floatA := 5.0
	floatP := &floatA
	// floatP = &a : Cannot convert between different types of pointers
	fmt.Print(floatP)

	// fmt.Print(p == floatP) : Cannot compare between different types of pointers

		// Declare pointer
		var apple int
		var banana *int
	
		apple = 10
		banana = &a
	
		fmt.Println(apple, banana)
	
		*banana = 20 // update a with pointer banana
		fmt.Println("changed value of a is : ", a)
	
		// Pointer cannot store value itself. Look at d = 10.
		var c int
		var d *int
		// a = 10
		d = &c
		*d = 100
		fmt.Print("c equals : ", c)
	
		var myNum int = 1
		var numPtr *int
		numPtr = &myNum
		// fmt.Println(&myNum, numPtr)
		*numPtr = 100
		fmt.Print("updated myNum value is : ", myNum)

		// Initialize your pointer with new() function. new() allocates memory
		// and return pointer with nil value.
		var numPtr2 *string = new(string)
		fmt.Println(numPtr2)
}

// Referencing variable X to see a value
func ChangeX(X *int) {
	// Dereferencing X to get the value and update it.
	*X = 10
}
