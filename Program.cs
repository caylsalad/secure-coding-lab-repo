namespace SecureLogin;

using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        bool isLoggedIn = false;

        Account adminAccount = new Account("admin", "S3cur3Pa55w0rd");

        string inputUser = string.Empty;
        string inputPassword = string.Empty;

        //computation and comparison of hash values: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/compute-hash-values

        //byte arrays to compare hashes
        byte[] inputBytesUser;
        byte[] hashBytesUser;
        byte[] inputBytesPassword;
        byte[] hashBytesPassword;

        //hashes of correct login info
        byte[] correctUserBytes;
        byte[] correctUserHash;
        byte[] correctPasswordBytes;
        byte[] correctPasswordHash;
        //get bytes
        correctUserBytes = ASCIIEncoding.ASCII.GetBytes(adminAccount.Username);
        correctPasswordBytes = ASCIIEncoding.ASCII.GetBytes(adminAccount.Password);
        //hash
        correctUserHash = new MD5CryptoServiceProvider().ComputeHash(correctUserBytes);
        correctPasswordHash = new MD5CryptoServiceProvider().ComputeHash(correctPasswordBytes);

        //compare if input username/password equal the actual username/password above
        bool bytesEqualU = false;
        bool bytesEqualP = false;

        Console.WriteLine("Welcome to the generic login form!");

        while(isLoggedIn != true)
        {
            Console.WriteLine("Enter valid username: ");
            inputUser = Console.ReadLine();
            inputBytesUser = ASCIIEncoding.ASCII.GetBytes(inputUser);
            Console.WriteLine("Enter valid password: ");
            inputPassword = Console.ReadLine();
            inputBytesPassword = ASCIIEncoding.ASCII.GetBytes(inputPassword);

            //compute hashes
            hashBytesUser = new MD5CryptoServiceProvider().ComputeHash(inputBytesUser);
            hashBytesPassword = new MD5CryptoServiceProvider().ComputeHash(inputBytesPassword);

            if (inputUser != adminAccount.Username || inputPassword != adminAccount.Password)
            {
                if (hashBytesUser.Length == correctUserHash.Length)
                {
                    int i = 0;
                    while ((i < hashBytesUser.Length) && (hashBytesUser[i] == correctUserHash[i]))
                    {
                        i += 1;
                    }
                    if (i == hashBytesUser.Length)
                    {
                        bytesEqualU = true;
                    }
                }
                if (hashBytesPassword.Length == correctPasswordHash.Length)
                {
                    int i = 0;
                    while ((i < hashBytesPassword.Length) && (hashBytesPassword[i] == correctPasswordHash[i]))
                    {
                        i += 1;
                    }
                    if (i == hashBytesPassword.Length)
                    {
                        bytesEqualP = true;
                    }
                }

                if ((bytesEqualU != true) || (bytesEqualP != true))
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect login information. Please try again.");
                }
            }
            else if ((inputUser == adminAccount.Username && inputPassword == adminAccount.Password) && ((bytesEqualU = true) && (bytesEqualP = true)))
            {
                isLoggedIn = true;
                Console.Clear();
                Console.WriteLine("You're in");
            }
        }
        
    }
}
