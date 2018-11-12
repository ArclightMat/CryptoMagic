using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CryptoMagic
{
    class Program
    {
        static bool fileCheck(string file) // Verifica se arquivo de entrada existe.
        {
            if (!File.Exists(@file))
            {
                Console.WriteLine("Arquivo \"{0}\" não existe!", file);
                return true;
            }
            else
            {
                return false;
            }
        }
        /* ---------------------------------------------------------------------------- */
        static string filePicker() // Permite escolher o arquivo e seu local.
        {
            Console.Write("Digite o caminho do arquivo: ");
            return Console.ReadLine();

        }
        /* ---------------------------------------------------------------------------- */
        static int passwordMaker() // Gerador de password com base na input do usuário
        {
            string senha_inicial;
            char[] aSenha;
            int senha;
            senha = 0;
            // Leitura e tratamento de senha.
            Console.Write("Insira a senha: ");
            senha_inicial = Console.ReadLine();
            aSenha = senha_inicial.ToCharArray();
            for (int i = 0; i < aSenha.Length; i++)
            {
                senha = senha + aSenha[i];
            }
            return senha;
        }
        /* ---------------------------------------------------------------------------- */
        static char[] inverter(char[] input) // Inverte o texto de entrada (input)
        {
            int n = 0;
            char[] output = new char[input.Length];
            // Inversão do texto.
            for (int i = input.Length - 1; i >= 0; i--)
            {
                output[n] = input[i];
                n++;
            }
            return output;
        }
        /* ---------------------------------------------------------------------------- */
        static void Main(string[] args)
        {
            char[] aPlain, aInvPlain;
            char modo;
            string plain, invPlain, file, output;
            int pass;
            bool check;
            MODO:
            Console.Write("\t- - - CryptoMagic Mk2 - - -\n\nCriptografar: c\nDescriptografar: d\n\nSelecione a opção desejada: ");
            Char.TryParse(Console.ReadLine().ToLower(), out modo); // Permite uma exceção caso a input seja null.
            switch (modo)
            {
                case 'c':
                    file = filePicker();
                    check = fileCheck(file);
                    if (check == true) break;
                    pass = passwordMaker();
                    // Leitura e tratamento do arquivo.
                    plain = File.ReadAllText(@file);
                    aPlain = plain.ToCharArray();
                    aInvPlain = new char[aPlain.Length];
                    aInvPlain = inverter(aPlain);
                    // Transposição positiva do texto.
                    for (int i = 0; i < aPlain.Length; i++)
                    {
                        aInvPlain[i] = (char)(aInvPlain[i] + pass);
                    }
                    // Salva o resultado final em um arquivo.
                    output = new string(aInvPlain);
                    file = file.Replace(".txt", "_encrypted.txt");
                    Console.WriteLine("Escrevendo em {0}...", file);
                    File.WriteAllText(@file, output);
                    break;
                case 'd':
                    file = filePicker();
                    check = fileCheck(file);
                    if (check == true) break;
                    pass = passwordMaker();
                    // Leitura e tratamento do arquivo.
                    invPlain = File.ReadAllText(@file);
                    aInvPlain = invPlain.ToCharArray();
                    aPlain = new char[aInvPlain.Length];
                    aPlain = inverter(aInvPlain);
                    // Transposição negativa do texto.
                    for (int i = 0; i < aPlain.Length; i++)
                    {
                        aPlain[i] = (char)(aPlain[i] - pass);
                    }
                    // Salva o resultado final em um arquivo.
                    file = file.Replace("_encrypted.txt", "_decrypted.txt");
                    Console.WriteLine("Escrevendo em {0}...", file);
                    output = new string(aPlain);
                    File.WriteAllText(@file, output);
                    break;
                default: // Caso a opção seja inválida, volta ao inicio.
                    Console.Write("Opção inválida!");
                    Console.ReadKey();
                    Console.Clear();
                    goto MODO;
            }
        }
    }
}