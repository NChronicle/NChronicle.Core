# SmtpChronicleLibraryConfigurationDelegate Delegate
 

A function to configure a <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a>.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Delegates.md">NChronicle.SMTP.Delegates</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public delegate void SmtpChronicleLibraryConfigurationDelegate(
	SmtpChronicleLibraryConfiguration configuration
)
```

**VB**<br />
``` VB
Public Delegate Sub SmtpChronicleLibraryConfigurationDelegate ( 
	configuration As SmtpChronicleLibraryConfiguration
)
```

**F#**<br />
``` F#
type SmtpChronicleLibraryConfigurationDelegate = 
    delegate of 
        configuration : SmtpChronicleLibraryConfiguration -> unit
```

<br />

#### Parameters
&nbsp;<dl><dt>configuration</dt><dd>Type: <a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">NChronicle.SMTP.Configuration.SmtpChronicleLibraryConfiguration</a><br />The <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a> configuration.</dd></dl>

## See Also


#### Reference
<a href="N_NChronicle_SMTP_Delegates.md">NChronicle.SMTP.Delegates Namespace</a><br />
