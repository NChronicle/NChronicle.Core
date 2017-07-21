# FileChronicleLibraryConfiguration.ListeningTo Method (String[])
 

Listen to records with at least one of the specified *tags*.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public void ListeningTo(
	params string[] tags
)
```

**VB**<br />
``` VB
Public Sub ListeningTo ( 
	ParamArray tags As String()
)
```

**F#**<br />
``` F#
member ListeningTo : 
        tags : string[] -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>tags</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Tags to listen to records with.</dd></dl>

## Remarks
This can be invoked multiple times with further *tags* to listen to, therefore invoking ListeningTo(String[]) once with 3 tags and invoking ListeningTo(String[]) 3 times with each of the same tags is semantically synonymous.

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_FileChronicleLibraryConfiguration.md">FileChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningTo.md">ListeningTo Overload</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br /><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_Ignoring_1.md">FileChronicleLibraryConfiguration.Ignoring(String[])</a><br /><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningToAllTags.md">FileChronicleLibraryConfiguration.ListeningToAllTags()</a><br />
