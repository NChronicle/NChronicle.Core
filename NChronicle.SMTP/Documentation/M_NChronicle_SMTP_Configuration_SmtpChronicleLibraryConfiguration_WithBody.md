# SmtpChronicleLibraryConfiguration.WithBody Method 
 

Specify the HTML email *body* in which records are sent.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithBody(
	string body
)
```

**VB**<br />
``` VB
Public Sub WithBody ( 
	body As String
)
```

**F#**<br />
``` F#
member WithBody : 
        body : string -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>body</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The email body in which to render records (see Remarks).</dd></dl>

## Remarks

There are a number of keywords and patterns that can be used to describe the format the email body should take, all of which are wrapped in an opening brace (`{`) and a closing brace (`}`) and called tokens. Everything else is treated as an non-manipulated string literal.

Standard keywords can be used independently or as part of a conditional. Independently they are replaced in place with their value in the output.

Conditional tokens allow you to only render a part of the pattern when a specified standard keyword exists and it's value is meaningful. It can be created by starting a token with the standard keyword followed by a question mark (`?`) character. The keyword will be tested (not rendered) to assess whether it exists or resolves to a non-null or non-empty value, if it does, the sub-pattern - everything after the question mark (`?`) character to the end of the token - is visited.

Inverse conditional tokens can be used as an opposite to conditional tokens, and render everything after the question mark (`?`) character if the keyword does not exist or have a meaningful value. It can be created by placing an exclamation mark (`!`) character before the question mark character (`?`) in an otherwise normal conditional token (`!?`).

Standard keywords available are:
`LVL` The level of this record. `TAGS` The tags for the record delimited by a comma and a space (`,`). `TH` The thread ID the record was created in. `MSG` The developer message for the record if any. May be absent. `EMSG` The exception message for the record if any. May be absent. `EXC` The full exception for the record if any. May be absent.
Functional tokens are tokens which may take in extra arguments to render; these start with a functional keyword and a `|` character. Arguments follow the `|` character until the end of the token and are split by a `|` character.

Functional keywords available are:
`TAGS` Prints all the tags for the record, taking 1 string argument to be used as the delimiter.
Tokens starting with a `%` character are <a href="http://msdn2.microsoft.com/en-us/library/03ybds8y" target="_blank">DateTime</a> tokens, rendering the current time in it's place. Everything after the `%` character to the end of the token is used as the output format for the <a href="http://msdn2.microsoft.com/en-us/library/03ybds8y" target="_blank">DateTime</a>, therefore any format string valid for <a href="http://msdn2.microsoft.com/en-us/library/zdtaw1bw" target="_blank">ToString(String)</a> is valid here. See documentation for <a href="http://msdn2.microsoft.com/en-us/library/zdtaw1bw" target="_blank">ToString(String)</a> for more information on formatting syntax and options.

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


## Examples

```
<p style="text-align: center;"><table style="width: 800px; margin: 0 auto; text-align: left"><tr><td>
A <strong>{LVL}</strong> level record occurred at <strong>{%yyyy/MM/dd HH:mm:ss.fff}</strong>:
{MSG?
    <br/><br/>
    {MSG}
}
{EXC?
    <br/><br/>
    {EXC}
}
{TAGS?
    <br/><br/>
    This record has the following tags:
    <br/>
    {TAGS| / }
}
</td></tr></table></p>
```

```
<p style="text-align: center;"><table style="width: 800px; margin: 0 auto; text-align: left"><tr><td>
A <strong>{LVL}</strong> level record occurred at <strong>{%yyyy/MM/dd HH:mm:ss.fff}</strong>:
```

In this example, the email body starts with a few HTML tags then uses a level token and a <a href="http://msdn2.microsoft.com/en-us/library/03ybds8y" target="_blank">DateTime</a> token to print out level and the time of occurrence for the record. It's format of the date is defined with a year first date format, then the time down to millisecond. In the email body, this would look similar to `1991/03/22 10:58:30:423`.

```
{MSG?
    <br/><br/>
    {MSG}
}
```

Following this is a conditional token testing the `MSG` token, if the record's message is not absent, then - with a few HTML line breaks - renders the record's message.

```
{EXC?
    <br/><br/>
    {EXC}
}
```

Next is another conditional, rendering the exception after a couple of HTML line breaks if there is one.

```
{TAGS?
    <br/><br/>
    This record has the following tags:
    <br/>
    {TAGS| / }
}
</td></tr></table></p>
```

Lastly - with a few more line breaks and a preceding sentence - is a functional token with the `TAGS` functional keyword, the argument here is a string containing a `/` character padded by space characters; this is used as the delimiter for the `TAGS` functional keyword. In the output, this would look similar to `"tag1 / tag2 / tag3"`.

The final output of a record with the email body in this example would look similar to:

```
A Critical level record occurred at 1991/03/22 10:58:30:423:

An exception occurred in the calculation.

System.DivideByZeroException: Attempted to divide by zero.
at NChronicle.TestConsole.Program.Test() in D:\Development\Live\NChronicle\NChronicle.TestConsole\Program.cs:line 44

This record has the following tags:
tag1 / tag2 / tag3
```


## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
