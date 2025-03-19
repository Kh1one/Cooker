using Azure.Core;
using cooker_api.entities;
using cooker_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Services
{
    public class PostService
    {
        private readonly CookerContext _db;
        public PostService(CookerContext db)
        {
            _db = db;
        }

       
    }
}
