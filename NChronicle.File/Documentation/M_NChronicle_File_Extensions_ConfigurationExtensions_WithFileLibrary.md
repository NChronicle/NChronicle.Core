# ConfigurationExtensions.WithFileLibrary Method 
 

Create and add a <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a> to the specified ChronicleConfiguration.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Extensions.md">NChronicle.File.Extensions</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public static FileChronicleLibrary WithFileLibrary(
	this ChronicleConfiguration config
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function WithFileLibrary ( 
	config As ChronicleConfiguration
) As FileChronicleLibrary
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member WithFileLibrary : 
        config : ChronicleConfiguration -> FileChronicleLibrary 

```

<br />

#### Parameters
&nbsp;<dl><dt>config</dt><dd>Type: NChronicle.Core.Model.ChronicleConfiguration<br />The ChronicleConfiguration for which to add the <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a>.</dd></dl>

#### Return Value
Type: <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a><br />The created <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a>.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type ChronicleConfiguration. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_NChronicle_File_Extensions_ConfigurationExtensions.md">ConfigurationExtensions Class</a><br /><a href="N_NChronicle_File_Extensions.md">NChronicle.File.Extensions Namespace</a><br />
