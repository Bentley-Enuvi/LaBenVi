using AutoMapper;
using Azure;
using LaBenVi_CouponAPI.Data;
using LaBenVi_CouponAPI.Models;
using LaBenVi_CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LaBenVi_CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly LaBenViDbContext _context;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponController(LaBenViDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new ResponseDto();
            _mapper = mapper;

        }


        [HttpGet]
        public ResponseDto GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon> result = _context.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(result);
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
                Coupon result = _context.Coupons.First(p => p.CouponId == id);

                _response.Result = _mapper.Map<CouponDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon result = _context.Coupons.FirstOrDefault(w => w.CouponCode.ToLower() == code.ToLower());
                if (result == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon result = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Add(result);
                _context.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(result);
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
        public ResponseDto Update([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon result = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Update(result);
                _context.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(result);
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
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon result = _context.Coupons.First(x => x.CouponId == id);
                _context.Coupons.Remove(result);
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