# FileComparer

Written in C#

Compares Folders and gives output for the same files in these Folders. At least one folder must be given.

Usage: ````FileComparer.exe [Options] C:\Folder1````

Options:
```
 -p, --pattern=pattern      the pattern which should be searched. multiple patterns: p=*.cs|*.md
 -h, --help                 show this message and exit
 -a, --algorithm=algorithm  MD5 is default. All Algorithm from https://msdn.microsoft.com/en-us/library/wet69s13(v=vs.110).aspx are supported
 ```