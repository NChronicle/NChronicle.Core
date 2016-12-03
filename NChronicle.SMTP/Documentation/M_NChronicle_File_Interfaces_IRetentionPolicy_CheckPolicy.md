# IRetentionPolicy.CheckPolicy Method 
 

Check whether the output file at the given *path* should have the file retention policy invoked upon it.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Interfaces.md">NChronicle.File.Interfaces</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
bool CheckPolicy(
	string path,
	byte[] pendingBytes
)
```

**VB**<br />
``` VB
Function CheckPolicy ( 
	path As String,
	pendingBytes As Byte()
) As Boolean
```

**F#**<br />
``` F#
abstract CheckPolicy : 
        path : string * 
        pendingBytes : byte[] -> bool 

```

<br />

#### Parameters
&nbsp;<dl><dt>path</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The path to the output file.</dd><dt>pendingBytes</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/yyb1w04y" target="_blank">System.Byte</a>[]<br />Any pending bytes that are to be written to the output file.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a><br />A <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a> indicating if the policy should be invoked.

## See Also


#### Reference
<a href="T_NChronicle_File_Interfaces_IRetentionPolicy.md">IRetentionPolicy Interface</a><br /><a href="N_NChronicle_File_Interfaces.md">NChronicle.File.Interfaces Namespace</a><br />
