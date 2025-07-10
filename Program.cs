using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Data.txt");

            if (File.Exists(filePath))
            {
                try
                {
                    QuineMethod f = new QuineMethod(GetFirstLineFromFile(filePath));

                    f.ShowTruthTable();
                    Console.WriteLine();

                    f.ShowNormalForm();
                    Console.WriteLine();
                    f.ShowResultOfGluing();
                    Console.WriteLine();
                    f.ShowImplicantMatrix();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка! : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }    
        }

        public static string GetFirstLineFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("Указанный файл не найден.");
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadLine();
            }
        }
    }
}
