// <copyright file="GetGeoObjectResponse.cs" company="Company">
//     Copyright (c) 2011-2012, Anton Gubarenko All rights reserved.
//     
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright notice, this list of
//       conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of
//       conditions and the following disclaimer in the documentation and/or other materials
//       provided with the distribution.
//     * Neither the name of Sergey Mudrov nor the names of its contributors may be used to endorse
//       or promote products derived from this software without specific prior written permission.
//     
//     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
//     OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
//     MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL ANTON
//     GUBARENKO BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
//     THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
//     OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//                POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author> Anton Gubarenko </author>

using GisApiWrapper.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DoubleGisApiWrapper.Response
{
	/// <summary>
	/// Class that represents the GeoObject Response. 
	/// </summary>
	[DataContract(Name = "getGeoObjectResponse", Namespace = "")]
	public class GetGeoObjectResponse
	{
		/// <summary>
		/// Gets or sets the API version. 
		/// </summary>
		/// <value> The API version. </value>
		[DataMember(Name = "api_version", Order = 5)]
		public string ApiVersion { get; set; }

		/// <summary>
		/// Gets or sets the response code. 200 if ok, else - error. 
		/// </summary>
		/// <value> The response code. </value>
		[DataMember(Name = "response_code", Order = 5)]
		public string ResponseCode { get; set; }

		/// <summary>
		/// Gets or sets the error code. 
		/// </summary>
		/// <value> The error code. </value>
		[DataMember(Name = "error_code", Order = 5)]
		public string ErrorCode { get; set; }

		/// <summary>
		/// Gets or sets the error message. 
		/// </summary>
		/// <value> The error message. </value>
		[DataMember(Name = "error_message", Order = 5)]
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets the total results. 
		/// </summary>
		/// <value> The total results. </value>
		[DataMember(Name = "total", Order = 5)]
		public string Total { get; set; }

		/// <summary>
		/// Gets or sets the results of the response. 
		/// </summary>
		/// <value> The results of the response. </value>
		[DataMember(Name = "result", Order = 5)]
		public IEnumerable<GeoObject> result { get; set; }
	}
}