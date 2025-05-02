# Gedcom
A program to manipulate Gedcom Version 5.5.1 files.


This program parses a .ged file and then allows various operations, such as converting the .ged to JSON and other custom actions.

The most basic data structure in a .ged file is a Gedcom line. A line contains a level, a tag, and a value. Here is an example of an INDI (Individual) record:

0 @I***REMOVED***@ INDI
1 NAME John /Doe/
2 GIVN John
2 SURN Doe
2 SOUR @S983746243@
1 SEX M

Each Gedcom line starts with a level, followed by a space, then a tag and value. In a well-formed Gedcom file, leading whitespace is not allowed, but I'll be indenting the data to make it more readable in this readme. The amount of indentation is determined by the level of the line. For example, here is the formatted individual record (INDI):

0 @I***REMOVED***@ INDI
	1 NAME John /Doe/
		2 GIVN John
		2 SURN Doe
	1 SEX M

Notice each line is indented by an amount equal to (Level * tab). 

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

Each record can have any number of sub records, which define the properties of the record. For 
example, the INDI record above has two child properties: NAME and SEX.

0 @I***REMOVED***@ INDI
	1 NAME John /Doe/
	1 SEX M

The NAME substructure then has two child properties of its own: GIVN and SURN.
1 NAME John /Doe/
	2 GIVN John
	2 SURN Doe

This creates a tree-like structure that can be used to build up any number of arbitrary objects. 
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
2. The INDI record has four properties: NAME, SEX, FAMC, and BIRT.
3. The NAME record has three properties: GIVN, SURN, and SOUR.
4. The SEX record has one property: SOUR.
5. The FAMC record has no properties.
6. The BIRT record has two properties: DATE, PLAC

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

The Gedcom data format allows for any number of arbitrary records to be created like this. This 
results in a very weakly-typed data structure. Empty strings, nulls, and undefined properties are 
all common and must be handled correctly.

The next most primitive class in Gedcom.NET is a Record. A record contains any number of parsed 
Gedcom lines. It has three properties: Level, Tag, Value. It also contains a child Record structure 
that contains the Gedcom lines for all of its substructures. Using the previous INDI (Individual) 
record example. 

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

