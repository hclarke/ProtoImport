# ProtoImport

Lets you easily use .proto files in your unity projects

## Installation

Clone/copy this repository into your Unity project's Assets folder

~~~~
git clone [this repo] Assets/ProtoImport
~~~~

You can also incluse it as a git submodule:
~~~~
git submodule add [this repo] Assets/ProtoImport
~~~~

## Usage

Drop a .proto file into your project, and a .proto.cs file will appear next to it. If you move or delete the proto file, the generated file will also be moved or deleted

If you delete or lose the generated file, reimport the .proto file to regenerate it
