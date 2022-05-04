using Microsoft.EntityFrameworkCore;
using PhotostudioDB.Exceptions;
using PhotostudioDB.Models;
using PhotostudioDB.Models.Services;

namespace PhotostudioDB;

public static class DbWorker
{
    private static ApplicationContext? _db;
    private static bool _isTrying;

    public static bool IsLoad
    {
        get
        {
            if (_db is not null) return true;
            if (_isTrying) return false;
            _isTrying = true;
            try
            {
                _db = new ApplicationContext(ApplicationContext.GetDb());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static bool UnLoad()
    {
        if (_db is null) return false;
        _db = null;
        _isTrying = false;
        return true;
    }

    internal static bool Save()
    {
        if (!IsLoad) throw new DbNotLoadException();
        try
        {
            _db!.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    #region Role

    internal static Role? GetRoleById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Roles.FirstOrDefault(o => o.Id == id);
    }

    #endregion

    #region Profile

    internal static Profile? GetProfileByClient(int? clientId)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Profiles.FirstOrDefault(p => p.ClientId == clientId!);
    }

    internal static Profile? GetProfileByEmployee(int? employeeId)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Profiles.FirstOrDefault(p => p.EmployeeId == employeeId);
    }

    public static bool AddProfile(Profile profile)
    {
        if (!IsLoad) throw new DbNotLoadException();
        try
        {
            _db!.Profiles.Add(profile);
            _db.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    #endregion

    #region Client

    internal static Client? GetClientByPhone(string phone)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Clients.FirstOrDefault(c => c.Phone.Contains(phone));
    }

    internal static Client? GetClientLogin(string login)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Clients.Include(c => c.Profile).FirstOrDefault(c => c.Profile!.Login == login);
    }

    internal static Client? GetClientAuth(string login, string pass)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Clients.Include(c => c.Profile).Include(c => c.Orders)
            .ThenInclude(o => o.Services).FirstOrDefault(
                c => c.Profile != null && c.Profile.Login == login && c.Profile.Pass == pass);
    }

    internal static bool RegisterClient(Profile client)
    {
        if (!IsLoad) throw new DbNotLoadException();
        try
        {
            _db!.Profiles.Add(client);
            _db.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Employee

    internal static Employee? GetEmployeeById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Employees.FirstOrDefault(e => e.Id == id);
    }

    internal static IEnumerable<Employee> GetEmployees()
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Employees.Where(e => _db.Profiles.First(p => p.EmployeeId == e.Id).IsActive);
    }

    internal static IEnumerable<Employee> GetEmployeesByRole(int roleId)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Employees.Where(e => e.RoleId == roleId);
    }

    #endregion

    #region Order

    internal static Order? GetOrderById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Orders.FirstOrDefault(o => o.Id == id);
    }

    internal static IEnumerable<Order> GetClientOrders(int clientId)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Orders.Where(o => o.ClientId == clientId);
    }

    public static bool AddOrder(Order order)
    {
        if (!IsLoad) throw new DbNotLoadException();
        try
        {
            _db!.Orders.Add(order);
            _db.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    #endregion

    #region Service

    internal static Service? GetServiceById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Services.FirstOrDefault(s => s.Id == id);
    }

    internal static IEnumerable<Service> GetServices()
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Services;
    }

    #endregion

    #region Contract

    internal static Contract? GetContractById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Contracts.FirstOrDefault(c => c.Id == id);
    }

    internal static IEnumerable<Contract> GetContractsById(int clientId)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Contracts.Where(c => c.ClientId == clientId);
    }

    #endregion

    #region Rented

    internal static Hall? GetHallById(int hall)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Halls.FirstOrDefault(h => h.Id == hall);
    }

    internal static RentedItem? GetRentedItemById(int hall)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.RentedItems.FirstOrDefault(h => h.Id == hall);
    }

    #endregion

    #region Address

    internal static Address? GetAddressById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Addresses.FirstOrDefault(a => a.Id == id);
    }

    internal static bool AddAddress(Address address)
    {
        if (!IsLoad) throw new DbNotLoadException();
        try
        {
            _db!.Addresses.Add(address);
            _db.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    #endregion

    #region ApplicationService

    internal static IEnumerable<ApplicationService> GetServicesOrder(int order)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return GetOrderById(order) is null ? new List<ApplicationService>() : GetOrderById(order)!.Services;
    }

    #endregion

    #region Status

    internal static Status? GetStatusById(int id)
    {
        if (!IsLoad) throw new DbNotLoadException();
        return _db!.Statuses.FirstOrDefault(s => s.Id == id);
    }

    #endregion

    internal static bool AddToken(RefreshToken token)
    {
        if (!IsLoad) throw new DbNotLoadException();
        try
        {
            _db!.RefreshTokens.Add(token);
            _db.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    internal static Profile? GetToken(string token)
    {
        if (!IsLoad) throw new DbNotLoadException();
        var strtoken = _db!.RefreshTokens.Include(r => r.Profile)
            .ThenInclude(p => p.Client).FirstOrDefault(a => a.Token == token);
        if (strtoken is null) return null;
        _db.RefreshTokens.Remove(strtoken);
        return strtoken.Profile;
    }
}