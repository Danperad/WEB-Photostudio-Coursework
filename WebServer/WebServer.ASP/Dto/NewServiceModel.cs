using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class NewServiceModel
{
    public int ServiceId { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public int? HallId { get; set; }
    public int? EmployeeId { get; set; }
    public int? RentedItemId { get; set; }
    public int? Number { get; set; }
    public bool? IsFullTime { get; set; }
}