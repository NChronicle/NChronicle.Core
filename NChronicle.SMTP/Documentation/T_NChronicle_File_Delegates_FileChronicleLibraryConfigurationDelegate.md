# FileChronicleLibraryConfigurationDelegate Delegate
 

A function to configure a <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a>.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Delegates.md">NChronicle.File.Delegates</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public delegate void FileChronicleLibraryConfigurationDelegate(
	FileChronicleLibraryConfiguration configuration
)
```

**VB**<br />
``` VB
Public Delegate Sub FileChronicleLibraryConfigurationDelegate ( 
	configuration As FileChronicleLibraryConfiguration
)
```

**F#**<br />
``` F#
type FileChronicleLibraryConfigurationDelegate = 
    delegate of 
        configuration : FileChronicleLibraryConfiguration -> unit
```

<br />

#### Parameters
&nbsp;<dl><dt>configuration</dt><dd>Type: <a href="T_NChronicle_File_Configuration_FileChronicleLibraryConfiguration.md">NChronicle.File.Configuration.FileChronicleLibraryConfiguration</a><br />The <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a> configuration.</dd></dl>

## See Also


#### Reference
<a href="N_NChronicle_File_Delegates.md">NChronicle.File.Delegates Namespace</a><br />
