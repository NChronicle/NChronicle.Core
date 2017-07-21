# Chronicle.Success Method (String, Exception, String[])
 

Record a <a href="T_NChronicle_Core_Model_ChronicleLevel.md">Success</a> level message and <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a> with the specified *tags*.

**Namespace:**&nbsp;<a href="N_NChronicle_Core_Model.md">NChronicle.Core.Model</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public void Success(
	string message,
	Exception exception,
	params string[] tags
)
```

**VB**<br />
``` VB
Public Sub Success ( 
	message As String,
	exception As Exception,
	ParamArray tags As String()
)
```

**F#**<br />
``` F#
abstract Success : 
        message : string * 
        exception : Exception * 
        tags : string[] -> unit 
override Success : 
        message : string * 
        exception : Exception * 
        tags : string[] -> unit 
```


#### Parameters
&nbsp;<dl><dt>message</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Message to be recorded.</dd><dt>exception</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">System.Exception</a><br /><a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a> to be recorded.</dd><dt>tags</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Tags to be appended to this record.</dd></dl>

#### Implements
<a href="M_NChronicle_Core_Interfaces_IChronicle_Success_1.md">IChronicle.Success(String, Exception, String[])</a><br />

## See Also


#### Reference
<a href="T_NChronicle_Core_Model_Chronicle.md">Chronicle Class</a><br /><a href="Overload_NChronicle_Core_Model_Chronicle_Success.md">Success Overload</a><br /><a href="N_NChronicle_Core_Model.md">NChronicle.Core.Model Namespace</a><br />
