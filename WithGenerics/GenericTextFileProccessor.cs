
using System.Text;

namespace TrainingGenerics.WithGenerics
{
    public static class GenericTextFileProccessor
    {
        public static List<T> LoadFromTextFile<T>(string filePath) where T: class, new()
        {
            var lines = File.ReadAllLines(filePath).ToList() ;
            List<T> output = new List<T>();
            T entry = new T();

            var cols = entry.GetType().GetProperties();

            var headers = lines[0].Split(',');

            lines.RemoveAt(0);

            foreach(var line in lines)
            {
                entry = new T();
                var vals = line.Split(',');

                for(var i = 0; i < headers.Length; i++)
                {
                    foreach(var col in cols)
                    {
                        if(col.Name == headers[i])
                        {
                            col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                        }
                    }
                }

                output.Add(entry);

            }

            return output;

        }

        public static void SaveToTextFile<T>(List<T> data, string filePath) where T: class
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            var cols = data[0].GetType().GetProperties();

            foreach(var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach(var row in data)
            {
                line = new StringBuilder();

                foreach(var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            File.WriteAllLines(filePath, lines);

        }
    }
}
