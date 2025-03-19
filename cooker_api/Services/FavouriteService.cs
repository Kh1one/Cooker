using cooker_api.entities;
using cooker_api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Services
{
    public class FavouriteService
    {
        private readonly CookerContext _db;
        public FavouriteService(CookerContext db)
        {
            _db = db;
        }

        

        
    }
}
