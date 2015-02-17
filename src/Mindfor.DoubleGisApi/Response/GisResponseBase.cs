using System.Runtime.Serialization;

namespace Midnfor.DoubleGisApi.Response
{
	[DataContract(Name = "gisResponseBase", Namespace = "")]
	public class GisResponseBase
	{
		/// <summary>
		/// Gets or sets the API version. 
		/// </summary>
		/// <value> The API version. </value>
		[DataMember(Name = "api_version", Order = 1)]
		public string ApiVersion { get; set; }

		/// <summary>
		/// Gets or sets the response code. 200 if ok, else - error. 
		/// </summary>
		/// <value> The response code. </value>
		[DataMember(Name = "response_code", Order = 2)]
		public string ResponseCode { get; set; }

		/// <summary>
		/// Gets or sets the error code. 
		/// </summary>
		/// <value> The error code. </value>
		[DataMember(Name = "error_code", Order = 3)]
		public string ErrorCode { get; set; }

		/// <summary>
		/// Gets or sets the error message. 
		/// </summary>
		/// <value> The error message. </value>
		[DataMember(Name = "error_message", Order = 4)]
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets the total results count. 
		/// </summary>
		/// <value> The total result count. </value>
		[DataMember(Name = "total", Order = 5)]
		public string Total { get; set; }
	}
}