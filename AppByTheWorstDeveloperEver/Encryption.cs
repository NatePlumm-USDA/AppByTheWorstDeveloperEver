using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppByTheWorstDeveloperEver
{
    public class Encryption
    {
        // naming convention issue
        AesManaged my_aes4 = new AesManaged
        {
            KeySize = 128,
            BlockSize = 128,
            // This mode is insecure
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        public void UselessFunction(String arg)
        {
            // oops - backwards check
            if (arg != null)
                return;

            ICryptoTransform enc = my_aes4.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                using (StreamWriter writer = new StreamWriter(cryptoStream))
                {
                    writer.Write(arg); // this will be null and that isn't allowed
                }

                // unused variable
                byte[] encrypted = ms.ToArray();
            }
        }
    }
}
