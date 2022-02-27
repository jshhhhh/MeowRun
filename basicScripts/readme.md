# Learning Unity Essentials
Started learning Unity to create websites like below(web + metaverse)

- [Cybertruck](https://bruno-simon.com/#cybertruck)
- [Kode Sports Club](https://www.kodeclubs.com/)

Install Unity at offical home page with license. License will be issued based on your account and license choice. Adjust Unity editor installation folder and general tab as well. Choose Unity version and add/install. 

- Install below VS code extensions for easier Unity development.
1. MonoBehaviour Snippets
2. Unity Code Snippets

## Understanding how project is structured
There are many details to create one game project.

- levels : map of the game => adjust file orders in build settings to match levels
- scene : .unity files
- prefabs : user-defined Unity object template. maintained to improve reusability
- Canvas : adding UI screens to game => start/end
- scripts : controls game object logics
- assets : sounds, fonts, materials .. 
- camera and light

- start screen ===(level 1)====>(...)====>(last level)====> end screen

## Unity Hub
You can add modules for more supports such as Web GL and Linux build in Unity hub. Install Webl GL to publish your game. 

<img src="reference/webgl-module.png" width=273 height=329 alt="unity build setting" />

## Beginner-friendly scripting tutorials
C# script in Unity is considered a behavior component. For example, you can change object's color with the script. 

```c# 
public class EaxmpleBehaviour : MonoBehaviour {
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            GetComponent<Renderer>().meterial.color = Color.red
        }
    }
}
```

## C# script
> For anyone just starting out with Unity, or anyone with previous knowledge of object-oriented programming, C# is the best Unity programming language to begin with. In fact, C# is the only Unity coding language worth learning for the platform, and with good reason.

> Unity uses Mono, which is a cross-platform implementation of Microsoft's .NET framework. C# is the primary language of .NET, and all of Unity's libraries are built using C# code.

> Unity has made it clear that it considers C# to be the only truly canon language for Unity development.

> C++ is the most common Unity development language used for plugin creation. People use plugins for a number of reasons, including speed and access to a codebase that's already written in another language. Building these scripts into dynamic link library (DLL) plugins saves you the trouble of rewriting code, and can even improve performance in some cases.

> Rust is a language with a lot of buzz around it. It was created by Mozilla in 2009, as a way for developers to develop high-performance software quickly. Experienced programmers love it for the incredible amount of control it gives, all while eliminating the pitfalls of languages like C++, which can feel less friendly at times. While it isn't possible to write Rust in Unity directly, you can access functions and methods written in Rust from your Unity code.

## Scripting
> Scripting is an essential ingredient in all applications you make in Unity. Most applications need scripts to respond to input from the player and to arrange for events in the gameplay to happen when they should. Beyond that, scripts can be used to create graphical effects, control the physical behaviour of objects or even implement a custom AI system for characters in the game.

### Unit testing
> As your project grows, and the number of scripts
, classes and methods in your project increases, it can become difficult to ensure that a change in one part of your code doesn’t break things somewhere else.

> Automated testing helps you check that all parts of your code are functioning as expected. It saves time by identifying where and when problems occur as soon as they are introduced during development, rather than relying on manual testing, or even worse - bug reports from your end users.

> The Unity Test Framework package (formerly the “Unity Test Runner”) is a tool that allows you to test your code in both Edit mode and Play mode, and also on target platforms such as Standalone, Android, or iOS.

### Creating and Using Scripts
> The behavior of GameObjects is controlled by the Components that are attached to them. Although Unity’s built-in Components can be very versatile, you will soon find you need to go beyond what they can provide to implement your own gameplay features. Unity allows you to create your own Components using scripts. These allow you to trigger game events, modify Component properties over time and respond to user input in any way you like.

> Unity supports the C# programming language natively. C# (pronounced C-sharp) is an industry-standard language similar to Java or C++.

> When you double-click a script Asset in Unity, it will be opened in a text editor. By default, Unity will use Visual Studio, but you can select any editor you like from the External Tools panel in Unity’s preferences (go to Unity > Preferences).


## Learning by doing
Summarized what I learned in a blockquote style. 

> The MonoBehaviour is a pre-made class by Unity. 1. file name and class name should be the same to apply C# script to your Unity game object 2. should inherit MonoBehaviour

```c#
// importing name space
using System.Collections;
using System.Collections.Generic;
using UnityEngine; // getting all the name spaces in Unity engine.
public class firstScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { Debug.Log("Started"); }
    // Update is called once per frame
    void Update() { }
}
```

> Awake : executed on a current scene once when the object is active. used to **initialize game object**.

```c# 
    private void Awake() { Debug.Log("Awake executed"); }
```

> OnEnable : execute whenever a **component gets active**.

```c# 
    private void OnEnable() { Debug.Log("OnEnable executed"); }
```

> Start function gets called **before the first Update function**. used to initialize data. Awake => OnEnable => Start

```c#
private void Start() { Debug.Log("Start function exectued"); }
```

> Update funciton gets called per frame when component is active. If FPS is 60, it means the funciton executes 60 times per second.

```c#
private void Update() { Debug.Log("Update funciton executed"); }
```

> OnDestroy executes when game object gets destroyed, scene changes, and game finishes.

```c#
private void OnDestroy() { Debug.Log("OnDestroy func executed"); }
```

> OnApplicationQuit exeuctes when game finishes. 

```c#    
private void OnApplicationQuit() { Debug.Log("OnApplicationQuit func executed"); }
```

## Vector math in Unity
Vector is a line drawing between two points. The line is called magnitude. 

<img src="reference/2d-vectors.png" width=632 height=376 alt="2d vector in Unity" />

The magnitude is calcualted with square root of (x,y) coordinates, squared.

<img src="reference/2d-magnitude.png" width=679 height=394 alt="2d vector magnitude" />

The magnitude can also be calculated in 3d with the same logic and equation, adding one more axis(z).

<img src="reference/3d-magnitude.png" width=728 height=393 alt="3d vector magnitude" />

Vectors can be combined such as : 1) dot product 2) cross product and Unity supports methods to caculate them easily.

## Reference
- [Unity Introduction(KOR)](https://youtube.com/playlist?list=PLC2Tit6NyVida7Jh6gSlw1BicuEUCFV6V)
- [Unity Beginner Tutorials(ENG)](https://youtube.com/playlist?list=PLPV2KyIb3jR5QFsefuO2RlAgWEz6EvVi6)
- [Unity official docs](https://docs.unity3d.com/Manual/ScriptingSection.html)
- [Beginner-friendly scripting tutorials](https://www.youtube.com/watch?v=Z0Z7xc18CcA&list=PLX2vGYjWbI0S9-X2Q021GUtolTqbUBB9B&index=1&t=14s)
- [The 5 Unity Game Development Languages: Which Should You Learn?](https://www.makeuseof.com/tag/unity-game-development-languages/)