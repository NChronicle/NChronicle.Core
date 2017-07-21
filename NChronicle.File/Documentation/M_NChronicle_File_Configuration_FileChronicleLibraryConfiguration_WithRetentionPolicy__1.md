# FileChronicleLibraryConfiguration.WithRetentionPolicy(*T*) Method (*T*)
 

Set a custom retention *policy* implementation for the output file.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public T WithRetentionPolicy<T>(
	T policy
)
where T : IRetentionPolicy

```

**VB**<br />
``` VB
Public Function WithRetentionPolicy(Of T As IRetentionPolicy) ( 
	policy As T
) As T
```

**F#**<br />
``` F#
member WithRetentionPolicy : 
        policy : 'T -> 'T  when 'T : IRetentionPolicy

```

<br />

#### Parameters
&nbsp;<dl><dt>policy</dt><dd>Type: *T*<br />The policy to set at the retention policy.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>The type of the specified *policy*.</dd></dl>

#### Return Value
Type: *T*<br />The specified <a href="T_NChronicle_File_Interfaces_IRetentionPolicy.md">IRetentionPolicy</a>.

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_FileChronicleLibraryConfiguration.md">FileChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithRetentionPolicy.md">WithRetentionPolicy Overload</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br />
