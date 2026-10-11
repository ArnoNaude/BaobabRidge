using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabRidge
{
    internal class AnimalRepository
    {
        public string FilePath { get; private set; }

        public AnimalRepository()
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt");
        }

        private static readonly UTF8Encoding Utf8NoBom = new UTF8Encoding(false);

        
        public bool TryLoadAll(out List<Animal> animals, out int skippedCount, out string error)
        {
            animals = new List<Animal>();
            skippedCount = 0;
            error = "";
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.WriteAllText(FilePath, "", Utf8NoBom); // Feature 2.5: create empty file
                    return true;
                }

                var seenIds = new HashSet<string>();
                foreach (string line in File.ReadAllLines(FilePath, Encoding.UTF8))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue; // blank lines are ignored, not counted

                    Animal animal;
                    if (Animal.TryParseLine(line, out animal) && seenIds.Add(animal.AnimalId))
                    {
                        animals.Add(animal);
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                return true;
            }
            catch (IOException)
            {
                error = DescribeFileError(new IOException());
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                error = DescribeFileError(new UnauthorizedAccessException());
                return false;
            }
        }

       
        public bool TryAppend(Animal animal, out string error)
        {
            error = "";
            try
            {
                string existing = File.Exists(FilePath) ? File.ReadAllText(FilePath, Encoding.UTF8) : "";
                // If the file doesn't end with a newline, add one first so lines don't merge.
                string prefix = existing.Length > 0 && !existing.EndsWith("\n") ? Environment.NewLine : "";
                File.AppendAllText(FilePath, prefix + animal.ToFileLine() + Environment.NewLine, Utf8NoBom);
                return true;
            }
            catch (IOException)
            {
                error = DescribeFileError(new IOException());
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                error = DescribeFileError(new UnauthorizedAccessException());
                return false;
            }
        }

        public static string DescribeFileError(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                return "The program does not have permission to use animals.txt. Check that the file is not read-only.";
            }
            if (ex is DirectoryNotFoundException)
            {
                return "The folder containing animals.txt could not be found.";
            }
            if (ex is FileNotFoundException)
            {
                return "animals.txt could not be found.";
            }
            if (ex is IOException)
            {
                return "animals.txt is being used by another program. Close it and try again.";
            }
            return "An unexpected file error occurred.";
        }
    }
}
