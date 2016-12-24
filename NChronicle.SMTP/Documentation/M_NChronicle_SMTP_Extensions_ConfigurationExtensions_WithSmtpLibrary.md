# ConfigurationExtensions.WithSmtpLibrary Method 
 

Create and add a <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a> to the specified ChronicleConfiguration.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Extensions.md">NChronicle.SMTP.Extensions</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static SmtpChronicleLibrary WithSmtpLibrary(
	this ChronicleConfiguration config
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function WithSmtpLibrary ( 
	config As ChronicleConfiguration
) As SmtpChronicleLibrary
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member WithSmtpLibrary : 
        config : ChronicleConfiguration -> SmtpChronicleLibrary 

```

<br />

#### Parameters
&nbsp;<dl><dt>config</dt><dd>Type: NChronicle.Core.Model.ChronicleConfiguration<br />The ChronicleConfiguration for which to add the <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a>.</dd></dl>

#### Return Value
Type: <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a><br />The created <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a>.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type ChronicleConfiguration. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Extensions_ConfigurationExtensions.md">ConfigurationExtensions Class</a><br /><a href="N_NChronicle_SMTP_Extensions.md">NChronicle.SMTP.Extensions Namespace</a><br />
