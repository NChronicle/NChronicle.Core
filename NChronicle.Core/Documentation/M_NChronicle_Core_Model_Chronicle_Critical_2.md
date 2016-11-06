# Chronicle.Critical Method (String, String[])
 

Record a <a href="T_NChronicle_Core_Model_ChronicleLevel.md">Critical</a> level message with specified tags.

**Namespace:**&nbsp;<a href="N_NChronicle_Core_Model.md">NChronicle.Core.Model</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void Critical(
	string message,
	params string[] tags
)
```

**VB**<br />
``` VB
Public Sub Critical ( 
	message As String,
	ParamArray tags As String()
)
```

**F#**<br />
``` F#
abstract Critical : 
        message : string * 
        tags : string[] -> unit 
override Critical : 
        message : string * 
        tags : string[] -> unit 
```


#### Parameters
&nbsp;<dl><dt>message</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Message to be recorded.</dd><dt>tags</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Tags to be appended to this record.</dd></dl>

#### Implements
<a href="M_NChronicle_Core_Interfaces_IChronicle_Critical_2.md">IChronicle.Critical(String, String[])</a><br />

## See Also


#### Reference
<a href="T_NChronicle_Core_Model_Chronicle.md">Chronicle Class</a><br /><a href="Overload_NChronicle_Core_Model_Chronicle_Critical.md">Critical Overload</a><br /><a href="N_NChronicle_Core_Model.md">NChronicle.Core.Model Namespace</a><br />
