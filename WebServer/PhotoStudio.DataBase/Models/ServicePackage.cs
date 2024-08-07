﻿using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public class ServicePackage
{
    internal ServicePackage()
    {
        Title = "";
        Description = "";
        Photograph = new Employee();
        Hall = new Hall();
        Photos = new List<string>();
        Services = new HashSet<ApplicationServiceTemplate>();
        Orders = new List<Order>();
    }

    #region Props

    public int Id { get; internal set; }
    [MaxLength(50)] public string Title { get; internal set; }
    [MaxLength(300)] public string Description { get; internal set; }
    public List<string> Photos { get; internal set; }
    public ISet<ApplicationServiceTemplate> Services { get; internal set; }
    public Hall Hall { get; internal set; }
    public int HallId { get; internal set; }
    public Employee Photograph { get; internal set; }
    public int EmployeeId { get; internal set; }
    public int Duration { get; internal set; }
    public decimal Price { get; internal set; }
    public List<Order> Orders { get; internal set; }

    #endregion
}