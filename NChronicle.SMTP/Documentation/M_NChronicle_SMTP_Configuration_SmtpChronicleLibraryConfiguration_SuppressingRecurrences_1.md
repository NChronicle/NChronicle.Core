# SmtpChronicleLibraryConfiguration.SuppressingRecurrences Method (TimeSpan)
 

Suppress the sending of emails for chronicle records that recur within the specified *maximumSuppressionTime* after. The default is to suppress for 24 hours.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void SuppressingRecurrences(
	TimeSpan maximumSuppressionTime
)
```

**VB**<br />
``` VB
Public Sub SuppressingRecurrences ( 
	maximumSuppressionTime As TimeSpan
)
```

**F#**<br />
``` F#
member SuppressingRecurrences : 
        maximumSuppressionTime : TimeSpan -> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>maximumSuppressionTime</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/269ew577" target="_blank">System.TimeSpan</a><br />The amount of time before a recurring records sends another email.</dd></dl>

## Remarks
Emails will not be sent for chronicle records that have already occurred within the given *maximumSuppressionTime* before. A recurring record is determined by the record's developer message, exception message, and exception stack trace.

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_SuppressingRecurrences.md">SuppressingRecurrences Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
