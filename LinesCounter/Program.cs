using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

Console.WriteLine("Укажите каталог проекта");
var path = Console.ReadLine();

DirectoryInfo directory = new DirectoryInfo(path);

var files = directory.GetFiles("",SearchOption.AllDirectories);
int counter = 0;
long symbols = 0;
foreach (var file in files)
{
	if ((file.Extension == ".cs" || file.Extension == ".xaml") && !file.Name.Contains("AssemblyInfo") && !file.FullName.Contains("\\obj\\") && !file.FullName.Contains("\\bin\\")) //&& !Regex.IsMatch(file.Name,"[.].*[.]")
    {
        var data = File.ReadAllLines(file.FullName);
        data = data.Where(x => !string.IsNullOrWhiteSpace(x) && !x.Contains("using") && x.Trim().Remove(2) != "//").ToArray();
        var count = data.Count();
        var symb = data.Sum(x => x.Length);
        Console.WriteLine(file.Name + ": " + count + ": " + symb);
        counter += count;
        symbols += symb;
    }
}
Console.WriteLine(counter);
Console.WriteLine(symbols);
Console.Read();
