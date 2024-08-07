﻿namespace PhotoStudio.WebApi.Lib.Dto;

public class RegisterDto
{
    public RegisterDto()
    {
        Password = LastName = FirstName = Phone = EMail = "";
    }

    public string Password { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string Phone { get; set; }
    public string EMail { get; set; }

    public static bool Check(RegisterDto? model)
    {
        return model is null || string.IsNullOrEmpty(model.Password) ||
               string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.FirstName) ||
               string.IsNullOrEmpty(model.Phone) || string.IsNullOrEmpty(model.EMail);
    }

    public DataBase.Models.Client MapToClient()
    {
        return new DataBase.Models.Client(LastName, FirstName, Phone)
        {
            MiddleName = MiddleName,
            EMail = EMail,
            Password = Password
        };
    }
}