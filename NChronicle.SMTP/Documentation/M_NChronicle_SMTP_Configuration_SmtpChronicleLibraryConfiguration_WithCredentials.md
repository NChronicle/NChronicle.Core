# SmtpChronicleLibraryConfiguration.WithCredentials Method (X509Certificate[])
 

Set certificate(s) to use to authenticate the sender when sending emails. This applies only whilst using the Network method, and automatically sets all emails to be sent via a secure SSL connection. This is not persisted when writing the <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a> to XML.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithCredentials(
	params X509Certificate[] certificates
)
```

**VB**<br />
``` VB
Public Sub WithCredentials ( 
	ParamArray certificates As X509Certificate()
)
```

**F#**<br />
``` F#
member WithCredentials : 
        certificates : X509Certificate[] -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>certificates</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/9yc7tebx" target="_blank">System.Security.Cryptography.X509Certificates.X509Certificate</a>[]<br />The certificates to authenticate with.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithCredentials.md">WithCredentials Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
