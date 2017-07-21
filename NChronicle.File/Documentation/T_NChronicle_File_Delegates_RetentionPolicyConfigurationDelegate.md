# RetentionPolicyConfigurationDelegate Delegate
 

A function to configure a <a href="T_NChronicle_File_RetentionPolicy.md">RetentionPolicy</a>.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Delegates.md">NChronicle.File.Delegates</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public delegate void RetentionPolicyConfigurationDelegate(
	RetentionPolicyConfiguration configuration
)
```

**VB**<br />
``` VB
Public Delegate Sub RetentionPolicyConfigurationDelegate ( 
	configuration As RetentionPolicyConfiguration
)
```

**F#**<br />
``` F#
type RetentionPolicyConfigurationDelegate = 
    delegate of 
        configuration : RetentionPolicyConfiguration -> unit
```

<br />

#### Parameters
&nbsp;<dl><dt>configuration</dt><dd>Type: <a href="T_NChronicle_File_Configuration_RetentionPolicyConfiguration.md">NChronicle.File.Configuration.RetentionPolicyConfiguration</a><br />The <a href="T_NChronicle_File_RetentionPolicy.md">RetentionPolicy</a> configuration.</dd></dl>

## See Also


#### Reference
<a href="N_NChronicle_File_Delegates.md">NChronicle.File.Delegates Namespace</a><br />
