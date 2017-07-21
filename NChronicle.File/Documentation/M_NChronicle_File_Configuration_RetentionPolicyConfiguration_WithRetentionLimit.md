# RetentionPolicyConfiguration.WithRetentionLimit Method 
 

Set a retention *limit*, defining how many of the newest archived logs are kept, the elder archived logs are deleted. the default retention limit is 20.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public void WithRetentionLimit(
	long limit
)
```

**VB**<br />
``` VB
Public Sub WithRetentionLimit ( 
	limit As Long
)
```

**F#**<br />
``` F#
member WithRetentionLimit : 
        limit : int64 -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>limit</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/6yy583ek" target="_blank">System.Int64</a><br />The maximum number of archived log files to keep.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_RetentionPolicyConfiguration.md">RetentionPolicyConfiguration Class</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br />
