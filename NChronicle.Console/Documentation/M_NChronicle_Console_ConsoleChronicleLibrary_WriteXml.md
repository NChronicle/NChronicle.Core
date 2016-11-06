# ConsoleChronicleLibrary.WriteXml Method 
 

Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.

**Namespace:**&nbsp;<a href="N_NChronicle_Console.md">NChronicle.Console</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void WriteXml(
	XmlWriter writer
)
```

**VB**<br />
``` VB
Public Sub WriteXml ( 
	writer As XmlWriter
)
```

**F#**<br />
``` F#
abstract WriteXml : 
        writer : XmlWriter -> unit 
override WriteXml : 
        writer : XmlWriter -> unit 
```


#### Parameters
&nbsp;<dl><dt>writer</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">System.Xml.XmlWriter</a><br /><a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a> stream to the configuration file.</dd></dl>

#### Implements
<a href="http://msdn2.microsoft.com/en-us/library/9yt8e1yw" target="_blank">IXmlSerializable.WriteXml(XmlWriter)</a><br />

## See Also


#### Reference
<a href="T_NChronicle_Console_ConsoleChronicleLibrary.md">ConsoleChronicleLibrary Class</a><br /><a href="N_NChronicle_Console.md">NChronicle.Console Namespace</a><br />NChronicle.SaveConfigurationTo(String)<br />
