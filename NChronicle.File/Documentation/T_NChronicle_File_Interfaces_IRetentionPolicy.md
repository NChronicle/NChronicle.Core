# IRetentionPolicy Interface
 

A file retention policy for controlling the archiving of a <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a>'s output file.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Interfaces.md">NChronicle.File.Interfaces</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public interface IRetentionPolicy : IXmlSerializable
```

**VB**<br />
``` VB
Public Interface IRetentionPolicy
	Inherits IXmlSerializable
```

**F#**<br />
``` F#
type IRetentionPolicy =  
    interface
        interface IXmlSerializable
    end
```

<br />
The IRetentionPolicy type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Interfaces_IRetentionPolicy_CheckPolicy.md">CheckPolicy</a></td><td>
Check whether the output file at the given *path* should have the file retention policy invoked upon it.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/6f7z1347" target="_blank">GetSchema</a></td><td>
This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <a href="http://msdn2.microsoft.com/en-us/library/f7th40y8" target="_blank">XmlSchemaProviderAttribute</a> to the class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/fhd7bk0a" target="_blank">IXmlSerializable</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Interfaces_IRetentionPolicy_InvokePolicy.md">InvokePolicy</a></td><td>
Invoke the file retention policy on the output file at the given *path*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/w6txf8t9" target="_blank">ReadXml</a></td><td>
Generates an object from its XML representation.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/fhd7bk0a" target="_blank">IXmlSerializable</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/9yt8e1yw" target="_blank">WriteXml</a></td><td>
Converts an object into its XML representation.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/fhd7bk0a" target="_blank">IXmlSerializable</a>.)</td></tr></table>&nbsp;
<a href="#iretentionpolicy-interface">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_File_Interfaces.md">NChronicle.File.Interfaces Namespace</a><br />
