using System;
using System.IO;
using System.Security.Cryptography;

namespace obfuscator
{
    class Program
    {
        private static readonly string TargetFile = @"/Users/chandlerwhite/Desktop/OBFUSCATE/test2.pdf"; // replace with the path to your target file
        // private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();    // used to generate cryptographically secure random bytes
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        private static string answer = "";    //used to store user input

        static void Main(string[] args)
        {
            try
            {
                //Get path of a folder called "OBFUSCATE" that is on the user's desktop.
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/OBFUSCATE";
                //Check if the folder exists, if not, create it.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //request name of file to obfuscate
                Console.WriteLine("Enter the name of the file to obfuscate: ");
                string file = Console.ReadLine();
                // Check if the target file exists in the folder "OBFUSCATE" on the desktop
                if (!File.Exists(path + "/" + file))
                {
                    Console.WriteLine("Error: Target file does not exist!");
                } else {
                    Console.WriteLine("Are you sure you want to obfuscate this file?  This action cannot be undone! (Y/N)");
                    answer = "";    //make sure answer is cleared
                    answer = Console.ReadLine();
                    if (answer == "Y")
                    {
                        Console.WriteLine("Obfuscating file...");

                        // Get the length of the target file
                        var fileLength = new FileInfo(path + "/" + file).Length;

                        // Generate random bytes and overwrite the file five times
                        var randomBytes = new byte[fileLength];
                        // var rng = new Random();

                        for (int i = 0; i < 5; i++)
                        {
                            rng.GetBytes(randomBytes);
                            File.WriteAllBytes(path + "/" + file, randomBytes);
                        }

                        Console.WriteLine("File obfuscation complete!");

                        //ask if user wants to delete the file
                        Console.WriteLine("Do you want to delete the file? (y/n)");
                        answer = "";    //make sure answer is cleared
                        answer = Console.ReadLine();
                        if (answer == "y")
                        {
                            File.Delete(path + "/" + file);
                            Console.WriteLine("File deleted.");
                        }
                        else
                        {
                            Console.WriteLine("File not deleted.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not obfuscated.");
                    }
                    
                }

                Console.WriteLine("Would you like to obfuscate another file? (y/n)");
                answer = "";    //make sure answer is cleared
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    Main(args);
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            rng.Dispose();   // close the RNGCryptoServiceProvider

        }

    }
}