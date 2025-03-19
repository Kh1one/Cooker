using cooker_api.entities;
using cooker_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Services
{
    public class UserService
    {
        private readonly CookerContext _db;
        public UserService(CookerContext db)
        {
            _db = db;
        }


    }
}
