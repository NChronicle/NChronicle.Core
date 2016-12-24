# SmtpChronicleLibraryConfiguration.UsingNetworkMethod Method (String, Int32, String, String, String, X509Certificate[])
 

Set all emails to be sent via a network connection to an SMTP server.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void UsingNetworkMethod(
	string host,
	int port,
	string username,
	string password,
	string domain,
	params X509Certificate[] certificates
)
```

**VB**<br />
``` VB
Public Sub UsingNetworkMethod ( 
	host As String,
	port As Integer,
	username As String,
	password As String,
	domain As String,
	ParamArray certificates As X509Certificate()
)
```

**F#**<br />
``` F#
member UsingNetworkMethod : 
        host : string * 
        port : int * 
        username : string * 
        password : string * 
        domain : string * 
        certificates : X509Certificate[] -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>host</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Name or IP address of SMTP server.</dd><dt>port</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />Port to connect to the SMTP server via.</dd><dt>username</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The username for the credentials to authenticate with.</dd><dt>password</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The password associated with the username for the credentials.</dd><dt>domain</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The domain associated with the credentials.</dd><dt>certificates</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/9yc7tebx" target="_blank">System.Security.Cryptography.X509Certificates.X509Certificate</a>[]<br />The certificates to authenticate with (sets all emails to be sent by a secure SSL connection).</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod.md">UsingNetworkMethod Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
