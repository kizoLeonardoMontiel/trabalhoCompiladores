using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace TrabalhoCompiladores
{
    class Program
    {
        static void Main(string[] args)
        {
            int numeroLinha = 0;
            int countSimbolos = 0;
            List<String> tabelaDeToken = new List<String>(); //Lista para salvar os tokens
            List<String> tabelaDeSimbolos = new List<String>(); // Lista para salvar os simbolos + seu numero 
            List<String> tabelaDeSimbolosBruto = new List<String>(); // Lista para salvar apenas os simbolos, sem seu numero
            List<String> erros = new List<String>(); //Lista para salvar os erros
            Console.WriteLine("Digite o caminho do arquivo");
            string caminho = Console.ReadLine();

            if (File.Exists(caminho))
            {
                string[] linhas = System.IO.File.ReadAllLines(@caminho);
                foreach (string linha in linhas)
                {
                    numeroLinha++;
                    if (VerificaPalavraReservada(linha)) //Verifica se a linha é uma palavra reservada
                    {
                        tabelaDeToken.Add(new String("[" + numeroLinha + "] " + linha.ToUpper()) + " - PALAVRA RESERVADA");
                    }
                    else if (VerificaInteiro(linha)) //Verifica se a linha é um inteiro
                    {
                        if (tabelaDeSimbolosBruto.Contains(linha)) //Procura na lista de simbolos (sem o numero identificador) se já existe
                        {
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO INTEIRO " + (tabelaDeSimbolosBruto.IndexOf(linha) + 1)));
                        }
                        else //Se não existe na lista de simbolos, aidiciona 1 no contador de simbolos, adiciona nas listas com e sem numeração, e adiciona na lista de token também
                        {
                            countSimbolos++;
                            tabelaDeSimbolosBruto.Add(new String(linha));
                            tabelaDeSimbolos.Add(new String(countSimbolos + " - " + linha));
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO INTEIRO " + countSimbolos));
                        }

                    }
                    else if (VerificaDecimal(linha)) //Verifica se a linha é um decimal
                    {
                        if (tabelaDeSimbolosBruto.Contains(linha)) //Procura na lista de simbolos (sem o numero identificador) se já existe
                        {

                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO REAL " + (tabelaDeSimbolosBruto.IndexOf(linha) + 1)));
                        }
                        else //Se não existe na lista de simbolos, aidiciona 1 no contador de simbolos, adiciona nas listas com e sem numeração, e adiciona na lista de token também
                        {
                            countSimbolos++;
                            tabelaDeSimbolosBruto.Add(new String(linha));
                            tabelaDeSimbolos.Add(new String(countSimbolos + " - " + linha));
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO REAL " + countSimbolos));
                        }

                    }
                    else if (VerificaIdentificador(linha)) //Finalmente, se não for nenhum dos acima, verifica se é um identificador válido
                    {
                        if (tabelaDeSimbolosBruto.Contains(linha)) //Procura na lista de simbolos (sem o numero identificador) se já existe
                        {
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "IDENTIFICADOR " + (tabelaDeSimbolosBruto.IndexOf(linha) + 1)));
                        }
                        else //Se não existe na lista de simbolos, aidiciona 1 no contador de simbolos, adiciona nas listas com e sem numeração, e adiciona na lista de token também
                        {
                            countSimbolos++;
                            tabelaDeSimbolos.Add(new String(countSimbolos + " - " + linha));
                            tabelaDeSimbolosBruto.Add(new String(linha));
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "IDENTIFICADOR " + countSimbolos));
                        }
                    }
                    else if (VerificaComentario(linha)) //Caso não for um identificador válido, verifica se é um comentário
                    {
                        tabelaDeToken.Add(new string("[" + numeroLinha + "] " + "COMENTÁRIO"));
                    }
                    else //Se não for nada, é um erro
                    {
                        erros.Add(new string(numeroLinha.ToString() + " (" + linha + ")"));
                    }

                }
                PrintTabelas(tabelaDeToken, tabelaDeSimbolos, erros);
            }
            else
            {
                Console.WriteLine("Arquivo não existe. Saindo...");
            }
        }

        static void PrintTabelas(List<String> tabelaDeToken, List<String> tabelaDeSimbolos, List<String> erros)
        {
            Console.WriteLine("Tabela de token:");
            foreach (String s in tabelaDeToken)
            {
                Console.WriteLine(s + "\n");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Tabela de simbolos: \n");
            foreach (String s in tabelaDeSimbolos)
            {
                Console.WriteLine(s + "\n");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Erros: \n");
            foreach (String s in erros)
            {
                Console.WriteLine(s + "\n");
            }
        }

        static bool VerificaIdentificador(String linha)
        {
            if (Regex.IsMatch(linha[0].ToString(), @"[a-zA-Z]$")) //Verifica o caracter inicial, se começa com letras maiusculas ou minusculas. Se não, volta a execução para o main e, como não sendo identificador
            {
                foreach (char c in linha) //Verificando todos os caracteres da linha recebida
                {
                    if (!Regex.IsMatch(c.ToString(), @"[a-zA-Z0-9]$")) //Se não der match em a-z, A-Z ou 0-9, volta a execução para o main, como não sendo identificador
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        static Boolean VerificaPalavraReservada(String linha)
        {
            string[] palavras = { "int", "double", "float", "real", "break", "case", "char", "const", "continue" }; //Lista das palavras reservadas
            foreach (String palavra in palavras)
            {
                if (palavra.Equals(linha)) //Se a linha recebida é igual a alguma das palavras, retorna como verdadeiro para a execução do programa.
                {
                    return true;
                }
            }
            return false;
        }

        static Boolean VerificaInteiro(String linha)
        {
            if (Regex.IsMatch(linha, @"[0-9]$") && linha.Length <= 2) //Verifica se a linha recebida é de 0-9, e se o tamanho é menor ou igual a 2. Se sim, retorna true.
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Boolean VerificaDecimal(String linha)
        {
            string[] decimais = linha.Split('.'); //Divide a linha recebida, quando tiver algum ponto, e salva os dois no array decimais (exemplo - 99.50, decimais= [99, 50]
            if (decimais.Length == 2) //Se a divisão der mais ou menos de 2 itens no array, quer dizer que teve mais ou menos de um ponto no input, e retorna falso. 
            {
                if (Regex.IsMatch(decimais[0], @"[0-9]$") && decimais[0].Length <= 2) //Se o primeiro item dos decimais for de 0-9 e o tamanho for menor ou igual a dois do item
                {
                    if (Regex.IsMatch(decimais[1], @"[0-9]$") && decimais[1].Length <= 2) //Mesma verificação acima, para o segundo item. Se os dois forem menor que 2 e tiverem 0-9, retorna true
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static Boolean VerificaComentario(String linha)
        {
            if (linha.Length > 1 && linha[0].Equals('/') && linha[1].Equals('/')) //Verifica se o tamanho da linha recebida é maior que 1 ("/" apenas não é comentário), e se os dois primeiros caracteres são "//" (// apenas é um comentário).Se sim retorna verdadeiro
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
