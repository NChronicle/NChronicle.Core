# InvalidFilePathException Class
 

The exception that is thrown when the specified file path is invalid.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;<a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">System.Exception</a><br />&nbsp;&nbsp;&nbsp;&nbsp;NChronicle.File.Exceptions.InvalidFilePathException<br />
**Namespace:**&nbsp;<a href="N_NChronicle_File_Exceptions.md">NChronicle.File.Exceptions</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public class InvalidFilePathException : Exception
```

**VB**<br />
``` VB
Public Class InvalidFilePathException
	Inherits Exception
```

**F#**<br />
``` F#
type InvalidFilePathException =  
    class
        inherit Exception
    end
```

<br />
The InvalidFilePathException type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Exceptions_InvalidFilePathException__ctor.md">InvalidFilePathException(String)</a></td><td>
Create a new InvalidFilePathException instance with the specified *message*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Exceptions_InvalidFilePathException__ctor_1.md">InvalidFilePathException(String, Exception)</a></td><td>
Create a new InvalidFilePathException instance with the specified *message* and *innerException*.</td></tr></table>&nbsp;
<a href="#invalidfilepathexception-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/2wyfbc48" target="_blank">Data</a></td><td>
Gets a collection of key/value pairs that provide additional user-defined information about the exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/71tawy4s" target="_blank">HelpLink</a></td><td>
Gets or sets a link to the help file associated with this exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/sh5cw61c" target="_blank">HResult</a></td><td>
Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/902sca80" target="_blank">InnerException</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a> instance that caused the current exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/9btwf6wk" target="_blank">Message</a></td><td>
Gets a message that describes the current exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/85weac5w" target="_blank">Source</a></td><td>
Gets or sets the name of the application or the object that causes the error.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dxzhy005" target="_blank">StackTrace</a></td><td>
Gets a string representation of the immediate frames on the call stack.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/2wchw354" target="_blank">TargetSite</a></td><td>
Gets the method that throws the current exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr></table>&nbsp;
<a href="#invalidfilepathexception-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/49kcee3b" target="_blank">GetBaseException</a></td><td>
When overridden in a derived class, returns the <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a> that is the root cause of one or more subsequent exceptions.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/fwb1489e" target="_blank">GetObjectData</a></td><td>
When overridden in a derived class, sets the <a href="http://msdn2.microsoft.com/en-us/library/a9b6042e" target="_blank">SerializationInfo</a> with information about the exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/44zb316t" target="_blank">GetType</a></td><td>
Gets the runtime type of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/es4y6f7e" target="_blank">ToString</a></td><td>
Creates and returns a string representation of the current exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr></table>&nbsp;
<a href="#invalidfilepathexception-class">Back to Top</a>

## Events
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Protected event](media/protevent.gif "Protected event")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ee332915" target="_blank">SerializeObjectState</a></td><td>
Occurs when an exception is serialized to create an exception state object that contains serialized data about the exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/c18k6c59" target="_blank">Exception</a>.)</td></tr></table>&nbsp;
<a href="#invalidfilepathexception-class">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_File_Exceptions.md">NChronicle.File.Exceptions Namespace</a><br />
