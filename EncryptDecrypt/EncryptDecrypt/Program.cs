using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------------------------------------------------
namespace EncryptDecrypt
{
    class Program
    {
        static string path = "Test.txt";

        static void EncryptDecrypt(string SW)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(path);
                for (int i = 0; i < buffer.Length; i++)
                    buffer[i] = (byte)(buffer[i] ^ SW[i % SW.Length]);

                string bufferText = "";

                for (int i = 0; i < buffer.Length; i++)
                    bufferText += (char)buffer[i];

                File.WriteAllText(path, bufferText);
                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
//---------------------------------------------------------------------
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter the path: ");
            //string path = Console.ReadLine();

            Console.Write("Enter a secret word:\t\t");
            string SW = Console.ReadLine();
            EncryptDecrypt(SW);
        }
    }
}
//---------------------------------------------------------------------