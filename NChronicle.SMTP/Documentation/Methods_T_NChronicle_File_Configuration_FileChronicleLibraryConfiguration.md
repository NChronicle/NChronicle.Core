# FileChronicleLibraryConfiguration Methods
 

The <a href="T_NChronicle_File_Configuration_FileChronicleLibraryConfiguration.md">FileChronicleLibraryConfiguration</a> type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_GetSchema.md">GetSchema</a></td><td>
Required for XML serialization, this method offers no functionality.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_Ignoring.md">Ignoring(ChronicleLevel[])</a></td><td>
Ignore records of the specified *levels*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_Ignoring_1.md">Ignoring(String[])</a></td><td>
Ignore records with at least one of the specified *tags*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningTo.md">ListeningTo(ChronicleLevel[])</a></td><td>
Listen to records of the specified *levels*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningTo_1.md">ListeningTo(String[])</a></td><td>
Listen to records with at least one of the specified *tags*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningToAllLevels.md">ListeningToAllLevels</a></td><td>
Listen to records of all ChronicleLevels.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ListeningToAllTags.md">ListeningToAllTags</a></td><td>
Listen to all records regardless of their tags.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_NotListening.md">NotListening</a></td><td>
Disable library - ignore records of all ChronicleLevels.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_ReadXml.md">ReadXml</a></td><td>
Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithLocalTime.md">WithLocalTime</a></td><td>
Set all dates in the output to be rendered in the environments local time zone.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithNoRetentionPolicy.md">WithNoRetentionPolicy</a></td><td>
Remove any set <a href="T_NChronicle_File_Interfaces_IRetentionPolicy.md">IRetentionPolicy</a> for the output file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithOutputPath.md">WithOutputPath</a></td><td>
Set the file *path* in which rendered records are appended, the path maybe absolute or relative to the application's working directory. The default file path is the application's working direction with the file name 'chronicle.log'.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Code example](media/CodeExample.png "Code example")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithOutputPattern.md">WithOutputPattern</a></td><td>
Specify the *pattern* in which records are written to the file via a specified string.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithRetentionPolicy.md">WithRetentionPolicy()</a></td><td>
Set a standard retention policy for the output file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithRetentionPolicy__1.md">WithRetentionPolicy(T)(T)</a></td><td>
Set a custom retention *policy* implementation for the output file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithTimeZone.md">WithTimeZone</a></td><td>
Set all dates in the output to be rendered in the specified *timeZone*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WithUtcTime.md">WithUtcTime</a></td><td>
Set all dates in the output to be rendered in UTC+0.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Configuration_FileChronicleLibraryConfiguration_WriteXml.md">WriteXml</a></td><td>
Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.</td></tr></table>&nbsp;
<a href="#filechroniclelibraryconfiguration-methods">Back to Top</a>

## See Also


#### Reference
<a href="T_NChronicle_File_Configuration_FileChronicleLibraryConfiguration.md">FileChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_File_Configuration.md">NChronicle.File.Configuration Namespace</a><br />
