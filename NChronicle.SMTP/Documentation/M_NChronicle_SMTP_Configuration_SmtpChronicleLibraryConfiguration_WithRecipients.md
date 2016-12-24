# SmtpChronicleLibraryConfiguration.WithRecipients Method (MailAddress[])
 

Set the *recipients* to which records are emailed. The default is no recipients (no email is sent).

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithRecipients(
	params MailAddress[] recipients
)
```

**VB**<br />
``` VB
Public Sub WithRecipients ( 
	ParamArray recipients As MailAddress()
)
```

**F#**<br />
``` F#
member WithRecipients : 
        recipients : MailAddress[] -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>recipients</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/yh392kbs" target="_blank">System.Net.Mail.MailAddress</a>[]<br />The recipients to send the emails to.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithRecipients.md">WithRecipients Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
