using book;

Dictionary<string, (string, int)[]> bookSelection = new()
{
    {
        "magyar",
        [
            ("Éva lányom", 2008), //1948
            ("Hitel", 2007), //1830
            ("Magyar Hírmondó", 2010), //1978
            ("Keleti 100 – „Mert szeretek élni…”", 2020),
            ("A zsidók útja", 2012), //1917
            ("Angolország történelme", 2023), //1892
            ("Micimackó - Bölcsességek a Százholdas Pagonyból", 2024),
            ("Apám szerint", 2024),
            ("A Pál utcai fiúk", 2016), //1906
            ("Sorstalanság", 2018), //1975
            ("Az ajtó", 2013), //1987
            ("A gyertyák csonkig égnek", 2009), //1942
            ("Az arany ember", 2021), //1871
            ("Az ember tragédiája", 2022), //1861
            ("Utazás a holdvilágban", 2024), //1937
            ("Emlékiratok könyve", 2011), //1986
        ]
    },
    {
        "angol",
        [
            ("Pride and Prejudice", 2010), //1813
            ("To Kill a Mockingbird", 2007), //1960
            ("1984", 2015), //1949
            ("Moby-Dick", 2014), //1851
            ("The Great Gatsby", 2019), //1925
            ("Brave New World", 2020), //1932
            ("Jane Eyre", 2021), //1847
            ("Wuthering Heights", 2007), //1847
            ("Animal Farm", 2009), //1945
            ("The Catcher in the Rye", 2015), //1951
            ("The Lord of the Rings", 2013), //1954
            ("Harry Potter and the Sorcerer's Stone", 2015), //1997
            ("Fahrenheit 451", 2008), //1953
            ("One Hundred Years of Solitude", 2017), //1970
            ("The Grapes of Wrath", 2021), //1939
        ]
    }
};

Dictionary<string, string[]> authorSelection = new()
{
    { 
        "magyar",
        [
            "Zsolt Ágnes",
            "Heyman Éva",
            "Széchenyi István",
            "Szalay Károly",
            "Matolcsy Ildikó",
            "Sebestyén Lajos",
            "Dávid Sándor", 
            "Dobor Dezső",
            "Ágoston Péter",
            "Lázár Gyula",
            "Náray Tamás",
            "Molnár Ferenc",
            "Kertész Imre",
            "Szabó Magda",
            "Márai Sándor",
            "Jókai Mór",
            "Madách Imre",
            "Szerb Antal",
            "Nádas Péter",
        ]
    },
    {
        "angol",
        [
            "Jane Austen",
            "Harper Lee",
            "George Orwell",
            "Herman Melville",
            "Scott Fitzgerald", 
            "Aldous Huxley",
            "Charlotte Brontë",
            "Emily Brontë",
            "William Shakespeare",
            "J.D. Salinger",
            "J.R.R. Tolkien",
            "J.K. Rowling",
            "Ray Bradbury",
            "Gabriel Garcia Marquez",
            "John Steinbeck",
        ]
    }
};

string[] GetSzerzok(string nyelv)
{
    int szerzoSzam = 1;
    if (Random.Shared.Next(100) >= 70)
    {
        szerzoSzam++;
        if (Random.Shared.Next(2) == 1)
        {
            szerzoSzam++;
        }
    }

    var authors = authorSelection[nyelv];
    string[] szerzok = new string[szerzoSzam];
    for (int j = 0; j < szerzoSzam; j++)
    {
        szerzok[j] = authors[Random.Shared.Next(0, authors.Length)];
    }

    return szerzok;
}

List<Book> books = [];
for (int i = 0; i < 15; i++)
{
    string isbnId;
    do
    {
        isbnId = "";
        for (int j = 0; j < 10; j++)
        {
            isbnId += Random.Shared.Next(0, 10).ToString();
        }
    }
    while (books.Any(b => b.ISBNId == long.Parse(isbnId)));

    int keszlet = 0;
    if (Random.Shared.Next(101) <= 30)
    {
        keszlet = Random.Shared.Next(5, 11);
    }

    string nyelv = "magyar";
    if (Random.Shared.Next(101) >= 80)
    {
        nyelv = "angol";
    }

    (string, int) konyv = bookSelection[nyelv][Random.Shared.Next(0, 16)];

    books.Add(new Book(
        long.Parse(isbnId),
        konyv.Item1,
        konyv.Item2,
        nyelv,
        keszlet,
        Random.Shared.Next(10, 101) * 100,
        GetSzerzok(nyelv))
    );
}


int bevetel = 0;
int elfogyott = 0;
int kiinduloKeszlet = books.Sum(b => b.Keszlet);
for (int i = 0; i < 100; i++)
{
    if (books.Count == 0)
    {
        Console.WriteLine("Elfogytak a könyvek!");
        break;
    }

    Book book = books[Random.Shared.Next(books.Count)];
    if (book.Keszlet > 0)
    {
        book.Keszlet--;
        bevetel += book.Ar;
        //Console.WriteLine($"Eladott köny: {book}");
    }
    else
    {
        if (Random.Shared.Next(101) <= 50)
        {
            book.Keszlet = Random.Shared.Next(10);
            //Console.WriteLine($"Nagyköri kiruccanás: {book}");
        }
        else
        {
            //Console.WriteLine($"Viszlát: {book}");
            elfogyott++;
            books.Remove(book);
        }
    }
}

int jelenlegiKeszlet = books.Sum(b => b.Keszlet);
Console.WriteLine($"Bevétel: {bevetel} Ft");
Console.WriteLine($"{elfogyott} könvy fogyott ki a nagykerben.");
Console.WriteLine("Készlet:");
Console.WriteLine($"\tA kiinduló készlet: {kiinduloKeszlet}");
Console.WriteLine($"\tA jelenlegi készlet: {jelenlegiKeszlet}");
Console.WriteLine($"\tKülönbség: {Math.Abs(jelenlegiKeszlet - kiinduloKeszlet)}");