# Hexadecimal ASCII to binary conversion library and tool [![Build status](https://ci.appveyor.com/api/projects/status/m9tluu3xsqwtjbu3?svg=true)](https://ci.appveyor.com/project/tewarid/net-hex-to-bin)

A simple library and tool to convert a file or data in hex format (e.g. DE AD or 0xDE 0xAD or DEAD) to binary.

The following are all valid hex values and produce the equivalent binary sequence. Whitespace is ignored, but the tool requires that you're consistent in your usage (or lack of usage) of the prefix `0x`.

```csharp
“0xDE 0xAD”
“0xDE0xAD”
“0xD E0xAD”
“0xD\r\nE0xAD”
“0xDE\r\n 0xAD”
“0xDE\n 0xAD”
“0xDE\r 0xAD”
“0xD\r\nE 0xAD”
“0x00DE 0xAD”
“DE AD”
“DEAD”
“DEA D”
“DEA\r\nD”
```

The following are invalid and will result in an error.

```csharp
“0x01DE 0xAD”
“0xDExAD”
“0xDE AD”
```
