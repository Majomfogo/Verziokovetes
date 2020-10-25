using gyak7_mikroszim.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        List<int> ferfiszam = new List<int>();
        List<int> noszam = new List<int>();


        public Form1()
        {
            InitializeComponent();

            
            
        }

        private void Simulation()
        {
            Nepesseg = GetNepesseg(textBox1.Text);
            SzuletesVal = GetSzulVal(@"C:\Temp\születés.csv");
            HalalVal = GetHalVal(@"C:\Temp\halál.csv");
            ferfiszam.Clear();
            noszam.Clear();
            richTextBox1.Clear();
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {

                for (int i = 0; i < Nepesseg.Count; i++)
                {
                    SimStep(year, Nepesseg[i]);
                }

                int nbrOfMales = (from x in Nepesseg
                                  where x.Nem == Gender.Ferfi && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Nepesseg
                                    where x.Nem == Gender.No && x.IsAlive
                                    select x).Count();

                ferfiszam.Add(nbrOfMales);
                noszam.Add(nbrOfFemales);
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
                
            }
            DisplayResults();
        }

        public void DisplayResults()
          
        {
            string sor1;
            string sor2;
            string sor3;
            int szamolo = 0;
            for (int i = 2005; i < numericUpDown1.Value; i++)
            {
                sor1 = "Szimulációs év: " + i.ToString();
                sor2 = "Fiúk: " + ferfiszam[szamolo].ToString();
                sor3 = "Lányok" + noszam[szamolo].ToString();

                richTextBox1.AppendText(sor1 + "\n\t" + sor2 + "\n\t" + sor3 + "\n\n");
                szamolo++;
                
               
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

        private void SimStep(int year, Person person)
        {
            
            if (!person.IsAlive) return;

            
            byte age = (byte)(year - person.SzulEv);

            
            double pDeath = (from x in HalalVal
                             where x.Nem == person.Nem && x.Kor == age
                             select x.Valószinu).FirstOrDefault();
            
            if (random.NextDouble() <= pDeath)
                person.IsAlive = false;

            
            if (person.IsAlive && person.Nem == Gender.No)
            {
                
                double pBirth = (from x in SzuletesVal
                                 where x.Kor == age
                                 select x.Valoszinu).FirstOrDefault();
                
                if (random.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.SzulEv = year;
                    újszülött.Gyerekszam = 0;
                    újszülött.Nem = (Gender)(random.Next(1, 3));
                    Nepesseg.Add(újszülött);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            
                textBox1.Text = ofd.FileName;
            
            
        }
    }
}
