# SmtpChronicleLibraryConfiguration.WithCredentials Method (String, String, String)
 

Set the credentials to use to authenticate the sender when sending emails. This applies only whilst using the Network method.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithCredentials(
	string username,
	string password,
	string domain = null
)
```

**VB**<br />
``` VB
Public Sub WithCredentials ( 
	username As String,
	password As String,
	Optional domain As String = Nothing
)
```

**F#**<br />
``` F#
member WithCredentials : 
        username : string * 
        password : string * 
        ?domain : string 
(* Defaults:
        let _domain = defaultArg domain null
*)
-> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>username</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The username for the credentials.</dd><dt>password</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The password associated with the username for the credentials.</dd><dt>domain (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The domain associated with the credentials.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithCredentials.md">WithCredentials Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
