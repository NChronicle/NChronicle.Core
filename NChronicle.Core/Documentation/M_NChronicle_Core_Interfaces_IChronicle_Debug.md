# IChronicle.Debug Method (Exception, String[])
 

Record a <a href="T_NChronicle_Core_Model_ChronicleLevel.md">Debug</a> level exception with specified tags.

**Namespace:**&nbsp;<a href="N_NChronicle_Core_Interfaces.md">NChronicle.Core.Interfaces</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
void Debug(
	Exception exception,
	params string[] tags
)
```

**VB**<br />
``` VB
Sub Debug ( 
	exception As Exception,
	ParamArray tags As String()
)
```

**F#**<br />
``` F#
abstract Debug : 
        exception : Exception * 
        tags : string[] -> unit 

```


#### Parameters
&nbsp;<dl><dt>exception</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">System.Exception</a><br /><a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a> to be recorded.</dd><dt>tags</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Tags to be appended to this record.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_Core_Interfaces_IChronicle.md">IChronicle Interface</a><br /><a href="Overload_NChronicle_Core_Interfaces_IChronicle_Debug.md">Debug Overload</a><br /><a href="N_NChronicle_Core_Interfaces.md">NChronicle.Core.Interfaces Namespace</a><br />
