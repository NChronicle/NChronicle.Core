# FileChronicleLibraryConfiguration.Ignoring Method (ChronicleLevel[])
 

Ignore records of the specified *levels*.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public void Ignoring(
	params ChronicleLevel[] levels
)
```

**VB**<br />
``` VB
Public Sub Ignoring ( 
	ParamArray levels As ChronicleLevel()
)
```

**F#**<br />
``` F#
member Ignoring : 
        levels : ChronicleLevel[] -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>levels</dt><dd>Type: NChronicle.Core.Model.ChronicleLevel[]<br />ChronicleLevels to ignore records of.</dd></dl>

## Remarks
This can be invoked multiple times with further *levels* to ignore, therefore invoking Ignoring(ChronicleLevel[]) once with 3 ChronicleLevels and invoking Ignoring(ChronicleLevel[]) 3 times with each of the same ChronicleLevels is semantically synonymous. As an exception, as the default collection of record levels listened to are volatile, if the levels listened to are still their default, invoking Ignoring(ChronicleLevel[]) will clear these levels and ignore records only of those *levels* specified in that and future invocations. The default listened to levels are: CriticalWarningSuccessInfo

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_FileChronicleLibraryConfiguration.md">FileChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_Ignoring.md">Ignoring Overload</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br /><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningTo.md">FileChronicleLibraryConfiguration.ListeningTo(ChronicleLevel[])</a><br /><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningToAllLevels.md">FileChronicleLibraryConfiguration.ListeningToAllLevels()</a><br /><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_NotListening.md">FileChronicleLibraryConfiguration.NotListening()</a><br />
