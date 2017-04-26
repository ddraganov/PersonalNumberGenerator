using System;
using System.IO;

namespace PersonalNumberGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = new DateTime(1900, 1, 1);
            DateTime end = new DateTime(2017, 4, 30);
            string filePath = @"C:\PersonalNumbers";

            byte[] weights = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };


            using (StreamWriter file = new StreamWriter(filePath))
            {
                for (DateTime iterrator = start; iterrator <= end; iterrator = iterrator.AddDays(1))
                {
                    byte[] numberByPos = new byte[10];

                    numberByPos[0] = (byte)(iterrator.Year % 100 / 10);
                    numberByPos[1] = (byte)(iterrator.Year % 100 % 10);

                    numberByPos[2] = (byte)(iterrator.Month / 10);
                    if (iterrator.Year < 1900)
                        numberByPos[2] += 2;
                    else if (iterrator.Year > 1999)
                        numberByPos[2] += 4;

                    numberByPos[3] = (byte)(iterrator.Month % 10);

                    numberByPos[4] = (byte)(iterrator.Day / 10);
                    numberByPos[5] = (byte)(iterrator.Day % 10);

                    for (int i = 0; i < 1000; i++)
                    {
                        numberByPos[6] = (byte)(i / 100);
                        numberByPos[7] = (byte)(i % 100 / 10);
                        numberByPos[8] = (byte)(i % 10);

                        // Последната цифра от ЕГН-то:
                        int checksum = 0;
                        for (int j = 0; j < 9; j++)
                        {
                            checksum += numberByPos[j] * weights[j];
                        }
                        checksum %= 11;
                        if (checksum >= 10) checksum = 0;

                        numberByPos[9] = (byte)checksum;

                        //Console.WriteLine(string.Concat(numberByPos));
                        file.WriteLine(string.Concat(numberByPos));
                    }
                    file.Flush();
                }
            }
        }
    }
}
