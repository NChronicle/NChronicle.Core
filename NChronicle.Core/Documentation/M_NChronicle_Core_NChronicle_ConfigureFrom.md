# NChronicle.ConfigureFrom Method 
 

Configure all new and existing <a href="T_NChronicle_Core_Model_Chronicle.md">Chronicle</a> instances with options and libraries from an XML file.

**Namespace:**&nbsp;<a href="N_NChronicle_Core.md">NChronicle.Core</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static void ConfigureFrom(
	string path,
	bool watch = true,
	int watchBufferTime = 1000
)
```

**VB**<br />
``` VB
Public Shared Sub ConfigureFrom ( 
	path As String,
	Optional watch As Boolean = true,
	Optional watchBufferTime As Integer = 1000
)
```

**F#**<br />
``` F#
static member ConfigureFrom : 
        path : string * 
        ?watch : bool * 
        ?watchBufferTime : int 
(* Defaults:
        let _watch = defaultArg watch true
        let _watchBufferTime = defaultArg watchBufferTime 1000
*)
-> unit 

```


#### Parameters
&nbsp;<dl><dt>path</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Path to the XML file.</dd><dt>watch (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Watch for changes to the file and reconfigure when it changes.</dd><dt>watchBufferTime (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />Time in milliseconds to wait after a change to the file until reconfiguring.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_Core_NChronicle.md">NChronicle Class</a><br /><a href="N_NChronicle_Core.md">NChronicle.Core Namespace</a><br />
