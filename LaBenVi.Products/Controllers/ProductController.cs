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
    //[Route("[controller]")]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        //[Authorize(Roles = "ADMIN, EDITOR")]
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
        [AllowAnonymous]
        //[Authorize(Roles = "ADMIN")]
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
