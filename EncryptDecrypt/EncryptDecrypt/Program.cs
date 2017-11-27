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
                FileAttributes att = File.GetAttributes(name);

                string Check;
                string[] secretWord = File.ReadAllLines(name);
                 Check = secretWord[secretWord.Length - 1].;

                if ((att & FileAttributes.Encrypted) == FileAttributes.Encrypted && )
                {
                    Console.WriteLine($"\nFile: {name.Remove(0, 5)} already encypted!");
                    goto Exit;
                }

                byte[] buffer = File.ReadAllBytes(name);
                for (int i = 0; i < buffer.Length; i++)
                    buffer[i] = (byte)(buffer[i] ^ SW[i % SW.Length]);

                string bufferText = "";

                for (int i = 0; i < buffer.Length; i++)
                    bufferText += (char)buffer[i];

                File.WriteAllText(path, bufferText);
                File.WriteAllText(name, bufferText + "|" + SW);

                File.SetAttributes(name, att | FileAttributes.Encrypted);

                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Exit:
            Console.Read();
        }
        //---------------------------------------------------------------------
        static FileAttributes EncryptAttribute(FileAttributes attributes, FileAttributes attributesToEncrypt)
        {
            return attributes & ~attributesToEncrypt;
        }
//---------------------------------------------------------------------
        static void Menu(string[] names, int sel)
        {
            FileAttributes att;
            string AttName;
            for (int i = 0; i < names.Length; i++)
            {
                att = File.GetAttributes(names[i]);

                if ((att & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                    AttName = "encrypted";
                else
                    AttName = "normal";

                Console.SetCursorPosition(0, i);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, i);
                if (i == sel)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"{names[i].Remove(0, 5)}\t\t{AttName}");

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
