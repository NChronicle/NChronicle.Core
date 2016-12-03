# ConsoleChronicleLibrary.Store Method 
 

Render the record to the console (if not filtered by ChronicleLevel or tag ignorance).

**Namespace:**&nbsp;<a href="N_NChronicle_Console.md">NChronicle.Console</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.2.0 (1.0.2.0)

## Syntax

**C#**<br />
``` C#
public void Store(
	ChronicleRecord record
)
```

**VB**<br />
``` VB
Public Sub Store ( 
	record As ChronicleRecord
)
```

**F#**<br />
``` F#
abstract Store : 
        record : ChronicleRecord -> unit 
override Store : 
        record : ChronicleRecord -> unit 
```


#### Parameters
&nbsp;<dl><dt>record</dt><dd>Type: NChronicle.Core.Model.ChronicleRecord<br />The ChronicleRecord to render.</dd></dl>

#### Implements
IChronicleLibrary.Store(ChronicleRecord)<br />

## See Also


#### Reference
<a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary Class</a><br /><a href="N_NChronicle_Console.md">NChronicle.Console Namespace</a><br />
