# Json Formatter Example

## Readme
This is an application where you can start learning how to write unit tests. Contained in this repo is a console app, in varying stages, that allow you to write code and unit tests using NUnit and Typemock Isolator (non-free version so you'll need a valid trial or paid license). The goal would be for a developer to clone this repo and implement the tests in the `stubs` branch using the reference branch `master` for questions or as a cheatsheet/guide.

### Branch Definitions
* master: The reference implementation using NUnit and Typemock Isolator. Contains the complete functional application and tests. Code coverage at 96% on the Application project.
* stubs: master minus code to create stubs on the functional and test methods. The goal of this is to code the implementation and tests at the same time!
   * Application
      * DefaultProgramActions.cs: Implement the methods in the file as there are comments on each method to guide the user in the implementation. Tests are in Application.Tests->DefaultProgramActionsTests.cs.
      * ProgramInitializer.cs: look for the lines `// your code here` in conjunction with the method comments to implement the method. Tests are in Application.Tests->ProgramInitializerTests.cs.
   * Application.Tests
      * All tests need implemented. The only file to skip is Any.cs as it's a helper class for writing tests.

### 

## Functional Application

### Objective
Create a console application (Application) that can read the contents of a text file, format the text as single-line JSON or indented JSON and write the text back to the same file.

### Requirements
1. Application accepts a single named command called “format” and a set of parameters that format needs to complete the work.
   * A default/non-named parameter that is the full or relative path to the file to load and save.
   * --none indicates to apply no formatting and save the data as single-line JSON.
   * --formatted indicates to apply formatting and save the data as indented JSON.
   * --help (only option provided) displays the Help contents.
   * the default is to save as indented json if neither --none or --formatted is provided
1. Must use the Newtonsoft.Json Nuget package for JSON manipulation.
1. Application responds with appropriate error if malformed input data is entered.
1. Application handles error scenarios gracefully. 
   * Examples being user does not have access to file, file is read only, file does not exist, data is not JSON, etc. This is not an exhaustive list but a subset of problems that might occur.
1. Calling only Application responds with help/usage text that detail how to use Application.

* Example commands (valid)
   * Application.exe
   * Application.exe format myfile.json --none
   * Application.exe format myfile.json --formatted
   * Application.exe format C:\JsonFiles\file.json --formatted
   * Application.exe --help
   * Application.exe format --help

* Example commands (invalid)
   * Application.exe format
   * Application.exe myfile.json
   * Application.exe format myfile.json --indented
   * Application.exe --none
   * Application.exe format --help --input
