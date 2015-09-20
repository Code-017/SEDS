using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

//SEDS V1.0 source file
//by Code 017
//Lisense: GNU Public Lisense V3

namespace SEDS
{
    class SEDS
    {
         static void Main(string[] args)
        {
            Console.Title = "Simple Encrypt and Decrypt System V3.0";

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Simple Encrypt and Decrypt System V3.0(SEDS)\nby Code017 copyright\n1.XOR Encrypt\n2.XOR Decrypt\n3.MD5Encrypt\n4.SHA1Encrypt\n5.AES Encrypt\n6.AES Decrypt\n7.About\n*******************************************");
            String input;
            while (true)
            {
                input = Console.ReadLine();
                SEDS seds = new SEDS();
                if (input == "1")
                {
                    seds.startEncrypt();
                }
                else if (input == "2")
                {
                    seds.startDecrypt();
                }
                else if (input == "3")
                {
                    seds.startMD5();
                }
                else if (input == "4")
                {
                    seds.startSHA1();
                }
                else if (input == "5")
                {
                    seds.startAESEncrypt();
                }
                else if (input == "6")
                {
                    seds.startAESDecrypt();
                }
                else if (input == "7")
                {
                    Console.WriteLine("Simple Encrypt and Decrypt System V3.0(SEDS)\nauthor Code017\nVer. 2.1\nauthor 's E-Mail:Vincent200398@outlook.com\nauthor'sQQ:1179738228\n*******************************************");
                }
                else Console.WriteLine("unknown command!");
            }
        }

        void startAESDecrypt()
        {
            Console.WriteLine("please input file path(must be correct,or strange things would happen)");
            string filePath = Console.ReadLine();
            Console.WriteLine("please input output file path");
            string outputPath = Console.ReadLine();
            Console.WriteLine("please input decrypt key");
            string key = Console.ReadLine();
            Console.WriteLine("start decrypt...");
            bytesToFile(AESDecrypt(fileToBytes(@filePath), key), @outputPath);
            Console.WriteLine("decryptFinish!");
        }

        void startAESEncrypt()
        {
            Console.WriteLine("please input file path(must be correct,or strange things would happen)");
            string filePath = Console.ReadLine();
            Console.WriteLine("please input output file path");
            string outputPath = Console.ReadLine();
            Console.WriteLine("please input decrypt key");
            string key = Console.ReadLine();
            Console.WriteLine("start encrypt...");
            bytesToFile(AESEncrypt(fileToBytes(@filePath),key), @outputPath);
            Console.WriteLine("encryptFinish!");
        }

        void startSHA1()
        {
            Console.WriteLine("A file or a string?(F/S)");
            string FileOrStr = Console.ReadLine();
            if (FileOrStr == "F")
            {
                Console.WriteLine("please input file path(must be correct,or strange things would happen)");
                string filePath = Console.ReadLine();

                Console.WriteLine("need to output as a file?(Y/N)");
                string outFile = Console.ReadLine();
                if (outFile == "Y")
                {
                    Console.WriteLine("please input output filePath");
                    string outputPath = Console.ReadLine();
                    Console.WriteLine("start encrypt...");
                    bytesToFile(SHA1toByte(fileToBytes(@filePath)), @outputPath);
                    Console.WriteLine("encryptFinish!");
                }
                else
                {
                    Console.WriteLine("start encrypt...");
                    string result = SHA1(fileToBytes(@filePath));
                    Console.WriteLine("encryptFinish!");
                    Console.WriteLine("result:\n" + result);
                }
            }
            else if (FileOrStr == "S")
            {
                Console.WriteLine("input a string");
                string input = Console.ReadLine();
                string result = SHA1fromString(@input);
                Console.WriteLine("result:\n" + result);
            }
            else
            {
                Console.WriteLine("unkown command");
                return;
            }
        }

        void startMD5()
        {
            Console.WriteLine("A file or a string?(F/S)");
            string FileOrStr=Console.ReadLine();
            if (FileOrStr == "F")
            {
                Console.WriteLine("please input file path(must be correct,or strange things would happen)");
                string filePath = Console.ReadLine();

                Console.WriteLine("need to output as a file?(Y/N)");
                string outFile = Console.ReadLine();
                if (outFile == "Y")
                {
                    Console.WriteLine("please input output filePath");
                    string outputPath = Console.ReadLine();
                    Console.WriteLine("start encrypt...");
                    bytesToFile(MD5toByte(fileToBytes(@filePath)), @outputPath);
                    Console.WriteLine("encryptFinish!");
                }
                else
                {
                    Console.WriteLine("start encrypt...");
                    string result = MD5(fileToBytes(@filePath));
                    Console.WriteLine("encryptFinish!");
                    Console.WriteLine("result:\n" + result);
                }
            }
            else if (FileOrStr == "S")
            {
                Console.WriteLine("input a string");
                string input = Console.ReadLine();
                string result = MD5fromString(@input);
                Console.WriteLine("result:\n" + result);
            }
            else
            {
                Console.WriteLine("unkown command");
                return;
            }
        }

