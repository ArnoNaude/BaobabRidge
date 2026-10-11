using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaobabRidge
{
    internal class AnimalValidator
    {
        private static readonly Regex IdPattern = new Regex(@"^WR-[0-9]{4}$");
        public static bool IsValidIdFormat(string id) => IdPattern.IsMatch(id);
        public static bool TryParseWholeNumber(string text, out int value)
        {
            value = 0;
            if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out value)) return false;
            return value >= 0 && value <= 100;
        }

        
        public static bool TryValidateId(string input, out string id, out string error)
        {
            id = (input ?? "").Trim().ToUpperInvariant();
            error = "";
            if (id.Length == 0) { error = "Animal ID is required."; return false; }
            if (!IsValidIdFormat(id)) { error = "Animal ID must be 'WR-' followed by exactly 4 digits (e.g. WR-0007)."; return false; }
            return true;
        }

        
        public static bool TryValidateName(string input, out string name, out string error)
            => ValidateText(input, "Name", out name, out error);

        
        public static bool TryValidateSpecies(string input, out string species, out string error)
            => ValidateText(input, "Species", out species, out error);

       
        public static bool TryValidateAge(string input, out int age, out string error)
        {
            error = "";
            if (!TryParseWholeNumber((input ?? "").Trim(), out age))
            {
                error = "Age must be a whole number between 0 and 100.";
                return false;
            }
            return true;
        }

        
        public static bool TryValidateScore(string input, out int score, out string error)
        {
            error = "";
            if (!TryParseWholeNumber((input ?? "").Trim(), out score))
            {
                error = "Recovery Score must be a whole number between 0 and 100.";
                return false;
            }
            return true;
        }

       
        private static bool ValidateText(string input, string fieldName, out string value, out string error)
        {
            value = (input ?? "").Trim();
            error = "";
            if (value.Length == 0) { error = $"{fieldName} is required."; return false; }
            if (value.Length > 30) { error = $"{fieldName} must be 30 characters or fewer."; return false; }
            if (value.Contains('|')) { error = $"{fieldName} must not contain the pipe character (|)."; return false; }
            return true;
        }
    }
}
