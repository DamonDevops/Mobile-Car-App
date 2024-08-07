﻿using MobileCarApp.Models;
using SQLite;
using System.Diagnostics;

namespace MobileCarApp.Services;

public class CarServices
{
    private SQLiteConnection conn;
    private string _dbPath;
    public string StatusMessage;
    int result = 0;

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
        catch(Exception)
        {
            StatusMessage = "Failed to retrieve list of cars.";
        }

        return new List<Car>();
    }

    public Car GetCar(int id)
    {
        try
        {
            Init();
            return conn.Table<Car>().FirstOrDefault(q => q.Id == id);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return null;
    }

    public void AddCar(Car car)
    {
        try
        {
            Init();

            if (car == null)
                throw new Exception("Invalid Car Record");

            result = conn.Insert(car);
            StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
        }
        catch(Exception)
        {
            StatusMessage = "Failed to insert Data";
        }
    }

    public void UpdateCar(Car car)
    {
        try
        {
            Init();

            if (car == null)
                throw new Exception("Invalid Car Record");

            result = conn.Update(car);
            StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to Update data.";
        }
    }

    public int DeleteCar(int id)
    {
        try
        {
            Init();
            result = conn.Table<Car>().Delete(q => q.Id == id);
            StatusMessage = result == 0 ? "Deletion Failed" : "Deleted Successfully";
            return result;
        }
        catch (Exception)
        {
            StatusMessage = "Failed to delete data.";
        }

        return 0;
    }
}
