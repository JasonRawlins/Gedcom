# Gedcom.NET
Gedcom.NET is a tool to query and transform Gedcom Standard 5.5.1 files (ged). Gedcom files are the default
format used on genealogy sites like Ancestry and FamilySearch to store genealogy information.

I started this because I wanted to search the data in ged files in an unstructured way. I was doing raw text 
searches through a ged file and found some interesting things right away. Then I got an idea for a fun
genealogy website, but I would need the Gedcom data as json. There didn't seem to be anything that did that. 
I didn't look too hard, actually, I just wanted an excuse to write a simple Gedcom parser. This tool expects a 
well-formed ged file and I have only tested it on trees exported from Ancestry. 

If you are a data structure nerd, you might get a kick out of the Gedcom Standard 5.5.1. They released it in
the 1980s and now that I understand it, I think it was pretty clever. I've integrated the documentation with
the code for easy reference. I can't imagine too many people are going to be interested in this, but for any
that are, I want it to be a tool to learn Gedcom 5.5.1 so that 3rd-party development is consistent with the 
standard.

# Goals of Gedcom.NET

* A tool to explore the raw data in gedcom files.
* Export to multiple file formats, starting with json.
* Easy-to-use command line tool to query and transform ged files.
* Provide a human-readable abstraction of the standard in the code.
* A code base that is integrated with the Gedcom documentation.
* Readonly for now.

## Gedcom structure
Gedcom files are quite simple. The most basic data structure is a Gedcom line. A line 
contains a level, a tag, and a value. Here is an example of an INDI (Individual) record:

```
0 @I72800176@ INDI
1 NAME John /Doe/
2 GIVN John
2 SURN Doe
2 SOUR @S983746243@
1 SEX M
```

Each Gedcom line starts with a level, followed by a space, then a tag and value. In a 
well-formed Gedcom file, leading whitespace is not allowed, but I'll be indenting the data 
to make it more readable. The amount of indentation is determined by the level 
of the line. For example, here is the formatted individual record (INDI):

```
0 @I72800176@ INDI
    1 NAME John /Doe/
        2 GIVN John
        2 SURN Doe
    1 SEX M
```

Notice each line is indented by an amount equal to (Level * indent). 

## Level 0 records
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
* Source: @S74323453@
* Individual: @I93847628@
* Repository: @R99271728@

Each record can have any number of subrecords, which define its properties. For example, the 
INDI record above has two subrecords: NAME and SEX.

```
0 @I72800176@ INDI
    1 NAME John /Doe/
    1 SEX M
```

The NAME substructure then has two subrecords of its own: GIVN and SURN.
```
1 NAME John /Doe/
    2 GIVN John
    2 SURN Doe
```

This creates a tree structure that can be used to build up any number of arbitrary objects. 
For example, here is a more complicated INDI (Individual record).

```
0 @I72800176@ INDI
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
```

There are several records here:
1. The INDI record itself. 
2. The INDI record has four subrecords: NAME, SEX, FAMC, and BIRT.
3. The NAME record has three subrecords: GIVN, SURN, and SOUR.
4. The SEX record has one subrecord: SOUR.
5. The FAMC record has no subrecords. 
6. The BIRT record has two subrecords: DATE, PLAC

An object-oriented representation of this data might look something like this: 

```
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
```

This results in a very weakly-typed data structure. Empty strings, nulls, and undefined properties 
are all common and must be handled.

## Gedcom Record 
The next most primitive class in Gedcom.NET is a Record. A record contains any number of parsed 
Gedcom lines. It has three properties: Level, Tag, Value. It also contains a list of child records 
that contains the Gedcom lines for all of its substructures. Using the previous INDI (Individual) 
record example:

```
0 @I72800176@ INDI
    1 NAME John /Doe/
        2 GIVN John
        2 SURN Doe
    1 SEX M
```

The first line has a level of "0", a tag of "INDI", and a value of "@I72800176@". It also has 
a Record property that contains NAME and SEX. The second line has a level of 1, a tag of "NAME", 
and a value of "John /Doe/", etc., Here is the Record class: 

```
class Record {
    int Level;
    string Tag;
    string Value;
    List<Record> Records;
}
```

Actually, that's pretty much it. Everything else is tree manipulation, almost all of which is done 
in the RecordBaseStructure base class. It's pretty simple and every other record inherits from it. 
The primary way of querying child records is by searching records based on their tag. The complete 
list of tags are in the C.cs class. Notice I've added a comment after each tag listing the records 
that use it. 

Here are the query functions in the RecordStructureBase class.

```
class RecordStructureBase
{    
    string _(string tag); // The "_" method finds the value of a child record by tag name. 
    Record First(string tag);
    List<Record> List(Func<Record, bool> predicate);
    T First<T>(string tag);
    List<T> List<T>(string tag);
}
```

Here is an example of how these functions are used in the PersonalNameStructure subrecord:

```
class PersonalNameStructure : RecordStructureBase
{
    string Given => _(C.GIVN);
    string Surname => _(C.SURN);
    string NamePersonal => Record.Value;
    NameVariation NamePhoneticVariation => First<NameVariation>(C.FONE);
}
```

Notice that the properties of the RecordStructureBase classes find a subrecord's value using 
tags. I've named all C# properties after their name in the The Gedcom Standard 5.1.1. For 
example, C.FONE in C.cs has a comment next to it: <NAME_PHONETIC_VARIATION>. I've named the 
property PersonalNameStructure.NamePhoneticVariation after this comment. I think properties 
are named plainly enough that they don't need much more explanation.

**Command line reference**
