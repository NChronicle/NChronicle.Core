# IRetentionPolicy Interface
 

A file retention policy for controlling the archiving of a <a href="T_NChronicle_File_FileChronicleLibrary.md">FileChronicleLibrary</a>'s output file.

**Namespace:**&nbsp;<a href="N_NChronicle_File_Interfaces.md">NChronicle.File.Interfaces</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public interface IRetentionPolicy
```

**VB**<br />
``` VB
Public Interface IRetentionPolicy
```

**F#**<br />
``` F#
type IRetentionPolicy =  interface end
```

<br />
The IRetentionPolicy type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Interfaces_IRetentionPolicy_CheckPolicy.md">CheckPolicy</a></td><td>
Check whether the output file at the given *path* should have the file retention policy invoked upon it.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_Interfaces_IRetentionPolicy_InvokePolicy.md">InvokePolicy</a></td><td>
Invoke the file retention policy on the output file at the given *path*.</td></tr></table>&nbsp;
<a href="#iretentionpolicy-interface">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_File_Interfaces.md">NChronicle.File.Interfaces Namespace</a><br />
