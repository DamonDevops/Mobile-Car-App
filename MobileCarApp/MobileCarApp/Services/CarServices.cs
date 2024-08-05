using MobileCarApp.Models;
using SQLite;
using System.Diagnostics;

namespace MobileCarApp.Services;

public class CarServices
{
    private SQLiteConnection conn;
    private string _dbPath;

    public CarServices(string dbPath)
    {
        _dbPath = dbPath;
    }

    private void Init()
    {
        if (conn != null) return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Car>();
    }

    public List<Car> GetCars() 
    {
        try
        {
            Init();
            return conn.Table<Car>().ToList();
        }
        catch(Exception e)
        {
            Debug.WriteLine($"Unable to get cars: {e.Message}");
            //await Shell.Current.DisplayAlert("Error", "Failed to retrive list of cars.", "Ok");
        }

        return new List<Car>();
        //return new List<Car>()
        //{
        //    new Car
        //    {
        //        Id = 1, Make = "Honda", Model = "Fit", Vin = "123"
        //    },
        //    new Car
        //    {
        //        Id = 2, Make = "Toyota", Model = "Prado", Vin = "123"
        //    },
        //    new Car
        //    {
        //        Id = 3, Make = "Honda", Model = "Civic", Vin = "123"
        //    },
        //    new Car
        //    {
        //        Id = 4, Make = "Audi", Model = "A5", Vin = "123"
        //    },
        //    new Car
        //    {
        //        Id = 5, Make = "BMW", Model = "M3", Vin = "123"
        //    },
        //    new Car
        //    {
        //        Id = 6, Make = "Nissan", Model = "Note", Vin = "123"
        //    },
        //    new Car
        //    {
        //        Id = 7, Make = "Ferrari", Model = "Spider", Vin = "123"
        //    }
        //};
    }
}
