# Learning Solidity Essentials
Took below course and summarized essentials. 

- [HashLips : Learn Solidity basics](https://www.youtube.com/watch?v=sngKPYfUgkc&list=PLvfQp12V0hS2PQd9-X-E2AjmXj1o05WOo&index=1&t=3178s)

## Solidity
> Solidity is an object-oriented, high-level language for implementing smart contracts. Smart contracts are programs which govern the behaviour of accounts within the Ethereum state. Solidity is a curly-bracket language. It is influenced by C++, Python and JavaScript, and is designed to target the Ethereum Virtual Machine (EVM).

> With Solidity you can create contracts for uses such as voting, crowdfunding, blind auctions, and multi-signature wallets.

> When deploying contracts, **you should use the latest released version of Solidity**. Apart from exceptional cases, only the latest version receives security fixes. Furthermore, breaking changes as well as new features are introduced regularly. We currently use a 0.y.z version number to indicate this fast pace of change.

### Ethereum virtual machine
> The Ethereum Virtual Machine or EVM is the runtime environment for smart contracts in Ethereum. It is not only sandboxed but actually completely isolated, which means that code running inside the EVM has no access to network, filesystem or other processes. Smart contracts even have limited access to other smart contracts.

### Convention
Naming in Solidity code is as follows : 

- file name is in Camel Case, starting with upper case.
<img src="../reference/solidity-file-convention.png" width=218 height=66 alt="Solidity file name" />

### Pragma
> A pragma is a compiler directive that allows you to provide additional information to the compiler. This information can change compilation details that are not otherwise under your control. For example, the pack pragma affects the layout of data within a structure. Compiler pragmas are also called directives.

### NatSpec Format
> Solidity contracts can use a special form of comments to provide rich documentation for functions, return variables and more. This special form is named the Ethereum Natural Language Specification Format (NatSpec). ... It is recommended that Solidity contracts are fully annotated using NatSpec for all public interfaces (everything in the ABI).

<img src="../reference/natspec-snippets.png" width=800 height=227 alt="NatSpec snippets" />

## Remix
Remix is a online editor to compile Solidity. You can either go below website, 

- [Remix IDE](https://remix.ethereum.org/)

or install extension in VS code. 

<img src="../reference/remix-vscode.png" width=251 height=394 alt="remix extension in vs code" />

> This project brings Remix plugins to Visual Studio Code. Remix plugins can perform a variety of tasks such as verifying contracts, linting, generating documentation, running tutorials, compiling, debugging and much more. The Remix Plugin API allows plugins that run in Remix to run in Visual Studio Code too. It allows developers to access and interact with the file system, components, extensions and other Remix plugins without actually having to create a different code base.


## Reference
- [Solidity official](https://docs.soliditylang.org/en/v0.8.11/)
- [VS Code marketplace : Ethereum Remix](https://marketplace.visualstudio.com/items?itemName=RemixProject.ethereum-remix)
- [Oracle : Pragmas](https://docs.oracle.com/cd/E19957-01/806-3571/Pragmas.html#:~:text=A%20pragma%20is%20a%20compiler,pragmas%20are%20also%20called%20directives.)