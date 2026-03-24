namespace GedcomTests.TestEntities;

public class TestSource(string xref, string title, string repoXref)
{
    public string Xref { get; set; } = xref;
    public string RepoXref { get; set; } = repoXref;
    public string Title { get; set; } = title;
}

public class TestSources
{
    public static List<TestSource> All
    {
        get
        {
            return [AberystwythUniversity, DeathAnnouncements, DivorceRecordsOfIreland, DylanDavisBiography,
                    MarriageRecordsOfIreland, MarriageRecordsOfScotland, WeddingAnnouncements,
                    WelshCityRecords, WelshGraveyards, WelshTitleDeedsSource];
        }
    }

    public static TestSource AberystwythUniversity
    {
        get
        {
            return new("@S977151981@", "Aberystwyth University (Ancestry source: Title)", "@R856111492@");
        }
    }

    public static TestSource DeathAnnouncements
    {
        get
        {
            return new("@S977151991@", "Death announcements", "@R856111493@");
        }
    }

    public static TestSource DivorceRecordsOfIreland
    {
        get
        {
            return new("@S977046024@", "Divorce records of Ireland", "@R856108551@");
        }
    }

    public static TestSource DylanDavisBiography
    {
        get
        {
            return new("@S977470769@", "Dylan Davis biography", "@R856120510@");
        }
    }

    public static TestSource MarriageRecordsOfIreland
    {
        get
        {
            return new("@S977046020@", "Marriage records of Ireland", "@R856108551@");
        }
    }

    public static TestSource MarriageRecordsOfScotland
    {
        get
        {


            return new("@S976697667@", "Marriage records of Scotland", "@R856097590@");
        }
    }

    public static TestSource WeddingAnnouncements
    {
        get
        {
            return new("@S977046068@", "Wedding announcements", "@R856108552@");
        }
    }

    public static TestSource WelshCityRecords
    {
        get
        {
            return new("@S977152012@", "Welsh city records", "@R856111495@");
        }
    }

    public static TestSource WelshGraveyards
    {
        get
        {
            return new("@S977152005@", "Welsh graveyards", "@R856111494@");
        }
    }

    public static TestSource WelshTitleDeedsSource
    {
        get
        {
            return new("@S977151969@", "Welsh title deeds source", "@R856111490@");
        }
    }
}

