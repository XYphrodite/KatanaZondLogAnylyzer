internal class Program
{
    private static void Main(string[] args)
    {
        string? filePathToOpen = string.Empty;


        while (string.IsNullOrEmpty(filePathToOpen))
        {
            Console.WriteLine("Укажите путь к файлу логов!");
            filePathToOpen = Console.ReadLine();
        }
        
        string data = string.Empty;

        string fileData = File.ReadAllText(filePathToOpen);

        var bytes = fileData.Split(' ').ToList();

        int a1 = 0, a2 = 0, a3 = 0;

        for (int i = 0; i < bytes.Count;)
        {
            if (bytes[i] == "31")
            {
                if (bytes[i + 1] != "00")
                {
                    string request = $"Запрос:" + Environment.NewLine
                        + $"Aдрес = {bytes[i + 1]}\tСумма = {bytes[i + 3]}" + Environment.NewLine;
                    //data += request + Environment.NewLine;
                    a1++;
                }
                i += 4;
            }
            else if (bytes[i] == "3E")
            {
                //
                var t = Convert.ToSByte(bytes[i + 3], 16);
                //if(t&&0b10000000)
                //{ }
                int l = ((int)Convert.ToByte(bytes[i + 5], 16) << 8) + ((int)Convert.ToByte(bytes[i + 4], 16));
                //int l = Convert.ToInt16(bytes[i + 5] + bytes[i + 4], 16);
                //int f = Convert.ToInt16(bytes[i + 7] + bytes[i + 6], 16);
                int f = ((int)Convert.ToByte(bytes[i + 7], 16) << 8) + ((int)Convert.ToByte(bytes[i + 6], 16));
                string response = $"Ответ:" + Environment.NewLine
                    + $"Aдрес = {bytes[i + 1]}\tТемпература =  {t}" + Environment.NewLine +
                    $"Относительный уровень = {l}\tЗначение частоты = {f}\tСумма = {bytes[i + 8]}" + Environment.NewLine;
                data += response +Environment.NewLine;
                a2++;
                i += 9;
            }
            else
            {
                data += $"Не распозанано: {bytes[i]}" + Environment.NewLine + Environment.NewLine;
                a3++;
                i++;
            }
        }

        data += $"Всего запросов {a1}" + Environment.NewLine;
        data += $"Всего ответов {a2}" + Environment.NewLine;
        data += $"Всего нераспознано {a3-1}" + Environment.NewLine;

        //Console.WriteLine(data);
        File.WriteAllText(filePathToOpen.Replace(".txt","_ИД.txt"), data);
    }
}