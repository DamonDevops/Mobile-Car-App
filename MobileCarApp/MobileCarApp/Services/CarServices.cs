﻿

using MobileCarApp.Models;

namespace MobileCarApp.Services;

public class CarServices
{
    public List<Car> GetCars() 
    {
        return new List<Car>()
        {
            new Car
            {
                Id = 1, Make = "Honda", Model = "Fit", Vin = "123"
            },
            new Car
            {
                Id = 2, Make = "Toyota", Model = "Prado", Vin = "123"
            },
            new Car
            {
                Id = 3, Make = "Honda", Model = "Civic", Vin = "123"
            },
            new Car
            {
                Id = 4, Make = "Audi", Model = "A5", Vin = "123"
            },
            new Car
            {
                Id = 5, Make = "BMW", Model = "M3", Vin = "123"
            },
            new Car
            {
                Id = 6, Make = "Nissan", Model = "Note", Vin = "123"
            },
            new Car
            {
                Id = 7, Make = "Ferrari", Model = "Spider", Vin = "123"
            }
        };
    }
}
