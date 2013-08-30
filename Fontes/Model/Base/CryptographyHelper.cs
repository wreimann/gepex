using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Model.Base
{
    [Serializable]
    public class CryptographyHelper
    {
        public static Encoding ObterCodificacaoPadrao()
        {
            return Encoding.GetEncoding("ISO-8859-1");
        }

        public static string CalculateMD5Hash(string input)
        {
            // Primeiro passo, calcular o MD5 hash a partir da string
            MD5 md5 = MD5.Create();
            byte[] inputBytes = ObterCodificacaoPadrao().GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // Segundo passo, converter o array de bytes em uma string haxadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string ToBase64(string inputSenha)
        {
            int x;
            byte[] inArray = new byte[inputSenha.Length];
            for (x = 0; x < inputSenha.Length; x++)
                inArray[x] = (byte)inputSenha[x];
            return Convert.ToBase64String(inArray);
        }

        public static string FromBase64(string inputSenha)
        {
            int x;
            string fromBase64 = "";
            byte[] outArray = new byte[inputSenha.Length];
            outArray = Convert.FromBase64String(inputSenha);
            for (x = 0; x < outArray.Length; x++)
                fromBase64 += (char)outArray[x];
            return fromBase64;
        }

    }
}
