[Parsing](Docs/Parsing.md)

# Gedcom.NET
Gedcom.NET is a tool to query and transform Gedcom Standard 5.5.1 files. Gedcom files are the default
format used by genealogy sites and programs like Ancestry, FamilySearch, RootsMagic, Family Tree Maker, and others 
to transfer genealogy information.

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



