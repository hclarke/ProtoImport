# ProtoImport

ProtoImport lets you use Protocol Buffers in your unity projects. Just drop a .proto file into your project, and you're ready to go!

## Modern Serialization

ProtoImport uses the proto3 version of Protocol Buffers, which is designed to produce small messages on the wire, serialize and deserialize quickly, allow changes to your message structure without breaking compatibility, and make it convenient to send messages between different platforms and languages.

## Unity Integration

ProtoImport hooks into Unity's asset importer, so all you have to do is drop a .proto file into your Assets folder, and it will generate a C# file to go with it. If you move, rename, delete, or change the .proto file, ProtoImport will update the C# file too.

We've ported the Protobuf runtime library to Unity's version of Mono, so everything works seamlessly!