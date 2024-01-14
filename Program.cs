using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


// obter o caminho para o diretório atual
var currentDirectory = Directory.GetCurrentDirectory();

// criar o caminho completo para o diretório lojas
var storesDirectory = Path.Combine(currentDirectory, "stores");

// Criar uma pasta com o caminho para o diretório
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

// Substitua a cadeia de caracteres stores na chamada de função FindFiles
var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles); // Add this line of code

// criar um arquivo vazio chamado totals.txt dentro do diretório salesTotalDir recém-criado
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

IEnumerable<string> FindFiles(string folderName)
{
  List<string> salesFiles = new();

  var foundFiles = Directory.EnumerateFiles
  (
    folderName, "*", SearchOption.AllDirectories
  );

  foreach (var file in foundFiles)
  {
    var extension = Path.GetExtension(file);

    if (extension == ".json") salesFiles.Add(file);
  }

  return salesFiles;
}

// Criar um método para calcular os totais de vendas
double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
  double salesTotal = 0;

  // read files loop
  foreach (var file in salesFiles)
  {
    // read the cntents of the file
    string salesJson = File.ReadAllText(file);

    // Parse the contents as JSON
    SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

    // Add the amount found in the Total field to the salesTotal variable
    salesTotal += data?.Total ?? 0;
  }

  return salesTotal;
}


record SalesData(double Total);







