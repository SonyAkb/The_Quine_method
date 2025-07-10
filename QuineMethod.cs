using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_3
{
    public class QuineMethod
    {
        char x = 'x';
        char y = 'y';
        char z = 'z';
        char w = 'w';

        int[,] truthTable = FillingTruthTable();//таблица истинности
        int[,] implicantMatrix;//импликантная матрица

        List<string> PDNF;//Совершенная дизъюнктивная нормальная форма
        List<string> resultOfGluing;//результат слияния
        List<string> tmp;

        public char X
        {
            get { return x; }
            set { x = value.ToString().ToLower().ToCharArray()[0]; }
        }

        public char Y
        {
            get { return y; }
            set { y = value.ToString().ToLower().ToCharArray()[0]; }
        }

        public char Z
        {
            get { return z; }
            set { z = value.ToString().ToLower().ToCharArray()[0]; }
        }

        public char W
        {
            get { return w; }
            set { w = value.ToString().ToLower().ToCharArray()[0]; }
        }


        public QuineMethod(string vector)
        {
            if (vector == null || vector.Length != 16 || !IsCorrectVector(vector))
            {
                throw new ArgumentNullException("Вектор некорректен");
            }

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                truthTable[i, 4] = Convert.ToInt32(vector[i] + "");
            }
            PDNF = СreaturePDNF();
            resultOfGluing = new List<string>();

            List<string> tmpPDNF = new List<string>(PDNF);
            Gluing(tmpPDNF);


            implicantMatrix = FillingInImplicantMatrix();
        }

        public void ShowTruthTable()
        {
            Console.WriteLine($"{X} | {Y} | {Z} | {W} | F |");
            Separate(19);

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                for (int j = 0; j < truthTable.GetLength(1); j++)
                {
                    Console.Write(truthTable[i, j] + " | ");
                }
                Console.WriteLine();
            }
        }

        public void ShowNormalForm()
        {
            if (PDNF.Count > 0)
            {
                Console.WriteLine("СДНФ");
                Console.Write($"F({X}, {Y}, {Z}, {W}) = {PDNF[0]}");
                for (int i = 1; i < PDNF.Count; i++)
                {
                    Console.Write($" + {PDNF[i]}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Функция при любых значениях ложна!");
            }
        }

        public void ShowResultOfGluing()
        {
            if (resultOfGluing.Count == 0)
            {
                return;
            }

            Console.WriteLine("Результат склеивания");
            Console.Write($"F({X}, {Y}, {Z}, {W}) = {resultOfGluing[0]}");
            for (int i = 1; i < resultOfGluing.Count; i++)
            {
                Console.Write($" + {resultOfGluing[i]}");
            }
            Console.WriteLine();
        }

        public void ShowImplicantMatrix()
        {
            if (PDNF.Count == 0)
            {
                Console.WriteLine("Невозможно составить импликантную матрицу\n");
                return;
            }
            Console.Write("".PadLeft(5, ' '));
            for (int i = 0; i < PDNF.Count; i++)
            {
                Console.Write(" |" + PDNF[i].PadLeft(5, ' '));
            }
            Console.WriteLine();
            for (int i = 0; i < implicantMatrix.GetLength(0); i++)
            {
                if (resultOfGluing.Count != 0)
                {
                    Console.Write(resultOfGluing[i].PadLeft(5, ' '));
                }

                for (int j = 0; j < implicantMatrix.GetLength(1); j++)
                {
                    if (implicantMatrix[i, j] == 1)
                    {
                        Console.Write(" |" + "+".PadLeft(5, ' '));
                    }
                    else
                    {
                        Console.Write(" |" + "".PadLeft(5, ' '));
                    }
                }
                Console.WriteLine();
            }
        }

        private List<string> СreaturePDNF()
        {
            List<string> PDNF = new List<string>();
            int[] tmpTable;
            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                if (truthTable[i, 4] == 1)
                {
                    tmpTable = new int[] { truthTable[i, 0], truthTable[i, 1], truthTable[i, 2], truthTable[i, 3] };
                    PDNF.Add(Forma(tmpTable));
                }
            }
            return PDNF;
        }

        private void AddIfNotUsed(ref bool[] useOrNot, ref List<string> tmpPDNF)
        {
            for (int i = 0; i < useOrNot.Length; i++)
            {
                if (!useOrNot[i])
                {
                    resultOfGluing.Add(tmpPDNF[i]);
                }
            }
        }
        public void qwe(List<string> tmpPDNF)
        {
            for(int i = 0;i < tmpPDNF.Count; i++)
            {
                Console.Write(tmpPDNF[i] + " + ");
            }
            Console.WriteLine();
        }

        private bool Gluing(List<string> tmpPDNF)
        {
            if (tmpPDNF.Count == 0) 
            {
                resultOfGluing = resultOfGluing.Distinct().ToList();
                return true; 
            }

            string gluingString = "";
            bool[] useOrNot = new bool[tmpPDNF.Count];

            List<string> newTmpPDNF = new List<string>();
            for (int i = 0; i < tmpPDNF.Count; i++) // строка
            {
                for (int j = i + 1; j < tmpPDNF.Count; j++) // подстрока
                {
                    gluingString = IsSubstringInString(tmpPDNF[j], tmpPDNF[i]);
                    if (tmpPDNF[i].Length - gluingString.Length == 1 && tmpPDNF[i].Length != 1)
                    {
                        useOrNot[i] = true;
                        useOrNot[j] = true;

                        newTmpPDNF.Add(gluingString);
                    }
                }
            }
            
            AddIfNotUsed(ref useOrNot, ref tmpPDNF);

            newTmpPDNF = new List<string> (newTmpPDNF.Distinct().ToList());
            
            return Gluing(newTmpPDNF); // Рекурсивный вызов для нового списка
        }

        public static string IsSubstringInString(string substr, string str, bool allCharactersflag = true)
        {
            if (!LinesWithoutCase(substr, str) && allCharactersflag)
            {
                return "";
            }

            string answer = "";
            foreach (char c in substr)
            {
                if (IsSymbolInString(c, ref str))
                {
                    answer += c;
                }
            }
            return answer;
        }

        public static bool LinesWithoutCase(string str1, string str2)
        {
            if (str1.ToLower() == str2.ToLower())
            {
                return true;
            }
            return false;
        }

        public static int[,] FillingTruthTable()
        {
            int[,] truthTable = new int[16, 5];

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                string binaryStr = Convert.ToString(i, 2).PadLeft(4, '0');

                for (int j = 0; j < truthTable.GetLength(1) - 1; j++)
                {
                    truthTable[i, j] = binaryStr[j] - '0';
                }
            }
            return truthTable;
        }

        private int[,] FillingInImplicantMatrix()
        {
            if(resultOfGluing.Count == 1)
            {
                int[,] newImplicantMatrix2 = new int[resultOfGluing.Count, PDNF.Count];
                for (int i = 0; i < resultOfGluing.Count; i++)
                {
                    for (int j = 0; j < PDNF.Count; j++)
                    {
                        if (IsSubstringInString(resultOfGluing[i], PDNF[j], false).Length == resultOfGluing[i].Length)
                        {
                            newImplicantMatrix2[i, j] = 1;
                        }
                    }
                }


                return newImplicantMatrix2;
            }


            int[,] newImplicantMatrix = new int[resultOfGluing.Count, PDNF.Count];
            for (int i = 0; i < resultOfGluing.Count; i++)
            {
                for (int j = 0; j < PDNF.Count; j++)
                {
                    if (IsSubstringInString(resultOfGluing[i], PDNF[j], false).Length == resultOfGluing[i].Length)
                    {
                        newImplicantMatrix[i, j] = 1;
                    }
                }
            }


            return newImplicantMatrix;
        }

        public static bool IsCorrectVector(string vector)
        {
            foreach (char c in vector)
            {
                if (c != '1' && c != '0')
                {
                    return false;
                }
            }
            return true;
        }

        private string Forma(int[] boolValue)
        {
            string referenceValues = $"{X}{Y}{Z}{W}" ;
            string answer = "";

            for (int i = 0; i < boolValue.GetLength(0); i++)
            {
                if(boolValue[i] == 0)
                {
                    answer += (referenceValues[i] + "").ToUpper();   
                }
                else
                {
                    answer += referenceValues[i];
                }
            }

            return answer;
        }

        public static bool IsSymbolInString(char symbol, ref string str)
        {
            foreach(char c in str)
            {
                if(c == symbol)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Separate(char symbol, int length, char intervalSymbol = ' ', int interval = 1, bool flagNeedAnotherSymbol  = false)
        {
            for(int i = 0; i < length ; i++)
            {
                if(flagNeedAnotherSymbol && i!=0 && i != length - 1 && i % interval == 0)
                {
                    Console.Write(intervalSymbol);
                }
                else
                {
                    Console.Write(symbol);
                }
            }
            Console.WriteLine();
        }

        public static void Separate(int num, string symbol = "-")
        {
            for (byte i = 0; i < num; i++) { Console.Write(symbol); }
            Console.WriteLine();
        }
    }
}
