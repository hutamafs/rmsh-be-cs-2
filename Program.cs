using System.Text.Json;

public class Expense
{

  public required string ID { get; set; }
  public required string Description { get; set; }
  public required int Amount { get; set; }

  public required DateTime Date { get; set; }
}

class Program
{

  static List<Expense> LoadExpense(string filePath)
  {
    {
      try
      {
        if (File.Exists(filePath))
        {
          string json = File.ReadAllText(filePath);
          if (!string.IsNullOrWhiteSpace(json))
          {
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? [];
          }
        }
        return [];
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading todos: {ex.Message}");
        return [];
      }
    }
  }

  static void WriteExpense(string filePath, List<Expense> expenses)
  {
    {
      try
      {
        string directory = Path.GetDirectoryName(filePath) ?? "";
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
          Directory.CreateDirectory(directory);
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(expenses, options);
        File.WriteAllText(filePath, json);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving expenses: {ex.Message}");
      }
    }
  }

  public static List<Expense> expenses = LoadExpense("expenses.json");
  public static int AddExpense(string description, int amount)
  {
    int maxId = 1;
    foreach (var e in expenses)
    {
      if (int.TryParse(e.ID, out int id) && id >= maxId)
      {
        maxId = id + 1;

      }
    }
    var expense = new Expense { ID = maxId.ToString(), Description = description, Amount = amount, Date = DateTime.UtcNow };
    expenses.Add(expense);
    return maxId;
  }

  public static void ListExpense()
  {
    if (expenses.Count > 0)
    {
      Console.WriteLine("ID   Date       Description       Amount");
      foreach (var e in expenses)
      {
        Console.WriteLine($"{e.ID,-4} {e.Date:dd-MM-yyyy} {e.Description,-15} {e.Amount,6}");
      }
    }
    else
    {
      Console.WriteLine("no expenses found");
    }
  }

  public static void SummaryExpense()
  {
    if (expenses.Count > 0)
    {
      int count = 0;
      foreach (var e in expenses)
      {
        count += e.Amount;
      }
      Console.WriteLine($"Total expenses: ${count}");
    }
    else
    {
      Console.WriteLine("no expenses so far");
    }
  }

  public static void DeleteId(string idToDelete)
  {
    Expense? item = expenses.SingleOrDefault(e => e.ID == idToDelete);
    if (item != null)
    {
      expenses.Remove(item);
      WriteExpense("expenses.json", expenses);
      Console.WriteLine($"deleted ID {idToDelete}");
    }
    else
    {
      throw new ArgumentException("id not found");
    }
  }

  static void Main(string[] args)
  {
    string command = args[0].ToLower();
    string? description = args.Length > 2 ? args[2] : null;
    string? amountStr = args.Length > 4 ? args[4] : null;

    try
    {
      switch (command)
      {
        case "add":
          if (description != null && amountStr != null)
          {
            bool isNum = int.TryParse(amountStr, out int amount);
            if (isNum)
            {
              int id = AddExpense(description, amount);
              WriteExpense("expenses.json", expenses);
              Console.WriteLine($"Expense added successfully (ID: {id})");
            }
            else
            {
              throw new FormatException("Error: Amount must be a valid number");
            }
          }
          else
          {
            throw new ArgumentException("Error: Description and amount are required for adding an expense");
          }
          break;
        case "summary":
          SummaryExpense();
          break;
        case "delete":
          string? idToDelete = args.Length > 1 ? args[2] : null;
          if (idToDelete != null && args[1] == "--id")
          {
            DeleteId(idToDelete);
          }
          else
          {
            throw new ArgumentException("need id to be deleted");
          }
          break;
        default:
          ListExpense();
          break;
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }


  }
}
