# ConsoleChronicleLibraryConfiguration.Ignoring Method (String[])
 

Ignore records with at least one of the specified *tags*.

**Namespace:**&nbsp;<a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.2.0 (1.0.2.0)

## Syntax

**C#**<br />
``` C#
public void Ignoring(
	params string[] tags
)
```

**VB**<br />
``` VB
Public Sub Ignoring ( 
	ParamArray tags As String()
)
```

**F#**<br />
``` F#
member Ignoring : 
        tags : string[] -> unit 

```


#### Parameters
&nbsp;<dl><dt>tags</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Tags to ignore records with.</dd></dl>

## Remarks
This can be invoked multiple times with further *tags* to ignore, therefore invoking Ignoring(String[]) once with 3 tags and invoking Ignoring(String[]) 3 times with each of the same tags is semantically synonymous.

## See Also


#### Reference
<a href="T_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration.md">ConsoleChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_Ignoring.md">Ignoring Overload</a><br /><a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration Namespace</a><br /><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningTo_1.md">ConsoleChronicleLibraryConfiguration.ListeningTo(String[])</a><br /><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningToAllTags.md">ConsoleChronicleLibraryConfiguration.ListeningToAllTags()</a><br />
