using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

//SEDS V1.0 source file
//by Code 017
//using GPL V3

namespace SEDS
{
    class SEDS
    {
         static void Main(string[] args)
        {
            Console.Title = "Simple Encrypt and Decrypt System V2.0";

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Simple Encrypt and Decrypt System V2.0(SEDS)\nby Code017 copyright\n1.Encrypt\n2.Decrypt\n3.MD5Encrypt\n4.SHA1Encrypt\n5.About this software\n*******************************************");
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
                else if(input == "3")
                {
                    seds.startMD5();
                }
                else if (input == "4")
                {
                    seds.startSHA1();
                }
                else if (input == "5")
                {
                    Console.WriteLine("Simple Encrypt and Decrypt System V2.0(SEDS)\nauthor Code017\nVer. 1.0\nauthor 's E-Mail:Vincent200398@outlook.com\nauthor'sQQ:1179738228\n***************************************");
                }
                else Console.WriteLine("unknown command!");
            }
        }

        void startSHA1()
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

        void startMD5()
        {
            Console.WriteLine("please input file path(must be correct,or strange things would happen)");
            string filePath = Console.ReadLine();
            Console.WriteLine("need to output as a file?(Y/N)");
            string outFile = Console.ReadLine();
            if(outFile == "Y")
            {
                Console.WriteLine("please input output filePath");
                string outputPath = Console.ReadLine();
                Console.WriteLine("start encrypt...");
                bytesToFile(MD5toByte(fileToBytes(@filePath)),@outputPath);
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

        private void startDecrypt()
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
    }
}
