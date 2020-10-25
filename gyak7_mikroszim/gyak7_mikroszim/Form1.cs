using gyak7_mikroszim.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace gyak7_mikroszim
{
    
    public partial class Form1 : Form
    {
        Random random = new Random(1994);
        List<Person> Nepesseg = new List<Person>();
        List<SzulVal> SzuletesVal = new List<SzulVal>();
        List<HalVal> HalalVal = new List<HalVal>();



        public Form1()
        {
            InitializeComponent();

            Nepesseg = GetNepesseg(@"C:\Temp\nép-teszt.csv");
            SzuletesVal = GetSzulVal(@"C:\Temp\születés.csv");
            HalalVal = GetHalVal(@"C:\Temp\halál.csv");

            dataGridView1.DataSource = HalalVal;

            for (int year = 2005; year <= 2024; year++)
            {
                
                for (int i = 0; i < Nepesseg.Count; i++)
                {
                    // Ide jön a szimulációs lépés
                }

                int nbrOfMales = (from x in Nepesseg
                                  where x.Nem == Gender.Ferfi && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Nepesseg
                                    where x.Nem == Gender.No && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }
        public List<Person> GetNepesseg(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        SzulEv = int.Parse(line[0]),
                        Nem = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        Gyerekszam = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<SzulVal> GetSzulVal(string csvpath)
        {
            List<SzulVal> szulval = new List<SzulVal>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    szulval.Add(new SzulVal()
                    {
                        Kor = int.Parse(line[0]),
                        Gyerekszam = int.Parse(line[1]),
                        Valoszinu = double.Parse(line[2])
                    });
                }
            }

            return szulval;
        }

        public List<HalVal> GetHalVal(string csvpath)
        {
            List<HalVal> halval = new List<HalVal>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    halval.Add(new HalVal()
                    {
                        Nem = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Kor = int.Parse(line[1]),                        
                        Valószinu = double.Parse(line[2])
                    });
                }
            }

            return halval;
        }
    }
}
