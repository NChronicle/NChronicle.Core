# SmtpChronicleLibraryConfiguration.WithSilentTimeout Method 
 

Set the time to wait in milliseconds before sending an email times out, and suppress exceptions from those time outs (only effects synchronous sending, asynchronous sending will always suppress exceptions). This applies only whilst using the Network method, and the default is 100,000 milliseconds (100 seconds).

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithSilentTimeout(
	int timeout
)
```

**VB**<br />
``` VB
Public Sub WithSilentTimeout ( 
	timeout As Integer
)
```

**F#**<br />
``` F#
member WithSilentTimeout : 
        timeout : int -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>timeout</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />Timeout in milliseconds.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
