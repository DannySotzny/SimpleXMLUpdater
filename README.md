# XML Update Tool

Update a value in a XML-Document by XPath
There are two operating modes:

First you´ll send the XML Doc via Standard Input   e.g.  updateXML <parameter> < inputFile.xml
or second you give the filename as first parameter.

in first case you could start with:

    dotnet run /package/metadata/version 2.0.0 < myPackage.nuspec

to update VersionInfo in a NuGet Spec file.
in second case you would call:

    dotnet run myPackage.nuspec /package/metadata/version 2.0.0

The Result is in both cases the output of the manipuled File

## Example:

Input XML:
```
<TestXML>
  <myValue>Hello</myValue>
</TestXML>
```
Command:

    dotnet run /TestXML/myValue World < example.xml

Output:
```
<TestXML>
  <myValue>World</myValue>
</TestXML>
```