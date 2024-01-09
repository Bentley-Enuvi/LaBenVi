using LaBenVi_UI.Services.IServices;
using LaBenVi_UI.Utilities;

namespace LaBenVi_UI.Services
{
	public class TokenProvider : ITokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public TokenProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}


		public void SetToken(string token)
		{
			_contextAccessor.HttpContext?.Response.Cookies.Append(Static_Details.TokenCookie, token);
		}

		public void ClearToken()
		{
			_contextAccessor.HttpContext?.Response.Cookies.Delete(Static_Details.TokenCookie);
		}

		public string? GetToken()
		{
			string? token = null;
			bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Static_Details.TokenCookie, out token);
			return hasToken is true ? token : null;
		}

		
	}
}
