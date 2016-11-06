# ConsoleChronicleLibraryConfiguration Class
 

Container for <a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary</a> configuration.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;NChronicle.Console.Configuration.ConsoleChronicleLibraryConfiguration<br />
**Namespace:**&nbsp;<a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public class ConsoleChronicleLibraryConfiguration : IXmlSerializable
```

**VB**<br />
``` VB
Public Class ConsoleChronicleLibraryConfiguration
	Implements IXmlSerializable
```

**F#**<br />
``` F#
type ConsoleChronicleLibraryConfiguration =  
    class
        interface IXmlSerializable
    end
```

The ConsoleChronicleLibraryConfiguration type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_GetSchema.md">GetSchema</a></td><td>
Required for XML serialization, this method offers no functionality.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_Ignoring.md">Ignoring(ChronicleLevel[])</a></td><td>
Ignore records of the specified *levels*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_Ignoring_1.md">Ignoring(String[])</a></td><td>
Ignore records with at least one of the specified *tags*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningTo.md">ListeningTo(ChronicleLevel[])</a></td><td>
Listen to records of the specified *levels*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningTo_1.md">ListeningTo(String[])</a></td><td>
Listen to records with at least one of the specified *tags*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningToAllLevels.md">ListeningToAllLevels</a></td><td>
Listen to records of all ChronicleLevels.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ListeningToAllTags.md">ListeningToAllTags</a></td><td>
Listen to all records regardless of their tags.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_NotListening.md">NotListening</a></td><td>
Disable library - ignore records of all ChronicleLevels.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_ReadXml.md">ReadXml</a></td><td>
Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithCriticalBackgroundColor.md">WithCriticalBackgroundColor</a></td><td>
Render all Critical level records with the specified *backgroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Black</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithCriticalForegroundColor.md">WithCriticalForegroundColor</a></td><td>
Render all Critical level records with the specified *foregroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Red</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithDebugBackgroundColor.md">WithDebugBackgroundColor</a></td><td>
Render all Debug level records with the specified *backgroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Black</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithDebugForegroundColor.md">WithDebugForegroundColor</a></td><td>
Render all Debug level records with the specified *foregroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Gray</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithInfoBackgroundColor.md">WithInfoBackgroundColor</a></td><td>
Render all Info level records with the specified *backgroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Black</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithInfoForegroundColor.md">WithInfoForegroundColor</a></td><td>
Render all Info level records with the specified *foregroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">White</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithLocalTime.md">WithLocalTime</a></td><td>
Set all dates in the output to be rendered in the environments local time zone.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Code example](media/CodeExample.png "Code example")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithOutputPattern.md">WithOutputPattern</a></td><td>
Specify the *pattern* in which records are written to the console via a specified string.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithSuccessBackgroundColor.md">WithSuccessBackgroundColor</a></td><td>
Render all Success level records with the specified *backgroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Black</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithSuccessForegroundColor.md">WithSuccessForegroundColor</a></td><td>
Render all Success level records with the specified *foregroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Green</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithTimeZone.md">WithTimeZone</a></td><td>
Set all dates in the output to be rendered in the specified *timeZone*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithUtcTime.md">WithUtcTime</a></td><td>
Set all dates in the output to be rendered in UTC+0.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithWarningBackgroundColor.md">WithWarningBackgroundColor</a></td><td>
Render all Warning level records with the specified *backgroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Black</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WithWarningForegroundColor.md">WithWarningForegroundColor</a></td><td>
Render all Warning level records with the specified *foregroundColor*. The default is <a href="http://msdn2.microsoft.com/en-us/library/s66hf68a" target="_blank">Yellow</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration_WriteXml.md">WriteXml</a></td><td>
Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.</td></tr></table>&nbsp;
<a href="#consolechroniclelibraryconfiguration-class">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration Namespace</a><br />
