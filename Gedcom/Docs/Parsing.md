# Parsing test

The final result of parsing is a tree. Populating the tree is achieved using two steps. 
In the first step, each line of the Gedcom file is parsed as a GedcomLine. Here 
is the definition of that class:

```
public class GedcomLine
{
    public int Level { get; set; } = -1;
    public string Tag { get; set; } = Constants.Empty;
    public string Value { get; set; } = "";
    public string Xref => Value;

    public static GedcomLine Parse(string line) {}
}
```

Afterwards, a GedcomDocument object is created. A GedcomDocument collects the GedcomLines
into a tree structure which is a List<List<GedcomLine>>. 


into a List<List<GedcomLines>>. 