# ConfigurationExtensions.WithConsoleLibrary Method 
 

Create and add a <a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary</a> to the specified ChronicleConfiguration.

**Namespace:**&nbsp;<a href="N_NChronicle_Console_Extensions.md">NChronicle.Console.Extensions</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.2.0 (1.0.2.0)

## Syntax

**C#**<br />
``` C#
public static ConsoleChronicleLibrary WithConsoleLibrary(
	this ChronicleConfiguration config
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function WithConsoleLibrary ( 
	config As ChronicleConfiguration
) As ConsoleChronicleLibrary
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member WithConsoleLibrary : 
        config : ChronicleConfiguration -> ConsoleChronicleLibrary 

```


#### Parameters
&nbsp;<dl><dt>config</dt><dd>Type: NChronicle.Core.Model.ChronicleConfiguration<br />The ChronicleConfiguration for which to add the <a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary</a>.</dd></dl>

#### Return Value
Type: <a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary</a><br />The created <a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary</a>.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type ChronicleConfiguration. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_NChronicle_Console_Extensions_ConfigurationExtensions.md">ConfigurationExtensions Class</a><br /><a href="N_NChronicle_Console_Extensions.md">NChronicle.Console.Extensions Namespace</a><br />
