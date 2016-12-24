# SmtpChronicleLibraryConfiguration.SuppressingRecurrences Method 
 

Suppress the sending of emails for chronicle records that recur within 24 hours after. This is the default.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void SuppressingRecurrences()
```

**VB**<br />
``` VB
Public Sub SuppressingRecurrences
```

**F#**<br />
``` F#
member SuppressingRecurrences : unit -> unit 

```

<br />

## Remarks
Emails will not be sent for chronicle records that have already occurred within the last day. A recurring record is determined by the record's developer message, exception message, and exception stack trace.

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_SuppressingRecurrences.md">SuppressingRecurrences Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
