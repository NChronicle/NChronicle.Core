# ConsoleChronicleLibraryConfiguration.WithOutputPattern Method 
 

Specify the *pattern* in which records are written to the console via a specified string.

**Namespace:**&nbsp;<a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WithOutputPattern(
	string pattern
)
```

**VB**<br />
``` VB
Public Sub WithOutputPattern ( 
	pattern As String
)
```

**F#**<br />
``` F#
member WithOutputPattern : 
        pattern : string -> unit 

```


#### Parameters
&nbsp;<dl><dt>pattern</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The output pattern in which to render records (see Remarks).</dd></dl>

## Remarks

There are a number of keywords and patterns that can be used to describe the format the console output should take, all of which are wrapped in an opening brace (`{`) and closing brace (`}`) and called tokens. Everything else is treated as an non-manipulated string literal.

Standard keywords can be used independently or as part of a conditional. Independently they are replaced in place with their value in the output.

Conditional tokens allow you to only render specific output when a specified standard keyword exists and it's value is meaningful. It can be created by starting a token with the standard keyword followed by a `?` character. The keyword will be tested (not rendered) to assess whether it exists or resolves to a non-null or non-empty value, if it does, the sub-pattern - everything after the `?` character to the end of the token - is visited.

Standard keywords available are:
`TAGS` The tags for the record delimited by a comma and a space (`,`). `TH` The thread ID the record was created in. `MSG` The message for the record if any. May be absent. `EXC` The exception for the record if any. May be absent.
Functional tokens are tokens which may take in extra arguments to render; these start with a functional keyword and a `|` character. Arguments follow the `|` character until the end of the token and are split by a `|` character.

Functional keywords available are:
`TAGS` Prints all the tags for the record, taking 1 string argument to be used as the delimiter.
Tokens starting with a `%` character are <a href="http://msdn2.microsoft.com/en-us/library/03ybds8y" target="_blank">DateTime</a> tokens, rendering the current time in it's place. Everything after the `%` character to the end of the token is used as the output format for the <a href="http://msdn2.microsoft.com/en-us/library/03ybds8y" target="_blank">DateTime</a>, therefore any format string valid for <a href="http://msdn2.microsoft.com/en-us/library/zdtaw1bw" target="_blank">ToString(String)</a> is valid here. See documentation for <a href="http://msdn2.microsoft.com/en-us/library/zdtaw1bw" target="_blank">ToString(String)</a> for more information on formatting syntax and options.

The default output pattern is:

```
"{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS}]}"
```


## Examples

```
"{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}} [{TAGS| / }]}"
```

```
"{%yyyy/MM/dd HH:mm:ss.fff}"
```

In this example, the pattern first uses a <a href="http://msdn2.microsoft.com/en-us/library/03ybds8y" target="_blank">DateTime</a> token to print out the time for the rendered record. It's format is defined with a year first date format, then the time in down to milliseconds. In the output, this would look similar to `1991/03/22 10:58:30:423`.

```
[{TH}]
```

Next in this example is the managed thread Id for the record surrounded in square braces.

```
{MSG?{MSG} {EXC?\n}}
```

Following this is a conditional token testing the `MSG` keyword, if the record's message is not absent, then it will render the message, then test the `EXC` keyword, appending a new line to the message if there is an exception.

```
{EXC?{EXC}}
```

Next is another conditional, rendering the exception and a new line if there is one.

```
[{TAGS| / }]}
```

Lastly - inside square braces - is a functional token with the `TAGS` functional keyword, the argument here is a string containing a `/` character padded by space characters; this is used as the delimiter for the `TAGS` functional keyword. In the output, this would look similar to `"[tag1 / tag2 / tag3]"`.

The final output of a record with the pattern in this example would look similar to:

```
1991/03/22 10:58:30:423 [13] An exception occurred in the calculation.
System.DivideByZeroException: Attempted to divide by zero.
at NChronicle.TestConsole.Program.Test() in D:\Development\Live\NChronicle\NChronicle.TestConsole\Program.cs:line 44
[tag1 / tag2 / tag3]
```
.

## See Also


#### Reference
<a href="T_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration.md">ConsoleChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration Namespace</a><br />
