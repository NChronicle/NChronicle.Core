# SmtpChronicleLibraryConfiguration.WithBodyFromFile Method 
 

Specify from the specified file *path* the HTML email body in which records are sent.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithBodyFromFile(
	string path
)
```

**VB**<br />
``` VB
Public Sub WithBodyFromFile ( 
	path As String
)
```

**F#**<br />
``` F#
member WithBodyFromFile : 
        path : string -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>path</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The file path from which to load the HTML.</dd></dl>

## Remarks
This fully supports the token syntax supported by <a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBody.md">WithBody(String)</a>. See documentation for <a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBody.md">WithBody(String)</a> for more information on formatting syntax and options. 
The default HTML email body is:

```
 <body style="text-align: center; background-color: #333;">
    <table style="background-color: #CCC; color: #333; font-family: 'Arial'; width: 800px; //margin:/ 40px auto; text-align: left" cell-spacing="0">
        <tr><td style="padding: 20px 20px 30px 20px;">
            <h2 style="margin: 0;"><strong>{LVL}</strong> level record occurred</h2>
            at <strong>{%yyyy/MM/dd HH:mm:ss.fff}</strong>  
        </td></tr>
        <tr><td style="padding: 10px 20px; background-color: #D8D8D8">
        <strong>{MSG}{MSG!?{EMSG}}</strong>
        </td></tr>
        {EXC?
        <tr><td style="padding: 10px 20px; background-color: #D8D8D8; color: #555;">
        {EXC}
        </td></tr>
        }
        {TAGS?
        <tr><td style="padding: 10px 20px; background-color: #D8D8D8; color: #666;">
            This record has the following tags:<br />{TAGS|, }
        </td></tr>
        }
        <tr><td style="padding: 20px 20px 30px 20px; text-align: center;">
        <small>Brought to you by NChronicle.SMTP.</small>
        </td></tr>
    </table>
</body>
```


## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
