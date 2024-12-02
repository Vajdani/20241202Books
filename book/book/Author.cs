namespace book
{
    internal class Author
    {
        private string keresztnev = "";
        private string vezeteknev = "";

        public string Keresztnev {
            get => keresztnev;
            set
            {
                if (value.Length < 3 || value.Length > 32)
                {
                    throw new ArgumentException("Helytelen hosszasságú a megadott keresztnév!");
                }

                keresztnev = value;
            }
        }
        public string Vezeteknev {
            get => vezeteknev;
            set
            {
                if (value.Length < 3 || value.Length > 32)
                {
                    throw new ArgumentException("Helytelen hosszasságú a megadott vezetéknév!");
                }

                vezeteknev = value;
            }
        }
        public Guid Id { get; set; }

        public string TeljesNev => $"{Vezeteknev} {Keresztnev}";

        public Author(string nev)
        {
            string[] split = nev.Split(' ');
            Vezeteknev = split[0];
            Keresztnev = split[1];
            Id = Guid.NewGuid();
        }
    }
}
