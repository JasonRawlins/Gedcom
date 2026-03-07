Record structures

The record structure classes are the fundamental features of the project. They have a specific structure.
All of these classes inherit from a RecordStructureBase class. This class has helper methods that are
used to query their sub records. 

Examples: First(string tag), Single(string tag), List(string tag), etc. 


The files have four sections.

The first is a class that mirrors the specification exactly. All properties are named exactly as they are
in the specification. For example, the IndividualRecord class has property names like "IndividualEventStructures" 
or "PersonalNameStructures". These have been used instead of more friendly names like "Events" and "Names" 
so you can find their definitions in the specification. 

The second is a class that handles json serialization. It is named after the relevant record structure. For 
example: IndividualJsonConverter.

The third is a class that represents a data transfer object (DTO). This is the class a consuming system
would expect. It returns a representation that has friendly names. For example, "Events" instead of
"IndividualEventStructures". 

The fourth section is the text from the Gedcom specification along with page numbers. This allows you to 
see exactly what the class represents. For example:

INDIVIDUAL_RECORD:=

n @XREF:INDI@ INDI {1:1}
    +1 RESN <RESTRICTION_NOTICE> {0:1} p.60
    +1 <<PERSONAL_NAME_STRUCTURE>> {0:M} p.38
    +1 SEX <SEX_VALUE> {0:1} p.61

Notice that all record structures are queried using tags. These tags are defined in the specification
and enumerated in the Tag.cs file. Each property in this class has the following format
1. A friendly name used throughout the code (e.g. Tag.BaptismLds)
1. The tag value (e.g. BAPL)
1. The formal name of the tag in the specification (e.g. {Baptism-LDS})
1. A list of all records that use that tag (e.g. NAME is used in <NAME_PERSONAL>, <SUBMITTER_NAME>, <NAME_OF_REPOSITORY>, <NAME_OF_PRODUCT>)

Vendors can define custom tags used only by their system. These custom tags are prepended with an
underscore. They are defined in ExtensionTag.cs. Some of these are well-know, such as the custom
tags used by Ancestry (e.g. _DSCR, _ELEC, _OID)