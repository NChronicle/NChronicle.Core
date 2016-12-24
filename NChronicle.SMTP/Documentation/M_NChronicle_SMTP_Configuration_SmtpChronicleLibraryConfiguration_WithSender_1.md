# SmtpChronicleLibraryConfiguration.WithSender Method (String, String)
 

Set the *senderAddress* from which records are emailed. The default is no from address (no email is sent).

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithSender(
	string senderAddress,
	string displayName = null
)
```

**VB**<br />
``` VB
Public Sub WithSender ( 
	senderAddress As String,
	Optional displayName As String = Nothing
)
```

**F#**<br />
``` F#
member WithSender : 
        senderAddress : string * 
        ?displayName : string 
(* Defaults:
        let _displayName = defaultArg displayName null
*)
-> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>senderAddress</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The email address to send emails from.</dd><dt>displayName (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The display name for the email address to send emails from.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSender.md">WithSender Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
