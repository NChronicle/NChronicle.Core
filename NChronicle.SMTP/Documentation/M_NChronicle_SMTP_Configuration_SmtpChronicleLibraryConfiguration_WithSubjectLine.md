# SmtpChronicleLibraryConfiguration.WithSubjectLine Method 
 

Specify the email *subject* line with which records are sent.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithSubjectLine(
	string subject
)
```

**VB**<br />
``` VB
Public Sub WithSubjectLine ( 
	subject As String
)
```

**F#**<br />
``` F#
member WithSubjectLine : 
        subject : string -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>subject</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The email subject line in which to render records (see Remarks).</dd></dl>

## Remarks

This fully supports the token syntax supported by <a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBody.md">WithBody(String)</a>. See documentation for <a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBody.md">WithBody(String)</a> for more information on formatting syntax and options.

The default email subject line is:

```
[{LVL}] {MSG}{MSG!?{EMSG}}
```


## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br /><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBody.md">SmtpChronicleLibraryConfiguration.WithBody(String)</a><br />
