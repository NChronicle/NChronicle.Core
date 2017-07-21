# FileChronicleLibrary.Store Method 
 

Render the record to the file (if not filtered by ChronicleLevel or tag ignorance).

**Namespace:**&nbsp;<a href="N_NChronicle_File.md">NChronicle.File</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

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

<br />

#### Parameters
&nbsp;<dl><dt>record</dt><dd>Type: NChronicle.Core.Model.ChronicleRecord<br />The ChronicleRecord to render.</dd></dl>

#### Implements
IChronicleLibrary.Store(ChronicleRecord)<br />

## See Also


#### Reference
<a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary Class</a><br /><a href="N_NChronicle_File.md">NChronicle.File Namespace</a><br />
