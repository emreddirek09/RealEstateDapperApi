﻿using Dapper;
using Microsoft.IdentityModel.Tokens;
using RealEstate_Dapper_Api.Dtos.ProductDetailDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductRepository
{
    public class ProductRespository : IProductRepository
    {
        private readonly Context _context;

        public ProductRespository(Context context)
        {
            _context = context;
        }

        public async Task CreateProduct(CreateProductDto createProductDto)
        {
            string query = "insert into Product(Title,Price,City,Disctrict,CoverImage,Address,Description,ProductCategory,EmployeeID,Type,DealOfTheDay,Date,ProductStatus,SlugUrl) Values (@title,@price,@city,@disctrict,@coverImage,@address,@description,@productCategory,@employeeID,@type,@dealOfTheDay,@date,@productStatus,@slugUrl)";
            var paremeters = new DynamicParameters();
            paremeters.Add("@title", createProductDto.Title);
            paremeters.Add("@price", createProductDto.Price);
            paremeters.Add("@city", createProductDto.City);
            paremeters.Add("@disctrict", createProductDto.Disctrict);
            paremeters.Add("@coverImage", createProductDto.CoverImage);
            paremeters.Add("@address", createProductDto.Addresss);
            paremeters.Add("@description", createProductDto.Description);
            paremeters.Add("@productCategory", createProductDto.ProductCategory);
            paremeters.Add("@employeeID", createProductDto.EmployeeID);
            paremeters.Add("@dealOfTheDay", createProductDto.DealOfTheDay);
            paremeters.Add("@date", createProductDto.Date);
            paremeters.Add("@productStatus", createProductDto.ProductStatus);
            paremeters.Add("@type", createProductDto.Type);
            paremeters.Add("@slugUrl", createProductDto.SlugUrl);
            using (var con = _context.CreateConnection())
            {
                await con.ExecuteAsync(query, paremeters);
            }
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * from Product";
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast3ProductWithCategoryDto>> GetLast3ProductAsync()
        {
            string query = @"Select top 3 ProductID,Title,Price,City,Disctrict,Description,CategoryName,CoverImage,Address,Type,DealOfTheDay,Date,SlugUrl from Product 
                               inner join Category on Product.ProductCategory=Category.CategoryID where DealOfTheDay=1 order by ProductID desc";
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultLast3ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetLast5ProductAsync()
        {
            string query = @"Select top 5 ProductID,Title,Price,City,Disctrict,CategoryName,CoverImage,Address,Type,DealOfTheDay,Date,SlugUrl from Product 
                               inner join Category on Product.ProductCategory=Category.CategoryID order by ProductID desc";
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            string query = @"Select * From Product inner join Category on 
                            Product.ProductCategory=Category.CategoryID  where EmployeeID=@employeeID and ProductStatus=0
                            order by ProductID desc";
            var paremeters = new DynamicParameters();
            paremeters.Add("@employeeID", id);
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, paremeters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            string query = @"Select * From Product inner join Category on 
                            Product.ProductCategory=Category.CategoryID  where EmployeeID=@employeeID and ProductStatus=1
                            order by ProductID desc";
            var paremeters = new DynamicParameters();
            paremeters.Add("@employeeID", id);
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, paremeters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductByDealOfTheDayTrueWithCategoryDtoAsync()
        {
            string query = @"Select top 6 ProductID,Title,Price,City,Disctrict,CategoryName,CoverImage,Address,Type,DealOfTheDay,Date,SlugUrl from Product 
                               inner join Category on Product.ProductCategory=Category.CategoryID where DealOfTheDay=1 order by ProductID desc";
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }
        public async Task<List<ResultProductWithCategoryDto>> GetAllProductByDealOfTheDayTrueWithCategory()
        {
            string query = @"Select ProductID,Title,Price,City,Disctrict,CategoryName,CoverImage,Address,Type,DealOfTheDay,Date,SlugUrl from Product 
                               inner join Category on Product.ProductCategory=Category.CategoryID where DealOfTheDay=1 order by ProductID desc";
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductIdAsync(int id)
        {
            string query = @"Select ProductID,Title,Price,City,Description,Disctrict,CategoryName,CoverImage,Address,Type,DealOfTheDay,Date,SlugUrl from Product 
                               inner join Category on Product.ProductCategory=Category.CategoryID Where ProductID=@productID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@productID", id);
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<GetProductByProductIdDto>(query, paremeters);
                return values.FirstOrDefault();
            }
        }

        public async Task<ResultGetProductDetailByIdDto> GetProductDetailByProductIdDAsync(int id)
        {
            string query = @"Select * From ProductDetails Where ProductID=@productID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@productID", id);
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultGetProductDetailByIdDto>(query, paremeters);
                return values.FirstOrDefault();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetResultProductWithCategoryDtoAsync()
        {
            string query = @"Select ProductID,Title,Price,City,Disctrict,CategoryName,CoverImage,Address,Type,DealOfTheDay,Date,SlugUrl from Product 
                               inner join Category on Product.ProductCategory=Category.CategoryID ";
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            string query = "update Product Set DealOfTheDay=@dealOfTheDay Where ProductID=@productID";

            var paremeters = new DynamicParameters();
            paremeters.Add("@dealOfTheDay", false);
            paremeters.Add("@productID", id);

            using (var con = _context.CreateConnection())
            {
                await con.ExecuteAsync(query, paremeters);
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            string query = "update Product Set DealOfTheDay=@dealOfTheDay Where ProductID=@productID";

            var paremeters = new DynamicParameters();
            paremeters.Add("@dealOfTheDay", true);
            paremeters.Add("@productID", id);

            using (var con = _context.CreateConnection())
            {
                await con.ExecuteAsync(query, paremeters);
            }
        }

        public async Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string? searchKeyValue, string? propertyCategoryId, string? city)
        {
         
            string query = @$"select * from Product ";
            if (!String.IsNullOrEmpty(searchKeyValue) || !String.IsNullOrEmpty(propertyCategoryId) || !String.IsNullOrEmpty(city))
            {
                query += " Where ";

                if (!String.IsNullOrEmpty(searchKeyValue))
                {
                    query += $" Title like '%{searchKeyValue}%' ";

                    if (!String.IsNullOrEmpty(propertyCategoryId))
                    {
                        query += $" and ProductCategory={propertyCategoryId} ";
                    }
                    if (!city.IsNullOrEmpty())
                    {
                        query += $" and City='{city}' ";
                    }
                }
                else if (!String.IsNullOrEmpty(propertyCategoryId))
                {

                    query += $" ProductCategory={propertyCategoryId} ";

                    if (!city.IsNullOrEmpty())
                    {
                        query += $" and City='{city}' ";
                    }
                }
                else if (!String.IsNullOrEmpty(city))
                {

                    query += $" City='{city}' ";

                }

            }
            var paremeters = new DynamicParameters();
            using (var con = _context.CreateConnection())
            {
                var values = await con.QueryAsync<ResultProductWithSearchListDto>(query);
                return values.ToList();
            }
        }
    }
}
