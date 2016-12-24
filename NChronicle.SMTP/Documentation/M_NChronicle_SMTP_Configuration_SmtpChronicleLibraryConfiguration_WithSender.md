# SmtpChronicleLibraryConfiguration.WithSender Method (MailAddress)
 

Set the *fromAddress* from which records are emailed. The default is no from address (no email is sent).

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithSender(
	MailAddress fromAddress
)
```

**VB**<br />
``` VB
Public Sub WithSender ( 
	fromAddress As MailAddress
)
```

**F#**<br />
``` F#
member WithSender : 
        fromAddress : MailAddress -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>fromAddress</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/yh392kbs" target="_blank">System.Net.Mail.MailAddress</a><br />The email address to send emails from.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSender.md">WithSender Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
