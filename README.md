# Gedcom
A program to manage Gedcom Standard 5.5.1 files.

Gedcom.NET is a tool to explore and transform Gedcom files (.ged). Gedcom files are the default format used 
on genealogy sites like Ancestry and FamilySearch to store genealogy information.

I started this because I wanted to search the data in ged files an unstructured way. I was doing raw text 
searches through a ged file and found some interesting things right away. Then I got an idea for a fun
genealogy website, but I would need the Gedcom data as json. There didn't seem to be anything that did that
so I decided to write a simple Gedcom parser. 

If you are a data structure nerd, you might get a kick out of the Gedcom Standard 5.5.1. They released it in
the 1980s and now that I understand it, I think it was pretty clever. I've integrated the documentation with
the code for easy reference. I can't imagine too many people are going to be interested in this, but for any
that are, I want it to be a tool to learn Gedcom 5.5.1 so that 3rd-party development is consistent with the 
standard.

Here are the goals of Gedcom.NET:

* A tool to explore the raw data in ged files.
* Export to multiple file formats, starting with json.
* Easy-to-use command line tool to query and transform ged files.
* Provide a human-readable abstraction of the standard using a doman-driven code style. 
* Code base is integrated with the Gedcom documentation.
* Readonly for now.

**Gedcom structure**
Gedcom files are quite simple. The most basic data structure is a Gedcom line. A line 
contains a level, a tag, and a value. Here is an example of an INDI (Individual) record:

0 @I***REMOVED***@ INDI
1 NAME John /Doe/
2 GIVN John
2 SURN Doe
2 SOUR @S983746243@
1 SEX M

Each Gedcom line starts with a level, followed by a space, then a tag and value. In a 
well-formed Gedcom file, leading whitespace is not allowed, but I'll be indenting the data 
to make it more readable in this readme. The amount of indentation is determined by the level 
of the line. For example, here is the formatted individual record (INDI):

0 @I***REMOVED***@ INDI
	1 NAME John /Doe/
		2 GIVN John
		2 SURN Doe
	1 SEX M

Notice each line is indented by an amount equal to (Level * indent). 

A Gedcom line with a level of 0 is known as a record. There are seven record tags:
* FAM (Family)
* INDI (Individual)
* OBJE (Multimedia)
* NOTE (Note)
* REPO (Repository)
* SOUR (Source)
* SUBM (Submitter)

Each of these level 0 records has an id known as an xref. Its format is a letter representing
the record type (e.g. Individual xrefs start with an I, Source xrefs start with S), followed by 
a number and then surrounded by @ signs. Here are some examples:
* Source: @S914496133@
* Individual: @I322665662962@
* Repository: @R805769907@

Each record can have any number of sub records, which define its properties. For example, the 
INDI record above has two sub records: NAME and SEX.

0 @I***REMOVED***@ INDI
	1 NAME John /Doe/
	1 SEX M

The NAME substructure then has two sub records of its own: GIVN and SURN.
1 NAME John /Doe/
	2 GIVN John
	2 SURN Doe

This creates a tree structure that can be used to build up any number of arbitrary objects. 
For example, here is a more complicated INDI (Individual record).

0 @I***REMOVED***@ INDI
	1 NAME John /Doe/
		2 GIVN John
		2 SURN Doe
		2 SOUR @S914715036@
	1 SEX M
		2 SOUR @S914079447@
	1 FAMC @F2078@
	1 BIRT
		2 DATE 31 Dec 1999
		2 PLAC San Diego, California, USA

There are five distinct records here.
1. The INDI record itself. 
2. The INDI record has four sub records: NAME, SEX, FAMC, and BIRT.
3. The NAME record has three sub records: GIVN, SURN, and SOUR.
4. The SEX record has one sub record: SOUR.
5. The FAMC record has no sub records.
6. The BIRT record has two sub records: DATE, PLAC

An object-oriented representation of this data might look something like this: 

class INDI {
	NAME Name;
	SEX Sex { get; set; }
	FAMC ChildToFamilyLink { get; set;}
	BIRT Birth { get; set; }
}

class NAME {
	string GIVN; // Given
	string SURN; // Surname
	string SOUR; // Source
}

class SEX {
	string SOUR; // Source
}

class FAMC { 
	// No properties, but still has a value. More on that later.
}

public class BIRT {
	public string DATE;
	public string PLAC; // Place
}

This results in a very weakly-typed data structure. Empty strings, nulls, and undefined properties 
are all common and must be handled correctly.

The next most primitive class in Gedcom.NET is a Record. A record contains any number of parsed 
Gedcom lines. It has three properties: Level, Tag, Value. It also contains a child Record structure 
that contains the Gedcom lines for all of its substructures. Using the previous INDI (Individual) 
record example:

0 @I***REMOVED***@ INDI
	1 NAME John /Doe/
		2 GIVN John
		2 SURN Doe
	1 SEX M

The first line has a level of "0", a tag of "INDI", and a value of "@I***REMOVED***@". It also has 
a Record property that contains NAME and SEX. The second line has a level of 1, a tag of "NAME", 
and a value of "John /Doe/". The third line has a level of 2, a tag name of "GIVN", and a 
value of "John". Here is the Record class: 

class Record {
	int Level { get; }
	string Tag { get; } = "";
	string Value { get; } = "";
	List<GedcomLine> GedcomLines { get; } = []; // The Gedcom lines of this record and all child records.
	List<Record> Records { get; } = []; // A collection of all parsed child records. 	
}

Actually, that's pretty much it. Everything else is tree manipulation, almost all of which is done 
in the RecordBaseStructure base class. It's pretty simple and every other record inherits from it. 
The primary way of querying child records is by searching records based on their tag. The complete 
list of tags are in the C.cs class. Notice I've added a comment after each tag listing the records 
that use it. 

Here are the query functions in the RecordStructureBase class.

class RecordStructureBase
{    
    string _(string tag); // The method "_" finds a child record value by tag name. 
    Record First(string tag);
    List<Record> List(Func<Record, bool> predicate);
    T First<T>(string tag);
    List<T> List<T>(string tag);
}

Here is an example of how these functions are used in the PersonalNameStructure record:

class PersonalNameStructure : RecordStructureBase
{
    string Given => _(C.GIVN);
    string NamePersonal => Record.Value;
    NameVariation NamePhoneticVariation => First<NameVariation>(C.FONE);
    string Surname => _(C.SURN);
}

