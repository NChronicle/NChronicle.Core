# RetentionPolicyConfiguration.WithFileSizeLimitInMegabytes Method 
 

Set the file size limit for the output file before it will be archived. The file size limit must be above 50KB. The default file size limit is 100MB;.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public void WithFileSizeLimitInMegabytes(
	long bytes
)
```

**VB**<br />
``` VB
Public Sub WithFileSizeLimitInMegabytes ( 
	bytes As Long
)
```

**F#**<br />
``` F#
member WithFileSizeLimitInMegabytes : 
        bytes : int64 -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>bytes</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/6yy583ek" target="_blank">System.Int64</a><br />The maximum file size for the output file in Megabytes.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_RetentionPolicyConfiguration.md">RetentionPolicyConfiguration Class</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br />
