// <copyright file="GisApi.Sync.cs" company="Company">
//     Copyright (c) 2011, Anton Gubarenko All rights reserved.
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
//     POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author> Anton Gubarenko </author>

using _2GisApi.Response;

namespace GisApiWrapper
{
	using GisApiWrapper.Properties;
	using GisApiWrapper.Responses;
	using Newtonsoft.Json;
	using System;
	using System.Globalization;
	using System.IO;

	/// <summary>
	/// Class that represents API wrapper for 2GIS API. 
	/// </summary>
	public partial class GisApi
	{
		#region Synchronous access to 2GIS API methods.

		/// <summary>
		/// Synchronous access to 2GIS API for gets the geo objects by name. 
		/// </summary>
		/// <param name="q"> The query. Length &gt; 2. </param>
		/// <param name="types"> The types array. See GeoTypes. Unnecessary parameter. </param>
		/// <param name="project"> The project ID to search in. Unnecessary parameter. </param>
		/// <returns> The response from API. </returns>
		public ApiResponse<GetGeoObjectResponse> GetGeoObjectsSync(string q, string[] types = null, string project = null)
		{
			if (string.IsNullOrEmpty(q))
			{
				return new ApiResponse<GetGeoObjectResponse> { Error = new ArgumentNullException("q"), IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError };
			}

			string typesData = "";
			if (types != null && types.Length > 0)
			{
				typesData = string.Join(",", types);
				typesData = "&types=" + typesData;
			}

			string projectData = "";
			if (!string.IsNullOrEmpty(project))
			{
				projectData = "&project=" + project;
			}

			string request = string.Format(
				CultureInfo.CurrentCulture,
				"{0}geo/search?q={1}{2}{3}&key={4}&version=1.3&output=json",
				this._apiEndpoint,
				q.Trim().ToLower(CultureInfo.CurrentCulture),
				typesData,
				projectData,
				this._secretKey);

			var uri = new Uri(request);

			// Performing synchronous call open-read operations. 
			if (!this._client.IsBusy)
			{
				Stream responseStream = this._client.OpenRead(uri);

				try
				{
					////Getting result stream
					JsonSerializer _jsonDeserializer = new JsonSerializer();
					JsonTextReader _jsonReader = new JsonTextReader(new StreamReader(responseStream));

					var resp = _jsonDeserializer.Deserialize<GetGeoObjectResponse>(_jsonReader);

					return new ApiResponse<GetGeoObjectResponse> { Error = null, IsCanceled = false, Response = resp, InfoMessage = "OK" };
				}
				finally
				{
					if (responseStream != null)
					{
						responseStream.Close();
					}
				}
			}

			return new ApiResponse<GetGeoObjectResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError };
		}

		#endregion Synchronous access to 2GIS API methods.
	}
}