using static LaBenVi_UI.Utilities.Static_Details;

namespace LaBenVi_UI.Models
{
    public class RequestDto
    {
        public ApiAction ApiAction { get; set; } = ApiAction.GET;
        public object Data { get; set; }
        public string AccessToken { get; set; }
        public string Url { get; set; }

		public ContentType ContentType { get; set; } = ContentType.Json;
	}
}
