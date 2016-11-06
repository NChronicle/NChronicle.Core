# NChronicle.Configure Method 
 

Configure all new and existing <a href="T_NChronicle_Core_Model_Chronicle.md">Chronicle</a> instances with the specified options and <a href="T_NChronicle_Core_Interfaces_IChronicleLibrary.md">IChronicleLibrary</a>s.

**Namespace:**&nbsp;<a href="N_NChronicle_Core.md">NChronicle.Core</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static void Configure(
	ChronicleConfigurationDelegate configurationDelegate
)
```

**VB**<br />
``` VB
Public Shared Sub Configure ( 
	configurationDelegate As ChronicleConfigurationDelegate
)
```

**F#**<br />
``` F#
static member Configure : 
        configurationDelegate : ChronicleConfigurationDelegate -> unit 

```


#### Parameters
&nbsp;<dl><dt>configurationDelegate</dt><dd>Type: <a href="T_NChronicle_Core_Delegates_ChronicleConfigurationDelegate.md">NChronicle.Core.Delegates.ChronicleConfigurationDelegate</a><br />A function to set <a href="T_NChronicle_Core_NChronicle.md">NChronicle</a> configuration.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_Core_NChronicle.md">NChronicle Class</a><br /><a href="N_NChronicle_Core.md">NChronicle.Core Namespace</a><br />
