using DocumentFormat.OpenXml.Wordprocessing;
using Gedcom.RecordStructures;
using Microsoft.Win32;

namespace GedcomTests.TestEntities;

public class TestRepository(string xref, string name)
{
    public string Name { get; set; } = name;
    public string Xref { get; set; } = xref;
}

public class TestRepositories
{
    public static List<TestRepository> All
    {
        get
        {
            return [DavisFamilyRecords, GraveyardRecords, HmLandRegistryRepository, 
                IrishNewspapers, VitalRecordsOfIrelandRepository, VitalRecordsRepository, 
                WelshHigherEducationRepository, WelshNewspapers, WelshPoliticalRegistry];
        }
    }

    public static TestRepository DavisFamilyRecords
    {
        get
        {
            return new("@R856120510@", "Davis family records");
        }
    }

    public static TestRepository GraveyardRecords
    {
        get
        {
            return new("@R856111494@", "Graveyard records");
        }
    }

    public static TestRepository HmLandRegistryRepository
    {
        get
        {
            return new("@R856111490@", "HM Land Registry repository");
        }
    }

    public static TestRepository IrishNewspapers
    {
        get
        {
            return new("@R856108552@", "Irish newspapers");
        }
    }

    public static TestRepository VitalRecordsOfIrelandRepository
    {
        get
        {
            return new("@R856108551@", "Vital records of Ireland repository");
        }
    }

    public static TestRepository VitalRecordsRepository
    {
        get
        {
            return new("@R856097590@", "Vital records of Scotland repository");
        }
    }

    public static TestRepository WelshHigherEducationRepository
    {
        get
        {
            return new("@R856111492@", "Welsh higher education repository(Ancestry repo: Name)");
        }
    }

    public static TestRepository WelshNewspapers
    {
        get
        {
            return new("@R856111493@", "Welsh newspapers");
        }
    }

    public static TestRepository WelshPoliticalRegistry
    {
        get
        {
            return new("@R856111495@", "Welsh political registry");
        }
    }
}