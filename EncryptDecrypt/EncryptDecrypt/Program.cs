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
        //static string file = @"File/Test.txt";
        static string file = @"File";

        static void EncryptDecrypt(string SW, string name)
        {
            try
            {
                FileAttributes att = File.GetAttributes(name);
                bool encrypt = false;


                if ((att & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                {
                    string[] secretWord = File.ReadAllLines(name);
                    int StartIndexOfSecretWord = secretWord[secretWord.Length - 1].IndexOf('|');
                    string Check = secretWord[secretWord.Length - 1].Substring(StartIndexOfSecretWord);

                    encrypt = true;

                    if (Check == SW)
                    {
                        Console.WriteLine($"\nFile: {name.Remove(0, 5)} already encypted!");
                        goto Exit;
                    }
                }

                byte[] buffer = File.ReadAllBytes(name);
                for (int i = 0; i < buffer.Length; i++)
                    buffer[i] = (byte)(buffer[i] ^ SW[i % SW.Length]);

                string bufferText = "";

                for (int i = 0; i < buffer.Length; i++)
                    bufferText += (char)buffer[i];

                if (encrypt)
                {

                }
                else
                {
                    File.WriteAllText(name, bufferText + "|" + SW);
                    File.SetAttributes(name, att | FileAttributes.Encrypted);
                }


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
        static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
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

                Console.WriteLine($"{names[i].Remove(0, 5)}\t{AttName}");

            }
        }
//---------------------------------------------------------------------
        static void Main(string[] args)
        {
            if (!File.Exists(file))
                Directory.CreateDirectory(file);

            Console.CursorVisible = false;

            string[] names = Directory.GetFiles(file);

            int select = 0;

            while (true)
            {
                Menu(names, select);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (select < names.Length - 1)
                            select++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (select > 0)
                            select--;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("Enter a secret word:\t\t");
                        string SW = Console.ReadLine();
                        EncryptDecrypt(SW, names[select]);
                        Console.Clear();
                        Console.CursorVisible = false;
                        break;
                }
            }
        }
    }
}
//---------------------------------------------------------------------