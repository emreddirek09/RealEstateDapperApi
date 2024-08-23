﻿using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async void CreateCategory(CreateCategoryDto categoryDto)
        {
            string query = "insert into Category(CategoryName,CategoryStatus) Values (@categoryName,@categoryStatus)";
            var paremeters = new DynamicParameters();
            paremeters.Add("@categoryName", categoryDto.CategoryName);
            paremeters.Add("@categoryStatus", true);
            using (var con = _context.CreateConnection())
            {
                await con.ExecuteAsync(query,paremeters);
            }

        }

        public async void DeleteCategory(int id)
        {
            string query = "Delete From Category Where CategoryID=@categoryID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@categoryID", id); 
            using (var con = _context.CreateConnection())
            {
                await con.ExecuteAsync(query, paremeters);
            }
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "Select * From Category";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateCategory(UpdateCategoryDto categoryDto)
        {
            string query = "update Category Set CategoryName=@categoryName,CategoryStatus=@categoryStatus Where CategoryID=@categoryID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@categoryName", categoryDto.CategoryName);
            paremeters.Add("@categoryStatus", categoryDto.CategoryStatus);
            paremeters.Add("@categoryID", categoryDto.CategoryId);
            using (var con = _context.CreateConnection())
            {
                await con.ExecuteAsync(query, paremeters);
            }
        }
    }
}