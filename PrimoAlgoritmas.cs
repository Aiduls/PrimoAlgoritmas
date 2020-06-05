using System;
using System.IO;

namespace PrimoAlgoritmoLaboratorinis
{
    class PrimoAlgoritmas
    {
        const int nr = 25;
        static void Main(string[] args)
        {
            const int v = 10;
            const int start = nr % 10; // nr (nuo 0) 25, tad viršūnė - 25 % 10 = 5
            int[,] matrica = new int[v, v] {/*      0    1    2    3    4    5    6    7    8     9   */
                                            /*0*/ { 0,   8,   53,  55,  60,  88,  58,  49,  30,  3  },
                                            /*1*/ { 8,   0,   76,  22,  89,  38,  48,  48,  82,  34 },
                                            /*2*/ { 53,  76,  0,   97,  7,   80,  50,  45,  93,  77 },
                                            /*3*/ { 55,  22,  97,  0,   11,  98,  22,  7,   62,  9  },
                                            /*4*/ { 60,  89,  7,   11,  0,   21,  19,  63,  50,  71 },
                                            /*5*/ { 88,  38,  80,  98,  21,  0,   48,  57,  16,  87 }, // mano
                                            /*6*/ { 58,  48,  50,  22,  19,  48,  0,   44,  39,  73 },
                                            /*7*/ { 49,  48,  45,  7,   63,  57,  44,  0,   5,   25 },
                                            /*8*/ { 30,  82,  93,  62,  50,  16,  39,  5,   0,   3  },
                                            /*9*/ { 3,   34,  77,  9,   71,  87,  73,  25,  3,   0  } };

            surastiMinSvori(matrica,v, start);
        }

        static void surastiMinSvori(int[,] matrica, int v, int start)
        {
            int[] aplankytosVirsunes = new int[v];
            string[] aplankytosBriaunos = new string[v-1];
            int[] svoriai = new int[v-1];
            int svoris;
            int pradineV = start;

            for (int i=0, counter=1; i<v-1; i++, counter++) // ciklas kiekvienai virsunei
            {
                svoris = int.MaxValue;
                for (int j = 0; j < v; j++) // ciklas patikrinti visoms kiekvienos virsunes briaunoms
                {
                    if (matrica[start, j]<svoris && matrica[start, j] != 0 && !arAplankyta(j, aplankytosVirsunes, i))
                    {
                        svoris = matrica[start, j];
                        aplankytosVirsunes[counter] = j;
                        aplankytosBriaunos[i] = start.ToString() + j.ToString();
                    }
                }
                svoriai[i] = svoris;
                aplankytosVirsunes[i] = start;
                start = aplankytosVirsunes[counter];
            }

            spausdinti(pradineV, aplankytosBriaunos, gautiSvori(svoriai));
        }

        static bool arAplankyta(int virsune, int[] aplankytos, int kiekis)
        {
            for (int i=0;i<kiekis;i++)
            {
                if (aplankytos[i] == virsune) return true;
            }
            return false;
        }

        static int gautiSvori(int[] svoriai)
        {
            int svoris = 0;
            foreach (int el in svoriai)
            {
                svoris += el;
            }

            return svoris;
        }
        static void spausdinti(int pradineV, string[] aplankytosBriaunos, int svoris)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string failas = Path.Combine(projectDirectory, "Aido_Bulako_laboratorinis.txt");

            if (File.Exists(failas))
            {
                File.Delete(failas);
            }

            // Sukuria ir rašo į failą
            using (StreamWriter sw = File.CreateText(failas))
            {
                sw.WriteLine(nr + " " + pradineV);

                foreach (string briauna in aplankytosBriaunos)
                {
                    sw.Write(briauna + " ");
                }
                sw.WriteLine();

                sw.WriteLine(svoris);
            }
        }
    }
}
