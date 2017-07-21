# RetentionPolicy.CheckPolicy Method 
 

Check if the output file at the given *path* is still within the configured limits or is to be archived. It is ready if the file - with the pending bytes to be written - is over the configured file size limit or the file is older than the configured file age limit.

**Namespace:**&nbsp;<a href="N_NChronicle_File.md">NChronicle.File</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public bool CheckPolicy(
	string path,
	byte[] pendingBytes
)
```

**VB**<br />
``` VB
Public Function CheckPolicy ( 
	path As String,
	pendingBytes As Byte()
) As Boolean
```

**F#**<br />
``` F#
abstract CheckPolicy : 
        path : string * 
        pendingBytes : byte[] -> bool 
override CheckPolicy : 
        path : string * 
        pendingBytes : byte[] -> bool 
```

<br />

#### Parameters
&nbsp;<dl><dt>path</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The path to the output file.</dd><dt>pendingBytes</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/yyb1w04y" target="_blank">System.Byte</a>[]<br />Any pending bytes that are to be written to the output file.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a><br />A <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a> indicating if the output file is to be archived.

#### Implements
<a href="M_NChronicle_File_Interfaces_IRetentionPolicy_CheckPolicy.md">IRetentionPolicy.CheckPolicy(String, Byte[])</a><br />

## See Also


#### Reference
<a href="T_NChronicle_File_RetentionPolicy.md">RetentionPolicy Class</a><br /><a href="N_NChronicle_File.md">NChronicle.File Namespace</a><br />
