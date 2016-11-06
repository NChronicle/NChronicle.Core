# ConsoleChronicleLibraryConfiguration.ListeningTo Method (ChronicleLevel[])
 

Listen to records of the specified *levels*.

**Namespace:**&nbsp;<a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void ListeningTo(
	params ChronicleLevel[] levels
)
```

**VB**<br />
``` VB
Public Sub ListeningTo ( 
	ParamArray levels As ChronicleLevel()
)
```

**F#**<br />
``` F#
member ListeningTo : 
        levels : ChronicleLevel[] -> unit 

```


#### Parameters
&nbsp;<dl><dt>levels</dt><dd>Type: NChronicle.Core.Model.ChronicleLevel[]<br />ChronicleLevels to listen to records of.</dd></dl>

## Remarks
This can be invoked multiple times with further *levels* to listen to, therefore invoking ListeningTo(ChronicleLevel[]) once with 3 ChronicleLevels and invoking ListeningTo(ChronicleLevel[]) 3 times with each of the same ChronicleLevels is semantically synonymous. As an exception, as the default collection of record levels listened to are volatile, if the levels listened to are still their default, invoking ListeningTo(ChronicleLevel[]) will clear these levels and listen only to records of those *levels* specified in that and future invocations. The default listened to levels are: CriticalWarningSuccessInfo

## See Also


#### Reference
<a href="T_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration.md">ConsoleChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningTo.md">ListeningTo Overload</a><br /><a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration Namespace</a><br /><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_Ignoring.md">ConsoleChronicleLibraryConfiguration.Ignoring(ChronicleLevel[])</a><br /><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningToAllLevels.md">ConsoleChronicleLibraryConfiguration.ListeningToAllLevels()</a><br /><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_NotListening.md">ConsoleChronicleLibraryConfiguration.NotListening()</a><br />
