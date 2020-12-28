using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace IllTechLibrary.Crypto
{
    public class CryptoServices
    {
        private static AesManaged m_aes = null;
        private static ICryptoTransform enc = null;
        private static ICryptoTransform dec = null;

        // 16 byte blocks
        private static readonly int blockBits = 128;

        internal static void Initialize()
        {
            m_aes = new AesManaged();

            m_aes.Mode = CipherMode.CBC;
            m_aes.Padding = PaddingMode.PKCS7;

            m_aes.BlockSize = blockBits;
            m_aes.KeySize = 256; // 32 byte key

            m_aes.Key = Encoding.ASCII.GetBytes("Vh~10oMo4H],1,DpCOKUf,dfgxg68cWA");
            m_aes.IV = Encoding.ASCII.GetBytes("EU`4_c#V ?:-(4>Y");

            enc = m_aes.CreateEncryptor();
            dec = m_aes.CreateDecryptor();
        }

        internal static bool Ready()
        {
            if(m_aes == null)
                return false;
        
            return true;
        }

        internal static byte[] Encrypt(byte[] bytes)
        {
            byte[] ret = null;

            using (MemoryStream output = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(output, enc, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());

                    cs.Close();

                    ret = output.ToArray();
                }
            }

            return ret;
        }

        internal static byte[] Decrypt(byte[] bytes)
        {
            byte[] ret = null;

            using (MemoryStream output = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(output, dec, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Count());

                    cs.Close();

                    ret = output.ToArray();
                }
            }

            return ret;
        }
    }
}
