using diversa.Interfaces;

namespace diversa.Endpoints
{
    public class MorseCode : IEndPoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/morsecode", (string input) => $"{ToMorseCode(input)}");
        }

        public static string ToMorseCode(string input)
        {
            var output = new List<string>();

            foreach (char character in input.ToUpper())
            {
                if (_textToMorse.TryGetValue(character, out var morseCode))
                {
                    output.Add(morseCode);
                }
                else
                {
                    output.Add("!");
                }
            }

            return string.Join(" ", output);
        }

        private static readonly Dictionary<char, string> _textToMorse = new()
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
            {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
            {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
            {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
            {'Z', "--.."},
            {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
            {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
            {'9', "----."}, {'0', "-----"},
            {' ', "/"}  // Space between words
        };
    }
}