Coding patterns

This project uses several very recent C# features. In hindsight, I probably should have
used more familiar features but I'm not going back now. Here are a few patterns you 
may not be familiar with.

# Class constructors

Traditionally, a class would receive values in the constructor and assign them to properties.

e.g. 

public class SourceCitationDto
{
	public string Notes { get; set; }
	public string WhereWithinSource { get; set; }

	public public class SourceCitationDto(SourceCitation sourceCitation)
	{
		Notes = sourceCitation.Notes;
		WhereWithinSource = sourceCitation.WhereWithinSource;
	}
}

With a class construtor, the information normally passed into the constructor is passed
to the class instead. Notice the the parameter SourceCitation was passed to the class itself.

public class SourceCitationDto(SourceCitation sourceCitation) 
{
	public string Notes { get; set; } = sourceCitation.Notes;
	public string WhereWithinSource { get; set; } = sourceCitation.WhereWithinSource;
}

Then the values are assigned after the getters and setters. This eliminates the constructor 
completely. This is used in classes where you don't have to do custom logic. In classes with 
a lot of properties, this is cleaner. 

I do not use class constructors where custom logic must be used to initialize the class.
For example, IndividualDto requires processing the data in the constructor like this:

Events = individualRecord.IndividualEventStructures.Select(ies => new EventDto(ies)).ToList();

#Lazy-loaded properties

All the properties of the RecordStructure classes are lazy loaded using the ??= notation. A
private backing field was created and set to null:

private List<NoteStructure>? _noteStructures.

Then, when the property is accessed the first time, it checks to see if the backing field is 
null. This is always the case on first access. If it is null, it assigns a value to it. The
?? means "Is the left-hand side null?" and the = means "if it's null, assign it the value on 
the right." Here is an example:

public class SourceCitation : RecordStructureBase
{
    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _whereWithinSource = null;
    public string WhereWithinSource => _whereWithinSource ??= GetValue(Tag.Page);
}

#Expression body properties

All properties are accessed using expression body syntax:

public string SomeValue => "some value";

This is used instead of full getter and setters because who doesn't do this? Apparently it's 
uncommon:

public string SomeValue 
{
	get
	{
		return "some value";
	}
}