        void startDecrypt()
        {
            Console.WriteLine("please input file path(must be correct,or strange things would happen)");
            string filePath = Console.ReadLine();
            Console.WriteLine("please input output file path");
            string outputPath = Console.ReadLine();
            Console.WriteLine("please input decrypt key");
            string key = Console.ReadLine();
            Console.WriteLine("start decrypt...");
            bytesToFile(encrypt(fileToBytes(@filePath), keyToByte(key)), @outputPath);
            Console.WriteLine("decryptFinish!");
        }

         void startEncrypt()
        {
            Console.WriteLine("please input file path(must be correct,or strange things would happen)");
            string filePath = Console.ReadLine();
            Console.WriteLine("please input output file path");
            string outputPath = Console.ReadLine();
            Console.WriteLine("please input key(best 10-16 charactor");
            string key = Console.ReadLine();
            Console.WriteLine("start encrypt...");
            bytesToFile(encrypt(fileToBytes(@filePath), keyToByte(key)), @outputPath);
            Console.WriteLine("encryptFinish!");
        }

        byte[] fileToBytes(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] result = new byte[fs.Length];
            try {
                //fs.Read(result, 0, (int)fs.Length);
                for (int it = 0; it != fs.Length; it++)
                {
                    result[it] = br.ReadByte();
                }
                fs.Close();
                br.Close();
            }
            catch
            {
                Console.WriteLine("File Convert Failed...");
            }
            return result;
        }
        void bytesToFile(byte[] bytes,string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        byte[] encrypt(byte[] bytes, byte[] key)
        {
            byte[] result = new byte[bytes.Length];
            int keycnt = key.Length;
            int inputcnt = bytes.Length;
            int keyindex = 0;
            for (int index = 0; index != inputcnt; index++)
            {
                if(keyindex == keycnt)
                {
                    keyindex = 0;
                    result[index] = (byte)(bytes[index] ^ key[keyindex]);
                }
                else
                {
                    result[index] = (byte)(bytes[index] ^ key[keyindex]);
                    keyindex++;
                }
            }
            return result;
        }
        byte[] keyToByte(string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            return bytes;
        }
        string MD5(byte[] bytes)
        {
            string result;
            MD5 md5 = new MD5CryptoServiceProvider();
            result = BitConverter.ToString(md5.ComputeHash(bytes));

            return result;
        }
        string SHA1(byte[] bytes)
        {
            string result;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            result = BitConverter.ToString(sha1.ComputeHash(bytes));
            
            return result;
        }
        byte[] MD5toByte(byte[] bytes)
        {
            byte[] result;
            MD5 md5 = new MD5CryptoServiceProvider();
            result =md5.ComputeHash(bytes);

            return result;
        }
        byte[] SHA1toByte(byte[] bytes)
        {
            byte[] result;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            result = sha1.ComputeHash(bytes);

            return result;
        }
        string MD5fromString(string input)
        {
            string result;
            byte[] bytes = UTF8Encoding.Default.GetBytes(input);

            MD5 md5 = new MD5CryptoServiceProvider();
            bytes = md5.ComputeHash(bytes);
            result = BitConverter.ToString(md5.ComputeHash(bytes));

            return result;
        }
        string SHA1fromString(string input)
        {
            string result;
            byte[] bytes = UTF8Encoding.Default.GetBytes(input);

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            bytes = sha1.ComputeHash(bytes);
            result = BitConverter.ToString(sha1.ComputeHash(bytes));

            return result;
        }
        byte[] AESEncrypt(byte[] bytes,string key)
        {
            byte[] result;
            RijndaelManaged encryptor = new RijndaelManaged();
            encryptor.Key = UTF8Encoding.UTF8.GetBytes(key);
            encryptor.Mode = CipherMode.ECB;
            encryptor.Padding = PaddingMode.PKCS7;
            ICryptoTransform Transform = encryptor.CreateEncryptor();
            result = Transform.TransformFinalBlock(bytes, 0, bytes.Length);
            return result;
        }
        byte[] AESDecrypt(byte[] bytes, string key)
        {
            byte[] result;
            RijndaelManaged decryptor = new RijndaelManaged();
            decryptor.Key = UTF8Encoding.UTF8.GetBytes(key);
            decryptor.Mode = CipherMode.ECB;
            decryptor.Padding = PaddingMode.PKCS7;
            ICryptoTransform Transform = decryptor.CreateDecryptor();
            result = Transform.TransformFinalBlock(bytes, 0, bytes.Length);
            return result;
        }

    }
}
