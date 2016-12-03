# ConsoleChronicleLibraryConfiguration.ReadXml Method 
 

Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.

**Namespace:**&nbsp;<a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.2.0 (1.0.2.0)

## Syntax

**C#**<br />
``` C#
public void ReadXml(
	XmlReader reader
)
```

**VB**<br />
``` VB
Public Sub ReadXml ( 
	reader As XmlReader
)
```

**F#**<br />
``` F#
abstract ReadXml : 
        reader : XmlReader -> unit 
override ReadXml : 
        reader : XmlReader -> unit 
```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">System.Xml.XmlReader</a><br /><a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a> stream from the configuration file.</dd></dl>

#### Implements
<a href="http://msdn2.microsoft.com/en-us/library/w6txf8t9" target="_blank">IXmlSerializable.ReadXml(XmlReader)</a><br />

## See Also


#### Reference
<a href="T_NChronicle_Console_Configuration_ConsoleChronicleLibraryConfiguration.md">ConsoleChronicleLibraryConfiguration Class</a><br /><a href="N_NChronicle_Console_Configuration.md">NChronicle.Console.Configuration Namespace</a><br />NChronicle.ConfigureFrom(String, Boolean, Int32)<br />
