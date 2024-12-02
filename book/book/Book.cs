using System;

namespace book
{
    internal class Book
    {
        static string[] Nyelvek = ["magyar", "német", "angol"];

        private long iSBNId;
        private int ar;
        private int keszlet;
        private string nyelv = "";
        private int kiadasiEv;
        private string cim = "";

        public long ISBNId { get => iSBNId; private set => iSBNId = value; }
        public List<Author> Szerzok { get; private set; } = [];
        public string Cim {
            get => cim;
            private set
            {
                if (value.Length < 3 || value.Length > 64)
                {
                    throw new ArgumentException("Nem lehet ilyen hosszú a cím!");
                }

                cim = value;
            }
        }
        public int KiadasiEv {
            get => kiadasiEv;
            private set
            {
                if (value < 2007 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Helytelen év!");
                }

                kiadasiEv = value;
            }
        }
        public string Nyelv {
            get => nyelv;
            private set
            {
                if (!Nyelvek.Contains(value))
                {
                    throw new ArgumentException("Nem lehet ilyen nyelvű a könyv!");
                }

                nyelv = value;
            }
        }
        public int Keszlet {
            get => keszlet;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Nem lehet negatív a készlet!");
                }

                keszlet = value;
            }
        }
        public int Ar {
            get => ar;
            private set
            {
                if (value < 1000 || value > 10000 || value%100 != 0)
                {
                    throw new ArgumentException("Nem megfelő az ár!");
                }

                ar = value;
            }
        }

        public Book(long iSBNId, string cim, int kiadasiEv, string nyelv, int keszlet, int ar, params string[] szerzok)
        {
            ISBNId = iSBNId;
            Cim = cim;
            KiadasiEv = kiadasiEv;
            Nyelv = nyelv;
            Keszlet = keszlet;
            Ar = ar;

            if (szerzok.Length > 3)
            {
                throw new Exception("Túl sok szerzőt adott meg!");
            }

            foreach (string nev in szerzok)
            {
                Szerzok.Add(new Author(nev)); 
            }
        }

        public Book(string cim, string szerzoNev) : this(Random.Shared.Next(0, int.MaxValue), cim, 2024, "magyar", 0, 4500, szerzoNev)
        {

        }

        public override string ToString()
        {
            string szerzok;
            if (Szerzok.Count == 1)
            {
                szerzok = "Szerző: " + Szerzok[0].TeljesNev;
            }
            else
            {
                szerzok = "Szerzők: ";
                for (int i = 0; i < Szerzok.Count; i++)
                {
                    szerzok += $"{Szerzok[i].TeljesNev}{(i == Szerzok.Count - 1 ? "" : "; ")}";
                }
            }

            string keszlet;
            if (Keszlet == 0)
            {
                keszlet = "Beszerzés alatt";
            }
            else
            {
                keszlet = $"Készlet: {Keszlet} db";
            }

            return $"{Cim} ({KiadasiEv}) {szerzok} - {keszlet}";
        }
    }
}
