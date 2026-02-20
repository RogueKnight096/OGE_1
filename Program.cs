class Program
{
    static void Main(string[] args)
    {
        List<Employees> employees = read();
        Console.WriteLine($"Total records read: {employees.Count}");
    }

    public static List<Employees> read()
    {
        List<Employees> employeeList = new List<Employees>();
        using StreamReader sr = new StreamReader("Francis Tuttle Identities_Basic.csv");
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line != null)
            {
                string[] parts = line.Split(",");
                bool lifecycleState = parts[4].ToLower() == "active";
                Employees emp = new Employees
                {
                    DisplayName = parts[0],
                    FirstName = parts[1],
                    LastName = parts[2],
                    WorkEmail = parts[3],
                    CloudLifecycleState = lifecycleState,
                    IdentityId = parts[5],
                    IsManager = bool.Parse(parts[6]),
                    Department = parts[7],
                    JobTitle = parts[8],
                    Uid = parts[9],
                    AccessType = parts[10],
                    AccessSourceName = parts[11],
                    AccessDisplayName = parts[12],
                    AccessDescription = parts[13]
                };

                employeeList.Add(emp);
            }
        }
        return employeeList;
    }
}


public struct Employees // Struct for all Employees
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkEmail { get; set; }
        public bool CloudLifecycleState { get; set; }
        public string IdentityId { get; set; }
        public bool IsManager { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string Uid { get; set; }
        public string AccessType { get; set; }
        public string AccessSourceName { get; set; }
        public string AccessDisplayName { get; set; }
        public string AccessDescription { get; set; }
    }