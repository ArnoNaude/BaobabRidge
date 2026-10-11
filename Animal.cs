using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BaobabRidge
{
    internal class Animal
    {
        public string AnimalId { get; set; } = "";
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public int Age { get; set; }
        public int RecoveryScore { get; set; }
        public string Status { get; set; } = "";
        public string HousingUnit { get; set; } = "";

        
        public static (string Status, string HousingUnit) Classify(int score)
        {
            if (score <= 19) return ("Critical", "Intensive Care Unit");
            if (score <= 39) return ("Serious", "High-Dependency Ward");
            if (score <= 59) return ("Stable", "Recovery Ward");
            if (score <= 79) return ("Recovering", "Outdoor Enclosure");
            return ("Release-Ready", "Pre-Release Camp");
        }

        
        public void ApplyClassification()
        {
            (Status, HousingUnit) = Classify(RecoveryScore);
        }

     
        public string ToFileLine()
        {
            return string.Join("|", AnimalId, Name, Species, Age, RecoveryScore, Status, HousingUnit);
        }

        
        public static bool TryParseLine(string line, out Animal animal)
        {
            animal = new Animal();
            string[] parts = line.Split('|');
            if (parts.Length != 7) return false;

            string id = parts[0].Trim().ToUpperInvariant();
            if (!AnimalValidator.IsValidIdFormat(id)) return false;

            if (!AnimalValidator.TryParseWholeNumber(parts[3].Trim(), out int age)) return false;
            if (!AnimalValidator.TryParseWholeNumber(parts[4].Trim(), out int score)) return false;

            animal = new Animal
            {
                AnimalId = id,
                Name = parts[1].Trim(),
                Species = parts[2].Trim(),
                Age = age,
                RecoveryScore = score,
                Status = parts[5].Trim(),
                HousingUnit = parts[6].Trim()
            };
            return true;
        }
    }
}
