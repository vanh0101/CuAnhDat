// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

class Customer
{
    public string Name { get; set; }
    public int PreviousReading { get; set; }
    public int CurrentReading { get; set; }
    public int NumberOfPeople { get; set; }
    public string CustomerType { get; set; }
    public double Consumption { get; set; }
    public double UnitPrice { get; set; }
    public double EnvironmentalFee { get; set; }
    public double TotalWaterBill { get; set; }

    public Customer(string name, int previousReading, int currentReading, int numberOfPeople, string customerType)
    {
        Name = name;
        PreviousReading = previousReading;
        CurrentReading = currentReading;
        NumberOfPeople = numberOfPeople;
        CustomerType = customerType;
        CalculateConsumption();
        CalculateBill();
    }

    private void CalculateConsumption()
    {
        Consumption = CurrentReading - PreviousReading;
        if (Consumption < 0)
        {
            Console.WriteLine("This month's water number is no greater than the previous month's water number.");
            Consumption = 0;
        }
    }

    private void CalculateBill()
    {
        switch (CustomerType)
        {
            case "Governmental agency, public service":
                UnitPrice = 9955; // đồng/m3
                break;
            case "Manufacturing unit":
                UnitPrice = 11615; // đồng/m3
                break;
            case "Business service":
                UnitPrice = 22068; // đồng/m3
                break;
            default: // Household customer
                if (NumberOfPeople == 0)
                {
                    UnitPrice = 5973; // đồng/m3
                }
                else if (Consumption <= 10 * NumberOfPeople)
                {
                    UnitPrice = 5973; // đồng/m3
                }
                else if (Consumption <= 20 * NumberOfPeople)
                {
                    UnitPrice = 7052; // đồng/m3
                }
                else if (Consumption <= 30 * NumberOfPeople)
                {
                    UnitPrice = 8699; // đồng/m3
                }
                else
                {
                    UnitPrice = 15929; // đồng/m3
                }
                break;
        }

        EnvironmentalFee = UnitPrice * 0.1;
        TotalWaterBill = (UnitPrice + EnvironmentalFee) * Consumption;
        if (TotalWaterBill < 0)
        {
            TotalWaterBill = 0;
        }
    }
}

class Program
{
    static List<Customer> customers = new List<Customer>();

    static void Main(string[] args)
    {
        Console.WriteLine("Monthly Water Billing Program\n");

        while (true)
        {
            Console.WriteLine("1. Add customer");
            Console.WriteLine("2. Show customer list and bills");
            Console.WriteLine("3. Exit");

            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddCustomer();
                    break;
                case 2:
                    ShowCustomers();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void AddCustomer()
    {
        Console.WriteLine("\nEnter new customer information:");

        Console.Write("Customer type (household, government, manufacturing, business): ");
        string customerType = Console.ReadLine();

        Console.Write("Customer name: ");
        string name = Console.ReadLine();

        Console.Write("Previous water meter reading: ");
        int previousReading = int.Parse(Console.ReadLine());

        Console.Write("Current water meter reading: ");
        int currentReading = int.Parse(Console.ReadLine());

        // Kiểm tra nếu số nước tháng này ít hơn số nước tháng trước
        if (currentReading < previousReading)
        {
            Console.WriteLine("This month's water number is no greater than the previous month's water number.");
            return; // Dừng việc thêm khách hàng và trở lại menu chính
        }

        int numberOfPeople = 0;
        if (customerType.ToLower() == "household")
        {
            Console.Write("Number of people in household (if any): ");
            numberOfPeople = int.Parse(Console.ReadLine());
        }

        customers.Add(new Customer(name, previousReading, currentReading, numberOfPeople, customerType));
        Console.WriteLine("Customer added successfully!");
    }

    static void ShowCustomers()
    {
        if (customers.Count == 0)
        {
            Console.WriteLine("Customer list is empty.");
        }
        else
        {
            Console.WriteLine("\nCustomer list and bills:");

            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer name: {customer.Name}");
                Console.WriteLine($"Previous water meter reading: {customer.PreviousReading}");
                Console.WriteLine($"Current water meter reading: {customer.CurrentReading}");
                Console.WriteLine($"Consumption: {customer.Consumption} m3");
                Console.WriteLine($"Customer type: {customer.CustomerType}");
                Console.WriteLine($"Total water bill: {customer.TotalWaterBill} VND");
                Console.WriteLine("------------------------------------");
            }
        }
    }
}