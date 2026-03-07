# Gedcom structure

Gedcom stands for **GE**nealogical **D**ata **COM**munication.

It is used by sites and programs like Ancestry, RootsMagic, Family Tree Maker, and others to transfer genealogical data. 
It was first released in 1984 and developed over the next fifteen years. Development mostly stopped by 1999 with version
5.5.1. However, with the rise of genealogy sites over the next twenty years, there was renewed interest in the format.
The latest version is called [FamilySearch GEDCOM Version 7](https://gedcom.io/). However, that version has not been
widely adopted. Because of this, the defacto standard is Gedcom v5.5.1. and that is the version this project targets.

// Compare and constrast Ancestry, RootsMagic, and one other program type

Gedcom files are simply human-readable text files, so you can open them in any text editor. Some might argue about
the human-readable part, but I think if it's presented in the right way, it's easy to understand.

This is what a simple Gedcom file looks like for a single individual:
// Verify that the structure is similar for different programs.

```
0 HEAD
0 @I1@ INDI
1 NAME Sarah /Davis/
2 GIVN Sarah
2 SURN Davis
1 SEX F
0 TRLR
```

Some of the data is obvious, like GIVN for given name and SURN for surname. But it's difficult to see how the data
is structured. If I add more information, such as birth and death, it becomes more obscure. 

```
0 HEAD
0 @I1@ INDI
1 NAME Sarah /Davis/
2 GIVN Sarah
2 SURN Davis
1 SEX F
1 BIRT
2 DATE 8 Aug 1960
2 PLAC Cardiff, Glamorgan, Wales
1 DEAT
2 DATE 8 Aug 2020
2 PLAC Salt Lake City, Salt Lake, Utah, USA
0 TRLR
```

Once again, you'll see familiar information such as BIRT for birth and DEAT for death. Each of these events also 
have two additional pieces of information: DATE for date and PLAC for place. But what does each line mean and how
are they related to each other? To understand that, let's look at one line at a time.

The fundamental data structure in a Gedcom file is the Gedcom line. There are five basic rules that determine
the structure of a line:

1. Leading whitespace is ignored. 
2. There is level. This is an integer from 0-99. The level determines the structure of the data.
3. There is a tag. The tag describes the what type of data the line contains. For example, the INDI tag stands for
   an individual. The GIVN tag stands for a person's given name. The BIRT tag stands for a person's birth. 
4. There is an optional value. This can be data or a pointer to another record. 
5. The max length of a Gedcom line is 255 characters, terminated by \r, \n, or \r\n.

There are other rules, but these are the major ones. I'm going to show how level determines the structure of the
data by breaking the first rule I mentioned, that leading whitespace is ignored. I'm only doing this for the
documentation because it makes it easier to see how the lines are related. 

```
0 HEAD
0 @I1@ INDI
    1 NAME Sarah /Davis/
        2 GIVN Sarah
        2 SURN Davis
    1 SEX F
    1 BIRT
        2 DATE 8 Aug 1960
        2 PLAC Cardiff, Glamorgan, Wales
    1 DEAT
        2 DATE 8 Aug 2020
        2 PLAC Salt Lake City, Salt Lake, Utah, USA
0 TRLR
```

We can now easily see how the lines are related. You can see that ```2 GIVN``` (given) and ```2 SURN``` (surname) are 
properties of ```1 NAME``` and that ```1 NAME``` is a property of ```0 INDI``` (individual).

// Describe how records are logically ordered.

In this Gedcom file, there are examples of three different formats for a line. 

1. On the first line, you see an example of a level + tag: ```0 HEAD```.  ```1 BIRT``` and ```1 DEAT``` also follow this pattern.
1. On the second line, you see an example of a level + Id + tag: ```0 @I1 INDI```. An id is known as a cross reference Id, or xref.
1. On other lines, you'll see an examples of a level + tag + value: ```1 NAME Sarah /Davis/``` or ```2 DATE 8 Aug 1960```.
1. There is a fourth, but I'll go over that when I talk about repositories and sources. 

// Describe what a record is: A line with all its subordinate lines.

Records with a level of 0 are special and follow different rules. That's because they represent the nine fundamental
entities in the Gedcom specification. 

There are five main records: 

1. INDI - Which stands for a single individual. The record will contain all their facts and related sources.
1. FAM - Represents a single family. These records are created when an individual becomes a parent.
1. REPO - Represents a repository.
1. SOUR - Represents a source. This tag is also used for citations, but in a different way.
1. OBJE - Which stands for object. This identifies multimedia objects such as audio, video, or images. 

There is one record commonly used by other records:

1. NOTE - Which stands for note. // Explain 255 character limit.

Then there are three records that mostly contain metadata:

1. HEAD - Marks the start of the Gedcom file.
1. TRLR - Marks the end of the Gedcom file.
1. SUBM - Stands for submitter and contains information about who contributed to the tree.


# Families
Here is the Gedcom for a family containing a father, mother, and daughter. I've left out the birth and death
dates to focus on properties that matter:

```
0 HEAD
0 @I1@ INDI
    1 NAME Sara /Davis/
    1 SEX F
    1 FAMC @F1@
0 @I2@ INDI
    1 NAME Dylan /Davis/
    1 SEX M
    1 FAMS @F1@
0 @I3@ INDI
    1 NAME Fiona /Douglas/
    1 SEX F
    1 FAMS @F1@
0 @F1@ FAM
    1 HUSB @I2@
    1 WIFE @I3@
    1 CHIL @I1@
0 TRLR
```

There are three things to note here. 

First, a new level 0 FAM record has been added: ```0 @F1@ FAM```. This record has three subrecords: HUSB (husband), 
WIFE (wife), and CHIL (child).
// Describe HUSB and WIFE tags
Second, the father (Dylan Davis) and mother (Fiona Douglas) have a new level 1 tag that points to the new FAM record:
```1 FAMS @F1@```. FAMS stands for family spouse and references a FAM (family) record where the person is a spouse.
Third, the daughter (Sarah Davis) has a new level 1 tag that points to the new FAM record: ```1 FAMC @F1@```. FAMC
stands for Family child and means this individual is participating in a family as a child. 

# Repositories and sources






// Limitations
    Proprietary
    Loss of some information like resource links
    Find a complete list of issues

// Assumes the Gedcom file is valid. 
    I can't remember where I read it, but there was a discussion about Gedcom v7 where the developers attempted
    to read Gedcom files from various sources and programs. They wanted to see if they could faithfully reconstruct
    Gedcom files so they could have a uniform format. They eventually gave up because the variation in files
    was so great. One thorny problem is that they couldn't reliably reconstruct the NOTE fields because the 
    CONT (continue) and CONC (concatenate) tags were used so inconsistently that they couldn't be faithfully
    reconstructed.





// Basically my video.


The most basic data structure is a Gedcom line. A line 
contains a level, a tag, and a value. Here is an example of an INDI (Individual) record:

```
0 @I72800176@ INDI
1 NAME John /Doe/
2 GIVN John
2 SURN Doe
2 SOUR @S983746243@
1 SEX M
```

Each Gedcom line starts with a level, followed by a space, then a tag, followed by a space, and an 
optional value. In a well-formed Gedcom file, leading whitespace is not allowed, but I'll be indenting 
the data  to make it more readable. The amount of indentation is determined by the level of the line. 
For example, here is the formatted individual record (INDI):

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
list of tags are in the Constant.cs class. Notice I've added a comment after each tag listing the records 
that use it. 

Here are the query functions in the RecordStructureBase class.

```
class RecordStructureBase
{    
    string GetValue(string tag); // The "_" method finds the value of a child record by tag name. 
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
    string Given => GetValue(C.GIVN);
    string Surname => GetValue(C.SURN);
    string NamePersonal => Record.Value;
    NameVariation NamePhoneticVariation => First<NameVariation>(C.FONE);
}
```

Notice that the properties of the RecordStructureBase classes find a subrecord's value using 
tags. I've named all C# properties after their name in the The Gedcom Standard 5.5.1. For 
example, C.FONE in C.cs has a comment next to it: <NAME_PHONETIC_VARIATION>. I've named the 
property PersonalNameStructure.NamePhoneticVariation after this comment. I think properties 
are named plainly enough that they don't need much more explanation.