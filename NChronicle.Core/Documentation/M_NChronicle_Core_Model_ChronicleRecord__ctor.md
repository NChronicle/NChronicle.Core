# ChronicleRecord Constructor 
 

Create a new Chronicle Record with the specified *level*, *message* (optional), *exception* (optional), and *tags* (optional).

**Namespace:**&nbsp;<a href="N_NChronicle_Core_Model.md">NChronicle.Core.Model</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public ChronicleRecord(
	ChronicleLevel level,
	string message = null,
	Exception exception = null,
	params string[] tags
)
```

**VB**<br />
``` VB
Public Sub New ( 
	level As ChronicleLevel,
	Optional message As String = Nothing,
	Optional exception As Exception = Nothing,
	ParamArray tags As String()
)
```

**F#**<br />
``` F#
new : 
        level : ChronicleLevel * 
        ?message : string * 
        ?exception : Exception * 
        tags : string[] 
(* Defaults:
        let _message = defaultArg message null
        let _exception = defaultArg exception null
*)
-> ChronicleRecord
```


#### Parameters
&nbsp;<dl><dt>level</dt><dd>Type: <a href="T_NChronicle_Core_Model_ChronicleLevel.md">NChronicle.Core.Model.ChronicleLevel</a><br />Level/severity of this record.</dd><dt>message (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Developer message for this record. Optional.</dd><dt>exception (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">System.Exception</a><br />Related <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a> for this record. Optional.</dd><dt>tags</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Tags to append to this record. Optional.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_Core_Model_ChronicleRecord.md">ChronicleRecord Class</a><br /><a href="N_NChronicle_Core_Model.md">NChronicle.Core.Model Namespace</a><br />
