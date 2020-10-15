using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
/// <summary>
/// Descripción breve de FuncionesGenerales
/// </summary>
public class FuncionesGenerales
{
    /// <summary>
    /// Genera Password Aleatoria
    /// </summary>
    /// <param name="passwordLength"> PasswordLenght :La cantidad de caracteres de la cadena de salida</param>
    /// <returns> retorna :String </returns>
    /// 
    public string generarPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }
    public string Encriptar(string texto, string key)
    {
        //arreglo de bytes donde guardaremos la llave
        byte[] keyArray;
        //arreglo de bytes donde guardaremos el texto
        //que vamos a encriptar
        byte[] Arreglo_a_Cifrar =
        UTF8Encoding.UTF8.GetBytes(texto);

        //se utilizan las clases de encriptación
        //provistas por el Framework
        //Algoritmo MD5
        MD5CryptoServiceProvider hashmd5 =
        new MD5CryptoServiceProvider();
        //se guarda la llave para que se le realice
        //hashing
        keyArray = hashmd5.ComputeHash(
        UTF8Encoding.UTF8.GetBytes(key));

        hashmd5.Clear();

        //Algoritmo 3DAS
        TripleDESCryptoServiceProvider tdes =
        new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        //se empieza con la transformación de la cadena
        ICryptoTransform cTransform =
        tdes.CreateEncryptor();

        //arreglo de bytes donde se guarda la
        //cadena cifrada
        byte[] ArrayResultado =
        cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
        0, Arreglo_a_Cifrar.Length);

        tdes.Clear();

        //se regresa el resultado en forma de una cadena
        return Convert.ToBase64String(ArrayResultado,
               0, ArrayResultado.Length);
    }
    public string Desencriptar(string textoEncriptado, string key)
    {
        byte[] keyArray;
        //convierte el texto en una secuencia de bytes
        byte[] Array_a_Descifrar =
        Convert.FromBase64String(textoEncriptado);

        //se llama a las clases que tienen los algoritmos
        //de encriptación se le aplica hashing
        //algoritmo MD5
        MD5CryptoServiceProvider hashmd5 =
        new MD5CryptoServiceProvider();

        keyArray = hashmd5.ComputeHash(
        UTF8Encoding.UTF8.GetBytes(key));

        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes =
        new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform =
         tdes.CreateDecryptor();

        try
        {
            byte[] resultArray =
       cTransform.TransformFinalBlock(Array_a_Descifrar,
       0, Array_a_Descifrar.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        catch (Exception ex)
        {
            return "";

        }

        //se regresa en forma de cadena

    }
    public bool ValidaRut(string rutCompleto)
    {

        rutCompleto = rutCompleto.Replace(".", "");
        rutCompleto = rutCompleto.Replace("-", "");
        rutCompleto = rutCompleto.Substring(0, rutCompleto.Length - 1) + '-' + rutCompleto.Substring(rutCompleto.Length - 1);

        string[] rutSeparado = null;
        rutSeparado = rutCompleto.Split(new char[] { '-' });
        if (rutSeparado.Length != 2)//|| rutCompleto.IndexOf('.')!=0)
        {
            return false;
        }
        else
        {
            int resultado;
            int.TryParse(rutSeparado[0].ToString().Replace(".", ""), out resultado);
            if (resultado <= 0)
            {
                return false;
            }
            else
            {
                int rut = int.Parse(rutSeparado[0].ToString());

                int Digito;
                int Contador;
                int Multiplo;
                int Acumulador;
                string RutDigito;
                Contador = 2;
                Acumulador = 0;
                while (rut != 0)
                {
                    Multiplo = (rut % 10) * Contador;
                    Acumulador = Acumulador + Multiplo;
                    rut = rut / 10;
                    Contador = Contador + 1;
                    if (Contador == 8)
                    {
                        Contador = 2;
                    }
                }
                Digito = 11 - (Acumulador % 11);
                RutDigito = Digito.ToString().Trim();
                if (Digito == 10)
                {
                    RutDigito = "K";
                }
                if (Digito == 11)
                {
                    RutDigito = "0";
                }
                if (RutDigito.ToUpper() != rutSeparado[1].ToString().Trim().ToUpper())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
    public bool ValidaPlaca(string placa)
    {
        // Create a pattern for a word that starts with letter "M"  
        string pattern = @"[a-zA-Z]{2}\d{4}|[a-zA-Z]{4}\d{2}";
        // Create a Regex  
        Regex rg = new Regex(pattern);
        return rg.IsMatch(placa);
    }
}



