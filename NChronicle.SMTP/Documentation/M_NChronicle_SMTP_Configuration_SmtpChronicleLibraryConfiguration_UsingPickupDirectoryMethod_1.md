# SmtpChronicleLibraryConfiguration.UsingPickupDirectoryMethod Method (String, Boolean)
 

Set all emails to be sent via copying them into an *pickupDirectory* for delivery by an external application.

**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void UsingPickupDirectoryMethod(
	string pickupDirectory,
	bool createDirectory = false
)
```

**VB**<br />
``` VB
Public Sub UsingPickupDirectoryMethod ( 
	pickupDirectory As String,
	Optional createDirectory As Boolean = false
)
```

**F#**<br />
``` F#
member UsingPickupDirectoryMethod : 
        pickupDirectory : string * 
        ?createDirectory : bool 
(* Defaults:
        let _createDirectory = defaultArg createDirectory false
*)
-> unit 

```

<br />

#### Parameters
&nbsp;<dl><dt>pickupDirectory</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Path to the directory in which to place emails.</dd><dt>createDirectory (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Whether if the specified *pickupDirectory* does not exist to create it, throw an exception.</dd></dl>

## See Also


#### Reference
<a href="T_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration.md">SmtpChronicleLibraryConfiguration Class</a><br /><a href="Overload_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingPickupDirectoryMethod.md">UsingPickupDirectoryMethod Overload</a><br /><a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
