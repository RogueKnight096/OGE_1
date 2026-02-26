using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        List<EmployeeAccess> employees = read();
        Console.WriteLine($"Total records read: {employees.Count}");

        var inactiveEmployees =
        (
            from employ in employees
            where employ.CloudLifecycleState.Equals(false)
            select employ
        ).ToList<EmployeeAccess>();

        Console.WriteLine("Employees Currently Inactive: " + inactiveEmployees.Count);

        var AlphabetizedInactiveEmployees =
        (
            from employ in employees
            where employ.CloudLifecycleState == false
            group employ by employ.IdentityId into g
            let emp = g.First()
            orderby emp.LastName
            select emp
        ).ToList();

        foreach (var val in AlphabetizedInactiveEmployees)
        {
            Console.WriteLine(val.DisplayName);
        }

        var inactiveGrouped = employees
            .Where(e => !e.CloudLifecycleState)
            .GroupBy(e => e.DisplayName)
            .OrderBy(g => g.Key);

        foreach (var group in inactiveGrouped)
        {
            Console.WriteLine(group.Key);
            foreach (var record in group)
            {
                if (record.AccessSourceName != null && record.AccessDisplayName != null)
                {
                    Console.WriteLine($"   {record.AccessSourceName}  {record.AccessDisplayName}");
                }
            }
        }
        Console.WriteLine();
    }

    public static List<EmployeeAccess> read()
    {
        List<EmployeeAccess> employeeList = new List<EmployeeAccess>();
        using StreamReader sr = new StreamReader("Francis Tuttle Identities_Basic.csv");
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line != null)
            {
                string[] parts = line.Split(",");
                bool lifecycleState = parts[4].Trim() == "active";
                EmployeeAccess emp = new EmployeeAccess
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


public struct EmployeeAccess // Struct for all Employees
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