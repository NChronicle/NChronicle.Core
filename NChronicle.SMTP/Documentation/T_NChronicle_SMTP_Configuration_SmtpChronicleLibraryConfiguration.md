# SmtpChronicleLibraryConfiguration Class
 

Container for <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a> configuration.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;NChronicle.SMTP.Configuration.SmtpChronicleLibraryConfiguration<br />
**Namespace:**&nbsp;<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration</a><br />**Assembly:**&nbsp;NChronicle.SMTP (in NChronicle.SMTP.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public class SmtpChronicleLibraryConfiguration : IXmlSerializable
```

**VB**<br />
``` VB
Public Class SmtpChronicleLibraryConfiguration
	Implements IXmlSerializable
```

**F#**<br />
``` F#
type SmtpChronicleLibraryConfiguration =  
    class
        interface IXmlSerializable
    end
```

<br />
The SmtpChronicleLibraryConfiguration type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_AllowingRecurrences.md">AllowingRecurrences</a></td><td>
Do not suppress the sending of emails for recurring chronicle records. The default is to suppress for 24 hours.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_GetSchema.md">GetSchema</a></td><td>
Required for XML serialization, this method offers no functionality.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_Ignoring.md">Ignoring(ChronicleLevel[])</a></td><td>
Ignore records of the specified *levels*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_Ignoring_1.md">Ignoring(String[])</a></td><td>
Ignore records with at least one of the specified *tags*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_ListeningTo.md">ListeningTo(ChronicleLevel[])</a></td><td>
Listen to records of the specified *levels*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_ListeningTo_1.md">ListeningTo(String[])</a></td><td>
Listen to records with at least one of the specified *tags*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_ListeningToAllLevels.md">ListeningToAllLevels</a></td><td>
Listen to records of all ChronicleLevels.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_ListeningToAllTags.md">ListeningToAllTags</a></td><td>
Listen to all records regardless of their tags.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_NotListening.md">NotListening</a></td><td>
Disable library - ignore records of all ChronicleLevels.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_NotUsingSsl.md">NotUsingSsl</a></td><td>
Set all emails to be sent via an insecure connection. Any certificates set for authentication, will be cleared.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_ReadXml.md">ReadXml</a></td><td>
Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_SendingAsynchronously.md">SendingAsynchronously</a></td><td>
Send emails asynchronously; allowing more than one to be sent at any given time. This may also result in memory usage and connection stacking, and suppresses exceptions but is much faster. This is default.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_SendingSynchronously.md">SendingSynchronously</a></td><td>
Send emails synchronously; forcing all emails to be sent one at a time, synchronously.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_SuppressingRecurrences.md">SuppressingRecurrences()</a></td><td>
Suppress the sending of emails for chronicle records that recur within 24 hours after. This is the default.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_SuppressingRecurrences_1.md">SuppressingRecurrences(TimeSpan)</a></td><td>
Suppress the sending of emails for chronicle records that recur within the specified *maximumSuppressionTime* after. The default is to suppress for 24 hours.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod.md">UsingNetworkMethod(String, Int32, Boolean)</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_1.md">UsingNetworkMethod(String, Int32, X509Certificate[])</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_2.md">UsingNetworkMethod(String, Int32, String, SecureString, Boolean)</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_3.md">UsingNetworkMethod(String, Int32, String, SecureString, X509Certificate[])</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_6.md">UsingNetworkMethod(String, Int32, String, String, Boolean)</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_7.md">UsingNetworkMethod(String, Int32, String, String, X509Certificate[])</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_4.md">UsingNetworkMethod(String, Int32, String, SecureString, String, Boolean)</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_5.md">UsingNetworkMethod(String, Int32, String, SecureString, String, X509Certificate[])</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_8.md">UsingNetworkMethod(String, Int32, String, String, String, Boolean)</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingNetworkMethod_9.md">UsingNetworkMethod(String, Int32, String, String, String, X509Certificate[])</a></td><td>
Set all emails to be sent via a network connection to an SMTP server.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingPickupDirectoryMethod.md">UsingPickupDirectoryMethod()</a></td><td>
Set all emails to be sent via copying them into the directory used by IIS for delivery by an external application. Requires SMTP Service to be installed.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingPickupDirectoryMethod_1.md">UsingPickupDirectoryMethod(String, Boolean)</a></td><td>
Set all emails to be sent via copying them into an *pickupDirectory* for delivery by an external application.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_UsingSsl.md">UsingSsl</a></td><td>
Set all emails to be sent via a secure SSL connection. This applies only whilst using the Network method, and is enabled by default.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Code example](media/CodeExample.png "Code example")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBody.md">WithBody</a></td><td>
Specify the HTML email *body* in which records are sent.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithBodyFromFile.md">WithBodyFromFile</a></td><td>
Specify from the specified file *path* the HTML email body in which records are sent.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithCredentials.md">WithCredentials(X509Certificate[])</a></td><td>
Set certificate(s) to use to authenticate the sender when sending emails. This applies only whilst using the Network method, and automatically sets all emails to be sent via a secure SSL connection. This is not persisted when writing the <a href="T_NChronicle_SMTP_SmtpChronicleLibrary.md">SmtpChronicleLibrary</a> to XML.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithCredentials_1.md">WithCredentials(String, SecureString, String)</a></td><td>
Set the credentials to use to authenticate the sender when sending emails. This applies only whilst using the Network method.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithCredentials_2.md">WithCredentials(String, String, String)</a></td><td>
Set the credentials to use to authenticate the sender when sending emails. This applies only whilst using the Network method.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithCriticalMailPriority.md">WithCriticalMailPriority</a></td><td>
Mark all emails for Critical level records with the specified *priority*. The default is <a href="http://msdn2.microsoft.com/en-us/library/ms223213" target="_blank">High</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithDebugMailPriority.md">WithDebugMailPriority</a></td><td>
Mark all emails for Debug level records with the specified *priority*. The default is <a href="http://msdn2.microsoft.com/en-us/library/ms223213" target="_blank">Low</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithInfoMailPriority.md">WithInfoMailPriority</a></td><td>
Mark all emails for Info level records with the specified *priority*. The default is <a href="http://msdn2.microsoft.com/en-us/library/ms223213" target="_blank">Normal</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithLocalTime.md">WithLocalTime</a></td><td>
Set all dates in the output to be rendered in the environments local time zone.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithNoRecipients.md">WithNoRecipients</a></td><td>
Clear all recipients (no email is sent).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithRecipients.md">WithRecipients(MailAddress[])</a></td><td>
Set the *recipients* to which records are emailed. The default is no recipients (no email is sent).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithRecipients_1.md">WithRecipients(String[])</a></td><td>
Set the *recipients* to which records are emailed. The default is no recipients (no email is sent).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSender.md">WithSender(MailAddress)</a></td><td>
Set the *fromAddress* from which records are emailed. The default is no from address (no email is sent).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSender_1.md">WithSender(String, String)</a></td><td>
Set the *senderAddress* from which records are emailed. The default is no from address (no email is sent).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSilentTimeout.md">WithSilentTimeout</a></td><td>
Set the time to wait in milliseconds before sending an email times out, and suppress exceptions from those time outs (only effects synchronous sending, asynchronous sending will always suppress exceptions). This applies only whilst using the Network method, and the default is 100,000 milliseconds (100 seconds).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSubjectLine.md">WithSubjectLine</a></td><td>
Specify the email *subject* line with which records are sent.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithSuccessMailPriority.md">WithSuccessMailPriority</a></td><td>
Mark all emails for Success level records with the specified *priority*. The default is <a href="http://msdn2.microsoft.com/en-us/library/ms223213" target="_blank">Normal</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithTimeout.md">WithTimeout</a></td><td>
Set the time to wait in milliseconds before sending an email times out. This applies only whilst using the Network method, and the default is 100,000 milliseconds (100 seconds).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithTimeZone.md">WithTimeZone</a></td><td>
Set all dates in the output to be rendered in the specified *timeZone*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithUtcTime.md">WithUtcTime</a></td><td>
Set all dates in the output to be rendered in UTC+0.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WithWarningMailPriority.md">WithWarningMailPriority</a></td><td>
Mark all emails for Warning level records with the specified *priority*. The default is <a href="http://msdn2.microsoft.com/en-us/library/ms223213" target="_blank">Normal</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_SMTP_Configuration_SmtpChronicleLibraryConfiguration_WriteXml.md">WriteXml</a></td><td>
Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.</td></tr></table>&nbsp;
<a href="#smtpchroniclelibraryconfiguration-class">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_SMTP_Configuration.md">NChronicle.SMTP.Configuration Namespace</a><br />
