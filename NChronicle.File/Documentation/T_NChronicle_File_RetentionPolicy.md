# RetentionPolicy Class
 

The default <a href="T_NChronicle_File_Interfaces_IRetentionPolicy.md">IRetentionPolicy</a> archiving iteratively on file size and age.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;NChronicle.File.RetentionPolicy<br />
**Namespace:**&nbsp;<a href="N_NChronicle_File.md">NChronicle.File</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.3.0 (1.0.3.0)

## Syntax

**C#**<br />
``` C#
public class RetentionPolicy : IRetentionPolicy
```

**VB**<br />
``` VB
Public Class RetentionPolicy
	Implements IRetentionPolicy
```

**F#**<br />
``` F#
type RetentionPolicy =  
    class
        interface IRetentionPolicy
    end
```

<br />
The RetentionPolicy type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_RetentionPolicy__ctor.md">RetentionPolicy</a></td><td>
Create a new RetentionPolicy instance with the default configuration.</td></tr></table>&nbsp;
<a href="#retentionpolicy-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_RetentionPolicy_CheckPolicy.md">CheckPolicy</a></td><td>
Check if the output file at the given *path* is still within the configured limits or is to be archived. It is ready if the file - with the pending bytes to be written - is over the configured file size limit or the file is older than the configured file age limit.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_RetentionPolicy_Configure.md">Configure</a></td><td>
Configure this RetentionPolicy with the specified options.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_RetentionPolicy_InvokePolicy.md">InvokePolicy</a></td><td>
Archive the output file at the given *path*, naming with time the output file was created.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr></table>&nbsp;
<a href="#retentionpolicy-class">Back to Top</a>

## Remarks

This retention policy is based on file size and file age. Depending on the configuration, when the output file grows to the set size limit, or the time since it was created is greater than the set age limit, it will be archived and named with time the output file was created.

If the file name for the archive is already taken it will be appended by a subsequently growing number.

It is also possible to set a retention limit, this limit defines how many of the newest archived logs are kept.

The default configuration is a 100MB file size limit, 1 day file age limit, and a retention limit of 20.


## Examples
Starting an application at 12:35 on December 12th 2016 with a file size limit of 100MB, a file age limit of 30 minutes and an output path of `"chronicle.log"` will result in the output file being archived with the name of `"chronicle.2016.12.01.12.35.log"` at 13:05 or when 100MB of records have been written to it; which ever comes first.

## See Also


#### Reference
<a href="N_NChronicle_File.md">NChronicle.File Namespace</a><br />
