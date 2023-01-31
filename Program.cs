/*
MIT License

Copyright (c) 2023 Monzu77

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Text.Json;
using Spectre.Console;

namespace LarryCS
{
    public class Dictionary
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task Main()
        {
            Console.WriteLine("");
            AnsiConsole.Markup("[white]Enter a word: [/]");

            string? inputWord = Console.ReadLine();

            string url = "https://api.dictionaryapi.dev/api/v2/entries/en/"+inputWord;

            try
            {
                var response = await client.GetStringAsync(url);
                List<Word>? word = JsonSerializer.Deserialize<List<Word>>(response);

                int i = 0;

                Console.WriteLine("");
                Console.WriteLine("");
                AnsiConsole.Markup("[cornsilk1]──────────────────────────────────────────[/] [white]Definitions [/][cornsilk1]──────────────────────────────────────────[/]");
                foreach (var obj in word[0].meanings[0].definitions)
                {
                    AnsiConsole.MarkupInterpolated($"\n[wheat1]● {word[0].meanings[0].definitions[i].definition}[/]");
                    i++;
                }
                i = 0;
                Console.WriteLine("");
                AnsiConsole.Markup("\n [white]──────────────────────────────────────────[/] [white]Phonetics [/][white]──────────────────────────────────────────[/]");
                foreach (var obj in word[0].phonetics)
                {
                    AnsiConsole.MarkupInterpolated($"\n[wheat1]● {word[0].phonetics[i].text}[/]");                    
                    i++;
                }
                i = 0;
                Console.WriteLine("");
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("");
                Console.WriteLine("Error occured: " + e);

            }

        }


    }

    public class Word
    {
        public List<Meaning>? meanings {get; set;}
        public List<Meaning>? phonetics {get; set;}
    }

    public class Meaning
    {
        public List<Definition>? definitions {get; set;}
        public string? text {get; set;}
    }

    public class Definition
    {
        public string? definition {get; set;}
        
    }
}