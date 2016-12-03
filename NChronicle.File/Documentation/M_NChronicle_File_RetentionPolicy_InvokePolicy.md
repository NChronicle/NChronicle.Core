# RetentionPolicy.InvokePolicy Method 
 

Archive the output file at the given *path*, naming with time the output file was created.

**Namespace:**&nbsp;<a href="N_NChronicle_File.md">NChronicle.File</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public void InvokePolicy(
	string path
)
```

**VB**<br />
``` VB
Public Sub InvokePolicy ( 
	path As String
)
```

**F#**<br />
``` F#
abstract InvokePolicy : 
        path : string -> unit 
override InvokePolicy : 
        path : string -> unit 
```

<br />

#### Parameters
&nbsp;<dl><dt>path</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The path to the output file.</dd></dl>

#### Implements
<a href="M_NChronicle_File_Interfaces_IRetentionPolicy_InvokePolicy.md">IRetentionPolicy.InvokePolicy(String)</a><br />

## See Also


#### Reference
<a href="T_NChronicle_File_RetentionPolicy.md">RetentionPolicy Class</a><br /><a href="N_NChronicle_File.md">NChronicle.File Namespace</a><br />
