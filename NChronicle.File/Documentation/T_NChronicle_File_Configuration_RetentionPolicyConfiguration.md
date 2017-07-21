# RetentionPolicyConfiguration Class
 

Container for <a href="T_NChronicle_File_RetentionPolicy.md">RetentionPolicy</a> configuration.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;NChronicle.File.Configuration.RetentionPolicyConfiguration<br />
**Namespace:**&nbsp;<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public class RetentionPolicyConfiguration
```

**VB**<br />
``` VB
Public Class RetentionPolicyConfiguration
```

**F#**<br />
``` F#
type RetentionPolicyConfiguration =  class end
```

<br />
The RetentionPolicyConfiguration type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration__ctor.md">RetentionPolicyConfiguration</a></td><td>
Initializes a new instance of the RetentionPolicyConfiguration class</td></tr></table>&nbsp;
<a href="#retentionpolicyconfiguration-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_GetSchema.md">GetSchema</a></td><td>
Required for XML serialization, this method offers no functionality.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_ReadXml.md">ReadXml</a></td><td>
Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithAgeLimit.md">WithAgeLimit</a></td><td>
Set the age limit for the output file before it will be archived. The default age limit is 1 day.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithFileSizeLimitInBytes.md">WithFileSizeLimitInBytes</a></td><td>
Set the file size limit for the output file before it will be archived. The file size limit must be above 50KB. The default file size limit is 100MB;.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithFileSizeLimitInKilobytes.md">WithFileSizeLimitInKilobytes</a></td><td>
Set the file size limit for the output file before it will be archived. The file size limit must be above 50KB. The default file size limit is 100MB;.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithFileSizeLimitInMegabytes.md">WithFileSizeLimitInMegabytes</a></td><td>
Set the file size limit for the output file before it will be archived. The file size limit must be above 50KB. The default file size limit is 100MB;.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithNoAgeLimit.md">WithNoAgeLimit</a></td><td>
Remove the age limit for the output file so as not to archive it - regardless of it's age - unless it extends over the set file size limit.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithNoFileSizeLimit.md">WithNoFileSizeLimit</a></td><td>
Remove the file size limit for the output file so as not to archive it - regardless of it's file size - unless it extends over the set age limit.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithNoRetentionLimit.md">WithNoRetentionLimit</a></td><td>
Remove the retention limit so as not to delete any archived log regardless of the quantity.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WithRetentionLimit.md">WithRetentionLimit</a></td><td>
Set a retention *limit*, defining how many of the newest archived logs are kept, the elder archived logs are deleted. the default retention limit is 20.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_RetentionPolicyConfiguration_WriteXml.md">WriteXml</a></td><td>
Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.</td></tr></table>&nbsp;
<a href="#retentionpolicyconfiguration-class">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br />
