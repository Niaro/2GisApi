// <copyright file="GisApi.cs" company="Mindfor">
//     Copyright (c) 2015-2012, George Evstigneev All rights reserved.
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
//     MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GEORGE
//     EVSTIGNEEV BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
//     THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
//     OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//                POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author> George Evstigneev </author>

using Midnfor.DoubleGisApi.Response;
using Midnfor.DoubleGisApi.Types;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Midnfor.DoubleGisApi
{
	public class ErrorMsgs
	{
		public const string BoundError = "Bound must contains 2 points";
		public const string PagesizeError = "Page size must be in 5..50";
		public const string PointError = "Point must be set";
		public const string RadiusError = "Radius must be in 1..40000";
		public const string WhatError = "What length must be > 2";
		public const string WhereError = "Where length must be > 2";
		public const string WrongParamsError = "Wrong parameters";
	}

	/// <summary>
	/// Class that represents API wrapper for 2GIS API. 
	/// </summary>
	public partial class GisApi : IDisposable
	{
		#region Fields

		/// <summary>
		/// Secret key that you can obtain here: http://partner.api.2gis.ru/ 
		/// </summary>
		private readonly string _secretKey;

		/// <summary>
		/// API endpoint. 
		/// </summary>
		private readonly string _apiEndpoint;

		/// <summary>
		/// API endpoint. 
		/// </summary>
		private readonly string _apiTrackerEndpoint;

		/// <summary>
		/// API endpoint. 
		/// </summary>
		private readonly string _apiAdsEndpoint;

		/// <summary>
		/// HttpClient object to make API calls. 
		/// </summary>
		private readonly HttpClient _client;

		#endregion Fields

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="GisApi"/> class. 
		/// </summary>
		/// <param name="key"> The secret key. </param>
		public GisApi(string key)
		{
			this._client = new HttpClient();

			this._secretKey = key;
			this._apiEndpoint = "http://catalog.api.2gis.ru/";
			this._apiTrackerEndpoint = "http://stat.api.2gis.ru/?v=1.3&hash=";
			this._apiAdsEndpoint = "http://catalog.api.2gis.ru/ads/";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GisApi"/> class. 
		/// </summary>
		/// <param name="key"> The secret key. </param>
		/// <param name="client"> The client object for async operations. </param>
		public GisApi(string key, HttpClient client)
		{
			this._client = client ?? new HttpClient();
			this._secretKey = key;
			this._apiEndpoint = "http://catalog.api.2gis.ru/";
			this._apiTrackerEndpoint = "http://stat.api.2gis.ru/?v=1.3&hash=";
			this._apiAdsEndpoint = "http://catalog.api.2gis.ru/ads/";
		}

		#endregion Constructor

		#region Delegates

		/// <summary>
		/// Callback for API calls. So you don't have to get type of each response. 
		/// </summary>
		/// <typeparam name="T"> Response type. </typeparam>
		/// <param name="resp"> The response object. </param>
		public delegate void ApiCallbackType<T>(ApiResponse<T> resp);

		#endregion Delegates

		#region Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting
		/// unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			////Releasing HttpClient
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources 
		/// </summary>
		/// <param name="removeResources">
		/// <c> true </c> to release both managed and unmanaged resources; <c> false </c> to release
		/// only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool removeResources)
		{
			if (removeResources)
			{
				////Releasing HttpClient
				_client?.Dispose();
			}
		}

		/// <summary>
		/// Finds the firms by what and where parameters. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="where"> The where field. Length &gt; 2. </param>
		/// <param name="radius">
		/// The radius of search. Values from 1 to 40000. 250 by default. Unnecessary parameter.
		/// </param>
		/// <param name="page"> The number of requested page. Unnecessary parameter. </param>
		/// <param name="pagesize">
		/// The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.
		/// </param>
		/// <param name="sort"> The sorting key. Unnecessary parameter. </param>
		/// <param name="filters"> The additional sorting filter by working time. </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirms(string what, string where, long? radius, int? page, int? pagesize, SortDirections? sort, string[] filters)
		{
			var result = new ApiResponse<FirmSearchResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking what and where fields
			if (what.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhatError;
				return result;
			}

			////Checking where range
			if (where.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhereError;
				return result;
			}

			////Checking radius range
			if (radius.HasValue)
			{
				if (radius.Value < 1 || radius.Value > 40000)
				{
					result.InfoMessage = ErrorMsgs.RadiusError;
					return result;
				}
			}

			////Checking pagesize range
			if (pagesize.HasValue)
			{
				if (pagesize.Value < 5 || pagesize.Value > 50)
				{
					result.InfoMessage = ErrorMsgs.PagesizeError;
					return result;
				}
			}

			string sortType = String.Empty;
			////Checking sort
			if (sort.HasValue)
			{
				sortType = Enum.GetName(typeof(SortDirections), sort);
			}

			string filtersArray = "";
			/////Checking filters
			if (filters != null && filters.Length > 0 && !filters.Equals(String.Empty))
			{
				filtersArray = filters.Aggregate(filtersArray, (current, filter) => current + ("&filters[worktime]=" + filter));
			}

			////If all passed
			if (what.Trim().Length > 2 && where.Trim().Length > 2)
			{
				var request = String.Format(
					CultureInfo.CurrentCulture,
					this._apiEndpoint + "search?what={0}&where={1}&radius={2}&page={3}&pagesize={4}&key={5}&version=1.3&sort={6}{7}&output=json",
					what.Trim().ToLower(),
					where.Trim().ToLower(),
					radius?.ToString(CultureInfo.CurrentCulture) ?? string.Empty,
					page?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
					pagesize?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
					this._secretKey,
					String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
					filtersArray);
				var uri = new Uri(request);
				try
				{
					var response = await _client.GetAsync(uri);
					return await HandleResponse<FirmSearchResponse>(response);
				}
				catch (HttpRequestException ex)
				{
					return new ApiResponse<FirmSearchResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
				}
			}
			return result;
		}

		/// <summary>
		/// Finds the firms by what and point parameters. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="point"> The coordinates of the serach area in WGS84 format. </param>
		/// <param name="radius">
		/// The radius of search. Values from 1 to 40000. 250 by default. Unnecessary parameter.
		/// </param>
		/// <param name="page"> The number of requested page. Unnecessary parameter. </param>
		/// <param name="pagesize">
		/// The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.
		/// </param>
		/// <param name="sort"> The sorting key. Unnecessary parameter. </param>
		/// <param name="filters"> The additional sorting filter by working time. </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirms(string what, GeoPoint point, long? radius, int? page, int? pagesize, SortDirections? sort, string[] filters)
		{
			var result = new ApiResponse<FirmSearchResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking what and where fields
			if (what.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhatError;
				return result;
			}

			////Checking radius range
			if (radius.HasValue)
			{
				if (radius.Value < 1 || radius.Value > 40000)
				{
					result.InfoMessage = ErrorMsgs.RadiusError;
					return result;
				}
			}

			////Checking pagesize range
			if (pagesize.HasValue)
			{
				if (pagesize.Value < 5 || pagesize.Value > 50)
				{
					result.InfoMessage = ErrorMsgs.PagesizeError;
					return result;
				}
			}

			string sortType = String.Empty;
			////Checking sort
			if (sort.HasValue)
			{
				sortType = Enum.GetName(typeof(SortDirections), sort);
			}

			string filtersArray = "";
			/////Checking filters
			if (filters != null && filters.Length > 0 && !filters.Equals(String.Empty))
			{
				filtersArray = filters.Aggregate(filtersArray, (current, filter) => current + ("&filters[worktime]=" + filter));
			}

			////If all passed
			if (what.Trim().Length > 2)
			{
				string request = String.Format(
					CultureInfo.CurrentCulture,
					this._apiEndpoint + "search?what={0}&where=&point={1}&radius={2}&page={3}&pagesize={4}&key={5}&version=1.3&sort={6}{7}&output=json",
					what.Trim().ToLower(),
					point,
					radius?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
					page?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
					pagesize?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
					this._secretKey,
					String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
					filtersArray);
				var uri = new Uri(request);
				try
				{
					var response = await _client.GetAsync(uri);
					return await HandleResponse<FirmSearchResponse>(response);
				}
				catch (HttpRequestException ex)
				{
					return new ApiResponse<FirmSearchResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
				}
			}
			return result;
		}

		/// <summary>
		/// Finds the firms in specific region. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="bound">
		/// The bounds of the region. Must contatin 2 points. Upper left and lower right.
		/// </param>
		/// <param name="page"> The number of requested page. Unnecessary parameter. </param>
		/// <param name="pagesize">
		/// The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.
		/// </param>
		/// <param name="sort"> The sorting key. Unnecessary parameter. </param>
		/// <param name="filters"> The additional sorting filter by working time. </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirms(string what, Bound bound, int? page, int? pagesize, SortDirections? sort, string[] filters)
		{
			var result = new ApiResponse<FirmSearchResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking what and where fields
			if (what.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhatError;
				return result;
			}

			////Checking pagesize range
			if (pagesize.HasValue)
			{
				if (pagesize.Value < 5 || pagesize.Value > 50)
				{
					result.InfoMessage = ErrorMsgs.PagesizeError;
					return result;
				}
			}
			////Checking bounds
			if (bound.UpperLeftPoint == null || bound.LowerRightPoint == null)
			{
				result.InfoMessage = ErrorMsgs.BoundError;
				return result;
			}

			string sortType = String.Empty;
			////Checking sort
			if (sort.HasValue)
			{
				sortType = Enum.GetName(typeof(SortDirections), sort);
			}

			string filtersArray = "";
			/////Checking filters
			if (filters != null && filters.Length > 0 && !filters.Equals(String.Empty))
			{
				foreach (var filter in filters)
				{
					filtersArray += "&filters[worktime]=" + filter;
				}
			}

			////If all passed
			if (what.Trim().Length > 2)
			{
				string request = String.Format(
					CultureInfo.CurrentCulture,
					this._apiEndpoint + "search?what={0}&where=&point=&bound[point1]={1}&bound[point2]={2}{3}{4}&key={5}&version=1.3{6}{7}&output=json",
					what.Trim().ToLower(),
					bound.UpperLeftPoint,
					bound.LowerRightPoint,
					page.HasValue ? "&page=" + page.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
					pagesize.HasValue ? "&pagesize=" + pagesize.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
					this._secretKey,
					String.IsNullOrEmpty(sortType) ? String.Empty : "&sort=" + sortType,
					filtersArray);
				var uri = new Uri(request);
				try
				{
					var response = await _client.GetAsync(uri);
					return await HandleResponse<FirmSearchResponse>(response);
				}
				catch (HttpRequestException ex)
				{
					return new ApiResponse<FirmSearchResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
				}
			}
			return result;
		}

		/// <summary>
		/// Finds the firms in specific region. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="bound">
		/// The bounds of the region. Must contatin 2 points. Upper left and lower right.
		/// </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirms(string what, Bound bound)
		{
			return await FindFirms(what, bound, null, null, null, null);
		}

		/// <summary>
		/// Finds the firms by What and Where. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="where"> The where field. Length &gt; 2. </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirms(string what, string where)
		{
			var errorResult = new ApiResponse<FirmSearchResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking what and where fields
			if (what.Trim().Length < 3)
			{
				errorResult.InfoMessage = ErrorMsgs.WhatError;
				return errorResult;
			}

			////Checking where range
			if (where.Trim().Length < 3)
			{
				errorResult.InfoMessage = ErrorMsgs.WhereError;
				return errorResult;
			}
			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiEndpoint + "search?what={0}&where={1}&key={2}&version=1.3&output=json",
				what.Trim().ToLower(),
				where.Trim().ToLower(),
				this._secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<FirmSearchResponse>(response);
			}
			catch (Exception ex)
			{
				return new ApiResponse<FirmSearchResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the rubrics by city name. 
		/// </summary>
		/// <param name="where"> The city name. </param>
		public async Task<ApiResponse<GetRubricResponse>> GetRubrics(string where)
		{
			var result = new ApiResponse<GetRubricResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking where range
			if (where.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhereError;
				return result;
			}
			////If all passed
			if (where.Trim().Length > 2)
			{
				string request = String.Format(
					CultureInfo.CurrentCulture,
					this._apiEndpoint + "rubricator?where={0}&key={1}&version=1.3&output=json",
					where.Trim().ToLower(),
					this._secretKey);
				var uri = new Uri(request);
				try
				{
					var response = await _client.GetAsync(uri);
					return await HandleResponse<GetRubricResponse>(response);
				}
				catch (HttpRequestException ex)
				{
					return new ApiResponse<GetRubricResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the rubrics by city name or by rubric ID. 
		/// </summary>
		/// <param name="where"> The city name. </param>
		/// <param name="id"> The rubric ID. </param>
		/// <param name="parentId"> The parent rubric id. </param>
		/// <param name="showChildren"> Should show children flag. 1 - to show, any other - no. </param>
		/// <param name="sort">
		/// The sorting key. Supported: name - alphabetical sort, popularity - by popularity value.
		/// </param>
		public async Task<ApiResponse<GetRubricResponse>> GetRubrics(string where, string id, string parentId, int? showChildren, SortDirections? sort)
		{
			var result = new ApiResponse<GetRubricResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking where range
			if (where.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhereError;
				return result;
			}

			string sortType = String.Empty;
			////Checking sort
			if (sort.HasValue)
			{
				sortType = Enum.GetName(typeof(SortDirections), sort);
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiEndpoint + "rubricator?where={0}&id={1}&parent_id={2}&key={3}&sort={4}{5}&version=1.3&output=json",
				where.Trim().ToLower(),
				String.IsNullOrEmpty(id) ? String.Empty : id,
				String.IsNullOrEmpty(parentId) ? String.Empty : parentId,
				this._secretKey,
				String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
				showChildren != null ? "&show_children=" + showChildren : "");
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetRubricResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetRubricResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Finds the firms in selected rubric. 
		/// </summary>
		/// <param name="rubric"> The rubric for search. </param>
		/// <param name="where"> The where field. Length &gt; 2. </param>
		/// <param name="radius">
		/// The radius of search. Values from 1 to 40000. 250 by default. Unnecessary parameter.
		/// </param>
		/// <param name="page"> The number of requested page. Unnecessary parameter. </param>
		/// <param name="pagesize">
		/// The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.
		/// </param>
		/// <param name="sort"> The sorting key. Unnecessary parameter. </param>
		/// <param name="filters"> The additional sorting filter by working time. </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirmsInRubric(string rubric, string where, long? radius, int? page, int? pagesize, SortDirections? sort, string[] filters)
		{
			var result = new ApiResponse<FirmSearchResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking what and where fields
			if (rubric.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhatError;
				return result;
			}

			////Checking where range
			if (where.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhereError;
				return result;
			}

			////Checking radius range
			if (radius.HasValue)
			{
				if (radius.Value < 1 || radius.Value > 40000)
				{
					result.InfoMessage = ErrorMsgs.RadiusError;
					return result;
				}
			}

			////Checking pagesize range
			if (pagesize.HasValue)
			{
				if (pagesize.Value < 5 || pagesize.Value > 50)
				{
					result.InfoMessage = ErrorMsgs.PagesizeError;
					return result;
				}
			}

			string sortType = String.Empty;
			////Checking sort
			if (sort.HasValue)
			{
				sortType = Enum.GetName(typeof(SortDirections), sort);
			}

			string filtersArray = "";
			/////Checking filters
			if (filters != null && filters.Length > 0 && !filters.Equals(String.Empty))
			{
				filtersArray = filters.Aggregate(filtersArray, (current, filter) => current + ("&filters[worktime]=" + filter));
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiEndpoint + "searchinrubric?what={0}&where={1}&radius={2}&page={3}&pagesize={4}&key={5}&version=1.3&sort={6}{7}&output=json",
				rubric.Trim().ToLower(),
				where.Trim().ToLower(),
				radius?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
				page?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
				pagesize?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
				this._secretKey,
				String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
				filtersArray);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<FirmSearchResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<FirmSearchResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Finds the firms in selected rubric. 
		/// </summary>
		/// <param name="rubric"> The rubric for search. </param>
		/// <param name="where"> The where field. Length &gt; 2. </param>
		/// <param name="callback"> The callback. </param>
		public async Task<ApiResponse<FirmSearchResponse>> FindFirmsInRubric(string rubric, string where)
		{
			var result = new ApiResponse<FirmSearchResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////Checking what and where fields
			if (rubric.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhatError;
				return result;
			}

			////Checking where range
			if (where.Trim().Length < 3)
			{
				result.InfoMessage = ErrorMsgs.WhereError;
				return result;
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiEndpoint + "searchinrubric?what={0}&where={1}&key={2}&version=1.3&output=json",
				rubric.Trim().ToLower(),
				where.Trim().ToLower(),
				this._secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<FirmSearchResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<FirmSearchResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the firm info by it's ID and HASH. 
		/// </summary>
		/// <param name="id"> The id of the firm. </param>
		/// <param name="hash"> The unique hash of th e firm. </param>
		public async Task<ApiResponse<FirmProfileResponse>> GetFirmInfo(string id, string hash)
		{
			var result = new ApiResponse<FirmProfileResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////If all passed
			if (id.Trim().Length <= 2 || hash.Trim().Length <= 2) return result;

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiEndpoint + "profile?id={0}&hash={1}&key={2}&version=1.3&output=json",
				id.Trim().ToLower(),
				hash.Trim().ToLower(),
				this._secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<FirmProfileResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<FirmProfileResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the firms branches by firm ID. 
		/// </summary>
		/// <param name="firmid"> The firm ID. </param>
		public async Task<ApiResponse<BranchesOfFirmResponse>> GetFirmsBranches(string firmid)
		{
			var errorResult = new ApiResponse<BranchesOfFirmResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////If all passed
			if (firmid.Trim().Length <= 2) return errorResult;

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiEndpoint + "firmsByFilialId?firmid={0}&key={1}&version=1.3&output=json",
				firmid.Trim().ToLower(),
				this._secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<BranchesOfFirmResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<BranchesOfFirmResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Registers the firm profile views. 
		/// </summary>
		/// <param name="hash">
		/// The hash of the firm. Can be get from register_bc_url field of the firm's profile.
		/// </param>
		public async Task<ApiResponse<int>> RegisterProfileViews(string hash)
		{
			var errorResult = new ApiResponse<int> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };
			////If all passed
			if (hash.Trim().Length <= 2) return errorResult;

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiTrackerEndpoint + hash);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<int>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<int> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the projects. 
		/// </summary>
		public async Task<ApiResponse<GetProjectResponse>> GetProjects()
		{
			string request = String.Format(
				CultureInfo.CurrentCulture, "http://catalog.api.2gis.ru/project/list?version=1.3&output=json&key={0}",
				_secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetProjectResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetProjectResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the cities in project. 
		/// </summary>
		/// <param name="where"> The city name. </param>
		public async Task<ApiResponse<GetCitiesResponse>> GetCitiesInProject(string where)
		{
			var errorResult = new ApiResponse<GetCitiesResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking where range
			if (where.Trim().Length < 3)
			{
				errorResult.InfoMessage = ErrorMsgs.WhereError;
				return errorResult;
			}
			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/city/list?version=1.3&key={0}&where={1}",
				_secretKey,
				where.Trim().ToLower());
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetCitiesResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetCitiesResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the cities in project by project ID. 
		/// </summary>
		/// <param name="id"> The project id </param>
		public async Task<ApiResponse<GetCitiesResponse>> GetCitiesInProjectById(string id)
		{
			var errorResult = new ApiResponse<GetCitiesResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking where range
			if (id.Trim().Length < 1)
			{
				errorResult.InfoMessage = ErrorMsgs.WhereError;
				return errorResult;
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/city/list?version=1.3&key={0}&project_id={1}",
				_secretKey,
				id.Trim().ToLower());
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetCitiesResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetCitiesResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the geo objects by name. 
		/// </summary>
		/// <param name="q"> The query. Length &gt; 2. </param>
		public async Task<ApiResponse<GetGeoObjectResponse>> GetGeoObjects(string q, ApiCallbackType<GetGeoObjectResponse> callback)
		{
			var errorResult = new ApiResponse<GetGeoObjectResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking where range
			if (q.Trim().Length < 1)
			{
				errorResult.InfoMessage = ErrorMsgs.WhereError;
				return errorResult;
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/geo/search?q={0}&key={1}&version=1.3&output=json",
				q.Trim().ToLower(),
				_secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetGeoObjectResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetGeoObjectResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the geo objects by name. 
		/// </summary>
		/// <param name="q"> The query. Length &gt; 2. </param>
		/// <param name="types"> The types array. See GeoTypes. Unnecessary parameter. </param>
		/// <param name="limit"> The limit of results. By default: 1. Unnecessary parameter. </param>
		public async Task<ApiResponse<GetGeoObjectResponse>> GetGeoObjects(string q, string[] types, long? limit)
		{
			var errorResult = new ApiResponse<GetGeoObjectResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking where range
			if (q.Trim().Length < 1)
			{
				errorResult.InfoMessage = ErrorMsgs.WhereError;
				return errorResult;
			}
			//Getting types array
			var typesData = "";
			if (types != null && types.Length > 0)
			{
				typesData = types.Aggregate(typesData, (current, type) => current + (type + ","));
				typesData = typesData.Substring(0, typesData.Length - 1);
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/geo/search?q={0}&types={1}&limit={2}&key={3}&version=1.3&output=json",
				q.Trim().ToLower(),
				String.IsNullOrEmpty(typesData) ? String.Empty : typesData,
				limit?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
				_secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetGeoObjectResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetGeoObjectResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the geo objects by coordinats. 
		/// </summary>
		/// <param name="point"> The coordinates of the serach area in WGS84 format. </param>
		public async Task<ApiResponse<GetGeoObjectResponse>> GetGeoObjects(GeoPoint point)
		{
			var errorResult = new ApiResponse<GetGeoObjectResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking where range
			if (point == null)
			{
				errorResult.InfoMessage = ErrorMsgs.PointError;
				return errorResult;
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/geo/search?q={0}&key={1}&version=1.3&output=json",
				point,
				_secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetGeoObjectResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetGeoObjectResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the geo objects by coordinats. 
		/// </summary>
		/// <param name="point"> The coordinates of the serach area in WGS84 format. </param>
		/// <param name="types"> The types array. See GeoTypes. Unnecessary parameter. </param>
		/// <param name="radius">
		/// The search radius in meters. Values from 1 to 250. Unnecessary parameter.
		/// </param>
		/// <param name="project"> The project ID to search in. Unnecessary parameter. </param>
		public async Task<ApiResponse<GetGeoObjectResponse>> GetGeoObjects(GeoPoint point, string[] types, long? radius, string project)
		{
			var errorResult = new ApiResponse<GetGeoObjectResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking where range
			if (point == null)
			{
				errorResult.InfoMessage = ErrorMsgs.PointError;
				return errorResult;
			}
			////Checking radius range
			if (radius.HasValue)
			{
				if (radius.Value < 1 || radius.Value > 250)
				{
					errorResult.InfoMessage = ErrorMsgs.RadiusError;
					return errorResult;
				}
			}
			//Getting types array
			var typesData = "";
			if (types != null && types.Length > 0)
			{
				typesData = types.Aggregate(typesData, (current, type) => current + (type + ","));
				typesData = typesData.Substring(0, typesData.Length - 1);
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/geo/search?q={0}&types={1}&radius={2}&project={3}&key={4}&version=1.3&output=json",
				point,
				String.IsNullOrEmpty(typesData) ? String.Empty : typesData,
				radius?.ToString(CultureInfo.CurrentCulture) ?? String.Empty,
				String.IsNullOrEmpty(project) ? String.Empty : project,
				_secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetGeoObjectResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetGeoObjectResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Gets the geo object info. 
		/// </summary>
		/// <param name="id"> The id if geo object. </param>
		public async Task<ApiResponse<GetGeoObjectResponse>> GetGeoObjectInfo(string id)
		{
			var errorResult = new ApiResponse<GetGeoObjectResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			if (id.Trim().Length <= 2) return errorResult;

			string request = String.Format(
				CultureInfo.CurrentCulture,
				"http://catalog.api.2gis.ru/geo/get?id={0}&key={1}&version=1.3&output=json",
				id,
				_secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<GetGeoObjectResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<GetGeoObjectResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Finds the ads by what and where parameters. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="where"> The where field. Length &gt; 2. </param>
		/// <param name="format"> The format of the resonse. Short or full.. </param>
		/// <param name="page"> The number of requested page. Unnecessary parameter. </param>
		/// <param name="pagesize">
		/// The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.
		/// </param>
		public async Task<ApiResponse<FindAdsResponse>> FindAds(string what, string where, string format, int? page, int? pagesize)
		{
			var errorResult = new ApiResponse<FindAdsResponse> { IsCanceled = true, InfoMessage = ErrorMsgs.WrongParamsError };

			////Checking what and where fields
			if (what.Trim().Length < 3)
			{
				errorResult.InfoMessage = ErrorMsgs.WhatError;
				return errorResult;
			}

			////Checking where range
			if (where.Trim().Length < 3)
			{
				errorResult.InfoMessage = ErrorMsgs.WhereError;
				return errorResult;
			}
			////Checking pagesize range
			if (pagesize.HasValue)
			{
				if (pagesize.Value < 5 || pagesize.Value > 50)
				{
					errorResult.InfoMessage = ErrorMsgs.PagesizeError;
					return errorResult;
				}
			}

			string request = String.Format(
				CultureInfo.CurrentCulture,
				this._apiAdsEndpoint + "search?what={0}&where={1}{2}{3}{4}&key={5}&version=1.3&output=json",
				what.Trim().ToLower(),
				where.Trim().ToLower(),
				page.HasValue ? "&page=" + page.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
				pagesize.HasValue ? "&pagesize=" + pagesize.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
				string.IsNullOrEmpty(format) ? string.Empty : "&format=" + format,
				this._secretKey);
			var uri = new Uri(request);
			try
			{
				var response = await _client.GetAsync(uri);
				return await HandleResponse<FindAdsResponse>(response);
			}
			catch (HttpRequestException ex)
			{
				return new ApiResponse<FindAdsResponse> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
			}
		}

		/// <summary>
		/// Finds the ads by what and where parameters. 
		/// </summary>
		/// <param name="what"> The what field. Length &gt; 2. </param>
		/// <param name="where"> The where field. Length &gt; 2. </param>
		public async Task<ApiResponse<FindAdsResponse>> FindAds(string what, string where)
		{
			return await FindAds(what, where, null, null, null);
		}

		private static async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response) where T : IGisResponseBase
		{
			if (response.IsSuccessStatusCode)
			{
				try
				{
					var contentStream = await response.Content.ReadAsStreamAsync();
					var jsonSerializer = new JsonSerializer();
					var sr = new StreamReader(contentStream);
					IGisResponseBase resp = (T)jsonSerializer.Deserialize(sr, typeof(T));

					var apiResponse = new ApiResponse<T> { Response = (T)resp, InfoMessage = response.ReasonPhrase };
					if (resp.ErrorCode == "200") apiResponse.IsSuccess = true;

					return apiResponse;
				}
				catch (JsonSerializationException ex)
				{
					return new ApiResponse<T> { Error = ex, IsCanceled = true, InfoMessage = ex.Message };
				}
				finally
				{
					if (response.Content != null)
					{
						response.Dispose();
					}
				}
			}
			else
			{
				return new ApiResponse<T> { IsCanceled = true, InfoMessage = response.ReasonPhrase };
			}
		}

		#endregion Methods
	}
}