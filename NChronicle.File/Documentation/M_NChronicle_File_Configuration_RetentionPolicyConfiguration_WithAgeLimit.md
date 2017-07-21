# RetentionPolicyConfiguration.WithAgeLimit Method 
 

Set the age limit for the output file before it will be archived. The default age limit is 1 day.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public void WithAgeLimit(
	TimeSpan timeSpan
)
```

**VB**<br />
``` VB
Public Sub WithAgeLimit ( 
	timeSpan As TimeSpan
)
```

**F#**<br />
``` F#
member WithAgeLimit : 
        timeSpan : TimeSpan -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>timeSpan</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/269ew577" target="_blank">System.TimeSpan</a><br />The maximum age for the output file as a <a href="http://msdn2.microsoft.com/en-us/library/269ew577" target="_blank">TimeSpan</a>.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_RetentionPolicyConfiguration.md">RetentionPolicyConfiguration Class</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br />
