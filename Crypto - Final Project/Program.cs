using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cryptography_HW1;

namespace Crypto___Final_Project
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool loopEnd = false;
            String input = "", textKey = "", numericKey = "", text = "", output = "";

            FileIO io = new FileIO();
            SkipCipher cipher = new SkipCipher();

            try
            {
                while(!loopEnd)
                {
                    input = numericKey = text = textKey = "";

                    Console.Write("Please enter your chosen command from list below:\n" +
                                  "(1) Encrypt a text file\n" +
                                  "(2) Decrypt a text file\n" +
                                  "(3) List all possible numeric keys that can be used\n" +
                                  "(9) Quit Program\n");
                    input = Console.ReadLine();

                    switch(input)
                    {
                        case "1":
                            try
                            {
                                Console.WriteLine("Enter your text key: ");
                                textKey = Console.ReadLine();

                                Console.WriteLine("Enter your numeric key: ");
                                numericKey = Console.ReadLine();

                                text = io.Read();
                                output = cipher.Encrypt(textKey, numericKey, text);

                                if (!io.Write(output))
                                {
                                    Console.WriteLine("Shouldn't reach this line.");
                                }
                            }

                            catch(Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case "2":
                            try
                            {
                                Console.WriteLine("Enter your text key: ");
                                textKey = Console.ReadLine();

                                Console.WriteLine("Enter your numeric key: ");
                                numericKey = Console.ReadLine();

                                text = io.Read();
                                output = cipher.Decrypt(textKey, numericKey, text);

                                if( !io.Write(output) )
                                {
                                    Console.WriteLine("Shouldn't reach this line.");
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case "3":
                            Console.Clear();
                            Console.Write("Keys for a size 24 character table\n" + 
                                          "\nRow        Column      Key\n" +
                                          "-----------------------------\n" +
                                          "4           6          X010\n" +
                                          "6           4          X110\n" +
                                          "3           8          X011\n" +
                                          "8           3          X111\n" +
                                          "2           12         X014\n" +
                                          "12          2          X114\n" +
                                          "1           24         X024\n\n" + 
                                          "\nKeys for a size 25 character table\n" +
                                          "\nRow        Column      Key\n" +
                                          "-----------------------------\n" +
                                          "1           25         X025\n\n" +
                                          "\nKeys for a size 26 character table\n" +
                                          "\nRow        Column      Key\n" +
                                          "-----------------------------\n" +
                                          "2           13         X015\n" +
                                          "13          2          X115\n" +
                                          "1           26         X026\n");
                            Console.WriteLine("\n\nThe 'X' value is used to specify the start column for encryption");
                            Console.WriteLine("The 'X' value must be between 0 and the column value - 1");
                            Console.ReadLine();

                            break;

                        case "9":
                            loopEnd = true;
                            Console.WriteLine("\nThank you! Good bye!\n");
                            Console.ReadLine();
                            break;

                        default:
                            Console.WriteLine("Error::Invalid command entered. Please try again.");
                            break;
                    }

                    Console.Clear();
                }
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
