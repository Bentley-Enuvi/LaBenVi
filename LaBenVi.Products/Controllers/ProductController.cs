using AutoMapper;
using LaBenVi.Products.Data;
using LaBenVi.Products.Models;
using LaBenVi.Products.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaBenVi.Products.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly LaBenViDbContext _context;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductController(LaBenViDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new ResponseDto();
            _mapper = mapper;

        }


        [HttpGet]
        public ResponseDto GetAllProducts()
        {
            try
            {
                IEnumerable<Product> result = _context.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetById(int id)
        {
            try
            {
                Product result = _context.Products.First(p => p.ProductId == id);

                _response.Result = _mapper.Map<ProductDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product result = _mapper.Map<Product>(productDto);
                _context.Products.Add(result);
                _context.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(result);

                //if (productDto.ImageUrl != null)
                //{

                //    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                //    string filePath = @"wwwroot\ProductImages\" + fileName;

                //    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                //    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                //    FileInfo file = new FileInfo(directoryLocation);
                //    if (file.Exists)
                //    {
                //        file.Delete();
                //    }

                //    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                //    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                //    {
                //        productDto.Image.CopyTo(fileStream);
                //    }
                //    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                //    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                //    product.ImageLocalPath = filePath;
                //}
                //else
                //{
                //    product.ImageUrl = "https://placehold.co/600x400";
                //}
                //_db.Products.Update(product);
                //_db.SaveChanges();
                //_response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "ADMIN, EDITOR")]
        public ResponseDto Update([FromBody] ProductDto productDto)
        {
            try
            {
                Product result = _mapper.Map<Product>(productDto);
                _context.Products.Update(result);
                _context.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product result = _context.Products.First(x => x.ProductId == id);
                _context.Products.Remove(result);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
