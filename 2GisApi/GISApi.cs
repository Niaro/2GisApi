// <copyright file="GisApi.cs" company="Company">
//
//    Copyright (c) 2011-2012, Anton Gubarenko
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without
//    modification, are permitted provided that the following conditions are met:
//        * Redistributions of source code must retain the above copyright
//          notice, this list of conditions and the following disclaimer.
//        * Redistributions in binary form must reproduce the above copyright
//          notice, this list of conditions and the following disclaimer in the
//          documentation and/or other materials provided with the distribution.
//        * Neither the name of Sergey Mudrov nor the
//          names of its contributors may be used to endorse or promote products
//          derived from this software without specific prior written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//    DISCLAIMED. IN NO EVENT SHALL ANTON GUBARENKO BE LIABLE FOR ANY
//    DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//    ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//    (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// </copyright>
// <author>Anton Gubarenko</author>
// <remarks>
// 2014.05.23 Make class GisApi partial, add methods for synchronous access 2GIS API.
// </remarks>

using _2GisApi.Response;

namespace GisApiWrapper
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;

    using GisApiWrapper.Properties;
    using GisApiWrapper.Responses;

    using Newtonsoft.Json;

    /// <summary>
    /// Class that represents API wrapper for 2GIS API.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "JSON deserialization properties must be identical to API names")]
    [CLSCompliant(true)]
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
        /// WebClient object to make API calls.
        /// </summary>
        private readonly WebClient _client;

        /// <summary>
        /// Request type. Neede to serialize proper objects. 0 - FirmSearchResponse, 1 - FirmProfileResponse, 2 - FirmsSearchByFillialResponse
        /// </summary>
        private int _requestType = -1;

        /// <summary>
        /// Callback for FirmSearchResponse.
        /// </summary>
        private ApiCallbackType<FirmSearchResponse> _call;

        /// <summary>
        /// Callback for FirmProfileResponse.
        /// </summary>
        private ApiCallbackType<FirmProfileResponse> _call1;

        /// <summary>
        /// Callback for FirmsSearchByFillialResponse.
        /// </summary>
        private ApiCallbackType<BranchesOfFirmResponse> _call2;

        /// <summary>
        /// Callback for GetRubricsResponse.
        /// </summary>
        private ApiCallbackType<GetRubricResponse> _call3;

        /// <summary>
        /// Callback for RegisterProfileViews.
        /// </summary>
        private ApiCallbackType<int> _call4;

        /// <summary>
        /// Callback for GetProjectResponse.
        /// </summary>
        private ApiCallbackType<GetProjectResponse> _call5;

        /// <summary>
        /// Callback for GetGeoObjectResponse.
        /// </summary>
        private ApiCallbackType<GetCitiesResponse> _call6;

        /// <summary>
        /// Callback for GetGeoObjectResponse.
        /// </summary>
        private ApiCallbackType<GetGeoObjectResponse> _call7;

        /// <summary>
        /// Callback for FindAdsresponse.
        /// </summary>
        private ApiCallbackType<FindAdsResponse> _call8;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GisApi"/> class.
        /// </summary>
        /// <param name="key">The secret key.</param>
        public GisApi(string key)
        {
            this._client = new WebClient();
            this._client.OpenReadCompleted += this.clientFindFirms;
            this._secretKey = key;
            this._apiEndpoint = "http://catalog.api.2gis.ru/";
            this._apiTrackerEndpoint = "http://stat.api.2gis.ru/?v=1.3&hash=";
            this._apiAdsEndpoint = "http://catalog.api.2gis.ru/ads/";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GisApi"/> class.
        /// </summary>
        /// <param name="key">The secret key.</param>
        /// <param name="client">The client object for async operations.</param>
        public GisApi(string key, WebClient client)
        {
            this._client = client ?? new WebClient();
            this._client.OpenReadCompleted += this.clientFindFirms;
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
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="resp">The response object.</param>
        public delegate void ApiCallbackType<T>(ApiResponse<T> resp);

        #endregion Delegates

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ////Releasing WebClient
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="removeResources"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool removeResources)
        {
            if (removeResources)
            {
                ////Releasing WebClient
                if (_client != null)
                {
                    _client.Dispose();
                }
            }
        }

        /// <summary>
        /// Finds the firms by what and where parameters.
        /// </summary>
        /// <param name="what">The what field. Length > 2.</param>
        /// <param name="where">The where field. Length > 2.</param>
        /// <param name="radius">The radius of search. Values from 1 to 40000. 250 by default. Unnecessary parameter.</param>
        /// <param name="page">The number of requested page. Unnecessary parameter.</param>
        /// <param name="pagesize">The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.</param>
        /// <param name="sort">The sorting key. Unnecessary parameter.</param>
        /// <param name="filters">The additional sorting filter by working time.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirms(string what, string where, long? radius, int? page, int? pagesize, SortDirections? sort, string[] filters, ApiCallbackType<FirmSearchResponse> callback)
        {
            ////Assigning callback
            this._call = callback;
            ////Checking what and where fields
            if (what.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }

            ////Checking where range
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }

            ////Checking radius range
            if (radius.HasValue)
            {
                if (radius.Value < 1 || radius.Value > 40000)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.RadiusError });
                }
            }

            ////Checking pagesize range
            if (pagesize.HasValue)
            {
                if (pagesize.Value < 5 || pagesize.Value > 50)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PagesizeError });
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
                foreach (var filter in filters)
                {
                    filtersArray += "&filters[worktime]=" + filter;
                }
            }

            ////If all passed
            if (what.Trim().Length > 2 && where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "search?what={0}&where={1}&radius={2}&page={3}&pagesize={4}&key={5}&version=1.3&sort={6}{7}&output=json",
                    what.Trim().ToLower(CultureInfo.CurrentCulture),
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    radius.HasValue ? radius.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    page.HasValue ? page.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    pagesize.HasValue ? pagesize.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    this._secretKey,
                    String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
                    filtersArray);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 0;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the firms by what and point parameters.
        /// </summary>
        /// <param name="what">The what field. Length > 2.</param>
        /// <param name="point">The coordinates of the serach area in WGS84 format.</param>
        /// <param name="radius">The radius of search. Values from 1 to 40000. 250 by default. Unnecessary parameter.</param>
        /// <param name="page">The number of requested page. Unnecessary parameter.</param>
        /// <param name="pagesize">The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.</param>
        /// <param name="sort">The sorting key. Unnecessary parameter.</param>
        /// <param name="filters">The additional sorting filter by working time.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirms(string what, GeoPoint point, long? radius, int? page, int? pagesize, SortDirections? sort, string[] filters, ApiCallbackType<FirmSearchResponse> callback)
        {
            ////Assigning callback
            this._call = callback;
            ////Checking what and where fields
            if (what.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }

            ////Checking radius range
            if (radius.HasValue)
            {
                if (radius.Value < 1 || radius.Value > 40000)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.RadiusError });
                }
            }

            ////Checking pagesize range
            if (pagesize.HasValue)
            {
                if (pagesize.Value < 5 || pagesize.Value > 50)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PagesizeError });
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
                    this._apiEndpoint + "search?what={0}&where=&point={1}&radius={2}&page={3}&pagesize={4}&key={5}&version=1.3&sort={6}{7}&output=json",
                    what.Trim().ToLower(CultureInfo.CurrentCulture),
                    point,
                    radius.HasValue ? radius.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    page.HasValue ? page.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    pagesize.HasValue ? pagesize.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    this._secretKey,
                    String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
                    filtersArray);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 0;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the firms in specific region.
        /// </summary>
        /// <param name="what">The what field. Length > 2.</param>
        /// <param name="bound">The bounds of the region. Must contatin 2 points. Upper left and lower right.</param>
        /// <param name="page">The number of requested page. Unnecessary parameter.</param>
        /// <param name="pagesize">The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.</param>
        /// <param name="sort">The sorting key. Unnecessary parameter.</param>
        /// <param name="filters">The additional sorting filter by working time.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirms(string what, Bound bound, int? page, int? pagesize, SortDirections? sort, string[] filters, ApiCallbackType<FirmSearchResponse> callback)
        {
            ////Assigning callback
            this._call = callback;
            ////Checking what and where fields
            if (what.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }

            ////Checking bounds
            if (bound.UpperLeftPoint == null || bound.LowerRightPoint == null)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.BoundError });
            }

            ////Checking pagesize range
            if (pagesize.HasValue)
            {
                if (pagesize.Value < 5 || pagesize.Value > 50)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PagesizeError });
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
                    what.Trim().ToLower(CultureInfo.CurrentCulture),
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
                    ////Setting request type
                    this._requestType = 0;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the firms in specific region.
        /// </summary>
        /// <param name="what">The what field. Length > 2.</param>
        /// <param name="bound">The bounds of the region. Must contatin 2 points. Upper left and lower right.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirms(string what, Bound bound, ApiCallbackType<FirmSearchResponse> callback)
        {
            FindFirms(what, bound, null, null, null, null, callback);
        }

        /// <summary>
        /// Finds the firms by What and Where.
        /// </summary>
        /// <param name="what">The what field. Length > 2.</param>
        /// <param name="where">The where field. Length > 2.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirms(string what, string where, ApiCallbackType<FirmSearchResponse> callback)
        {
            ////Assigning callback
            this._call = callback;
            ////Checking what and where fields
            if (what.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }

            ////Checking where range
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }

            ////If all passed
            if (what.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "search?what={0}&where={1}&&key={2}&version=1.3&output=json",
                    what.Trim().ToLower(CultureInfo.CurrentCulture),
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    this._secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 0;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the rubrics by city name.
        /// </summary>
        /// <param name="where">The city name.</param>
        /// <param name="callback">The callback.</param>
        public void GetRubrics(string where, ApiCallbackType<GetRubricResponse> callback)
        {
            ////Assigning callback
            this._call3 = callback;
            ////Checking where field
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////If all passed
            if (where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "rubricator?where={0}&key={1}&version=1.3&output=json",
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    this._secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 3;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the rubrics by city name or by rubric ID.
        /// </summary>
        /// <param name="where">The city name.</param>
        /// <param name="id">The rubric ID.</param>
        /// <param name="parentId">The parent rubric id.</param>
        /// <param name="showChildren">Should show children flag. 1 - to show, any other - no.</param>
        /// <param name="sort">The sorting key. Supported: name - alphabetical sort, popularity - by popularity value.</param>
        /// <param name="callback">The callback.</param>
        public void GetRubrics(string where, string id, string parentId, int? showChildren, SortDirections? sort, ApiCallbackType<GetRubricResponse> callback)
        {
            ////Assigning callback
            this._call3 = callback;
            ////Checking where field
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }

            string sortType = String.Empty;
            ////Checking sort
            if (sort.HasValue)
            {
                sortType = Enum.GetName(typeof(SortDirections), sort);
            }

            ////If all passed
            if (where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "rubricator?where={0}&id={1}&parent_id={2}&key={3}&sort={4}{5}&version=1.3&output=json",
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    String.IsNullOrEmpty(id) ? String.Empty : id,
                    String.IsNullOrEmpty(parentId) ? String.Empty : parentId,
                    this._secretKey,
                    String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
                    showChildren != null ? "&show_children=" + showChildren : "");
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 3;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the firms in selected rubric.
        /// </summary>
        /// <param name="rubric">The rubric for search.</param>
        /// <param name="where">The where field. Length > 2.</param>
        /// <param name="radius">The radius of search. Values from 1 to 40000. 250 by default. Unnecessary parameter.</param>
        /// <param name="page">The number of requested page. Unnecessary parameter.</param>
        /// <param name="pagesize">The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.</param>
        /// <param name="sort">The sorting key. Unnecessary parameter.</param>
        /// <param name="filters">The additional sorting filter by working time.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirmsInRubric(string rubric, string where, long? radius, int? page, int? pagesize, SortDirections? sort, string[] filters, ApiCallbackType<FirmSearchResponse> callback)
        {
            ////Assigning callback
            this._call = callback;
            ////Checking what and where fields
            if (rubric.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }
            ////Checking where range
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////Checking radius range
            if (radius.HasValue)
            {
                if (radius.Value < 1 || radius.Value > 40000)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.RadiusError });
                }
            }
            ////Checking pagesize range
            if (pagesize.HasValue)
            {
                if (pagesize.Value < 5 || pagesize.Value > 50)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PagesizeError });
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
                foreach (var filter in filters)
                {
                    filtersArray += "&filters[worktime]=" + filter;
                }
            }

            ////If all passed
            if (rubric.Trim().Length > 2 && where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "searchinrubric?what={0}&where={1}&radius={2}&page={3}&pagesize={4}&key={5}&version=1.3&sort={6}{7}&output=json",
                    rubric.Trim().ToLower(CultureInfo.CurrentCulture),
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    radius.HasValue ? radius.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    page.HasValue ? page.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    pagesize.HasValue ? pagesize.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    this._secretKey,
                    String.IsNullOrEmpty(sortType) ? String.Empty : sortType,
                    filtersArray);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 0;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the firms in selected rubric.
        /// </summary>
        /// <param name="rubric">The rubric for search.</param>
        /// <param name="where">The where field. Length > 2.</param>
        /// <param name="callback">The callback.</param>
        public void FindFirmsInRubric(string rubric, string where, ApiCallbackType<FirmSearchResponse> callback)
        {
            ////Assigning callback
            this._call = callback;
            ////Checking what and where fields
            if (rubric.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }
            ////Checking where range
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////If all passed
            if (rubric.Trim().Length > 2 && where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "searchinrubric?what={0}&where={1}&key={2}&version=1.3&output=json",
                    rubric.Trim().ToLower(CultureInfo.CurrentCulture),
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    this._secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 0;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the firm info by it's ID and HASH.
        /// </summary>
        /// <param name="id">The id of the firm.</param>
        /// <param name="hash">The unique hash of th e firm.</param>
        /// <param name="callback">The callback.</param>
        public void GetFirmInfo(string id, string hash, ApiCallbackType<FirmProfileResponse> callback)
        {
            ////Assigning callback
            this._call1 = callback;
            ////If all passed
            if (id.Trim().Length > 2 && hash.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "profile?id={0}&hash={1}&key={2}&version=1.3&output=json",
                    id.Trim().ToLower(CultureInfo.CurrentCulture),
                    hash.Trim().ToLower(CultureInfo.CurrentCulture),
                    this._secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 1;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the firms branches by firm ID.
        /// </summary>
        /// <param name="firmid">The firm ID.</param>
        /// <param name="callback">The callback.</param>
        public void GetFirmsBranches(string firmid, ApiCallbackType<BranchesOfFirmResponse> callback)
        {
            ////Assigning callback
            this._call2 = callback;
            ////If all passed
            if (firmid.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiEndpoint + "firmsByFilialId?firmid={0}&key={1}&version=1.3&output=json",
                    firmid.Trim().ToLower(CultureInfo.CurrentCulture),
                    this._secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 2;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Registers the firm profile views.
        /// </summary>
        /// <param name="hash">The hash of the firm. Can be get from register_bc_url field of the firm's profile.</param>
        /// <param name="callback">The callback.</param>
        public void RegisterProfileViews(string hash, ApiCallbackType<int> callback)
        {
            ////Assigning callback
            this._call4 = callback;
            ////If all passed
            if (hash.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiTrackerEndpoint + hash);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 4;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }


        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetProjects(ApiCallbackType<GetProjectResponse> callback)
        {
            ////Assigning callback
            this._call5 = callback;

            string request = String.Format(
                CultureInfo.CurrentCulture, "http://catalog.api.2gis.ru/project/list?version=1.3&output=json&key={0}",
                _secretKey);
            var uri = new Uri(request);
            try
            {
                ////Setting request type
                this._requestType = 5;
                ////Performing async open-read
                if (!this._client.IsBusy)
                {
                    this._client.OpenReadAsync(uri);
                }
            }
            catch (WebException ex)
            {
                ////Calling proper callback and setting error data.
                this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
            }
        }

        /// <summary>
        /// Gets the cities in project.
        /// </summary>
        /// <param name="where">The city name.</param>
        /// <param name="callback">The callback.</param>
        public void GetCitiesInProject(string where, ApiCallbackType<GetCitiesResponse> callback)
        {
            ////Assigning callback
            this._call6 = callback;
            ////Checking where range
            if (where.Trim().Length < 3)
            {
                this._call6.Invoke(new ApiResponse<GetCitiesResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////If all passed
            if (where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/city/list?version=1.3&key={0}&where={1}",
                    _secretKey,
                    where.Trim().ToLower(CultureInfo.CurrentCulture));
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 6;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the cities in project by project ID.
        /// </summary>
        /// <param name="id">The project id</param>
        /// <param name="callback">The callback.</param>
        public void GetCitiesInProjectById(string id, ApiCallbackType<GetCitiesResponse> callback)
        {
            ////Assigning callback
            this._call6 = callback;
            ////Checking where range
            if (id.Trim().Length < 1)
            {
                this._call6.Invoke(new ApiResponse<GetCitiesResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////If all passed
            if (id.Trim().Length > 0)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/city/list?version=1.3&key={0}&project_id={1}",
                    _secretKey,
                    id.Trim().ToLower(CultureInfo.CurrentCulture));
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 6;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the geo objects by name.
        /// </summary>
        /// <param name="q">The query. Length > 2.</param>
        /// <param name="callback">The callback.</param>
        public void GetGeoObjects(string q, ApiCallbackType<GetGeoObjectResponse> callback)
        {
            ////Assigning callback
            this._call7 = callback;
            ////Checking where range
            if (q.Trim().Length < 1)
            {
                this._call7.Invoke(new ApiResponse<GetGeoObjectResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////If all passed
            if (q.Trim().Length > 0)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/geo/search?q={0}&key={1}&version=1.3&output=json",
                    q.Trim().ToLower(CultureInfo.CurrentCulture),
                    _secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 7;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the geo objects by name.
        /// </summary>
        /// <param name="q">The query. Length &gt; 2.</param>
        /// <param name="types">The types array. See GeoTypes. Unnecessary parameter.</param>
        /// <param name="limit">The limit of results. By default: 1. Unnecessary parameter.</param>
        /// <param name="callback">The callback.</param>
        public void GetGeoObjects(string q, string[] types, long? limit, ApiCallbackType<GetGeoObjectResponse> callback)
        {
            ////Assigning callback
            this._call7 = callback;
            ////Checking where range
            if (q.Trim().Length < 1)
            {
                this._call7.Invoke(new ApiResponse<GetGeoObjectResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            //Getting types array
            var typesData = "";
            if (types != null && types.Length > 0)
            {
                foreach (var type in types)
                    typesData += type + ",";
                typesData = typesData.Substring(0, typesData.Length - 1);
            }
            ////If all passed
            if (q.Trim().Length > 0)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/geo/search?q={0}&types={1}&limit={2}&key={3}&version=1.3&output=json",
                    q.Trim().ToLower(CultureInfo.CurrentCulture),
                    String.IsNullOrEmpty(typesData) ? String.Empty : typesData,
                    limit.HasValue ? limit.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    _secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 7;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }


        /// <summary>
        /// Gets the geo objects by coordinats.
        /// </summary>
        /// <param name="q">The coordinates of the serach area in WGS84 format.</param>
        /// <param name="callback">The callback.</param>
        public void GetGeoObjects(GeoPoint q, ApiCallbackType<GetGeoObjectResponse> callback)
        {
            ////Assigning callback
            this._call7 = callback;
            ////Checking where range
            if (q == null)
            {
                this._call7.Invoke(new ApiResponse<GetGeoObjectResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PointError });
            }
            ////If all passed
            if (q != null)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/geo/search?q={0}&key={1}&version=1.3&output=json",
                    q,
                    _secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 7;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the geo objects by coordinats.
        /// </summary>
        /// <param name="q">The coordinates of the serach area in WGS84 format.</param>
        /// <param name="types">The types array. See GeoTypes. Unnecessary parameter.</param>
        /// <param name="radius">The search radius in meters. Values from 1 to 250. Unnecessary parameter.</param>
        /// <param name="project">The project ID to search in. Unnecessary parameter.</param>
        /// <param name="callback">The callback.</param>
        public void GetGeoObjects(GeoPoint q, string[] types, long? radius, string project, ApiCallbackType<GetGeoObjectResponse> callback)
        {
            ////Assigning callback
            this._call7 = callback;
            ////Checking where range
            if (q == null)
            {
                this._call7.Invoke(new ApiResponse<GetGeoObjectResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PointError });
            }
            ////Checking radius range
            if (radius.HasValue)
            {
                if (radius.Value < 1 || radius.Value > 250)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.RadiusError });
                }
            }
            //Getting types array
            var typesData = "";
            if (types != null && types.Length > 0)
            {
                foreach (var type in types)
                    typesData += type + ",";
                typesData = typesData.Substring(0, typesData.Length - 1);
            }
            ////If all passed
            if (q != null)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/geo/search?q={0}&types={1}&radius={2}&project={3}&key={4}&version=1.3&output=json",
                    q,
                    String.IsNullOrEmpty(typesData) ? String.Empty : typesData,
                    radius.HasValue ? radius.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    String.IsNullOrEmpty(project) ? String.Empty : project,
                    _secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 7;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the geo object info.
        /// </summary>
        /// <param name="id">The id if geo object.</param>
        /// <param name="callback">The callback.</param>
        public void GetGeoObjectInfo(string id, ApiCallbackType<GetGeoObjectResponse> callback)
        {
            ////Assigning callback
            this._call7 = callback;
            ////If all passed
            if (id.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    "http://catalog.api.2gis.ru/geo/get?id={0}&key={1}&version=1.3&output=json",
                    id,
                    _secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 7;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the ads by what and where parameters.
        /// </summary>
        /// <param name="what">The what field. Length &gt; 2.</param>
        /// <param name="where">The where field. Length &gt; 2.</param>
        /// <param name="format">The format of the resonse. Short or full..</param>
        /// <param name="page">The number of requested page. Unnecessary parameter.</param>
        /// <param name="pagesize">The results count on page. Values from 5 to 50. 20 by default. Unnecessary parameter.</param>
        /// <param name="callback">The callback.</param>
        public void FindAds(string what, string where, string format, int? page, int? pagesize, ApiCallbackType<FindAdsResponse> callback)
        {
            ////Assigning callback
            this._call8 = callback;
            ////Checking what and where fields
            if (what.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhatError });
            }

            ////Checking where range
            if (where.Trim().Length < 3)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.WhereError });
            }
            ////Checking pagesize range
            if (pagesize.HasValue)
            {
                if (pagesize.Value < 5 || pagesize.Value > 50)
                {
                    this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = null, IsCanceled = true, Response = null, InfoMessage = ErrorMsgs.PagesizeError });
                }
            }

            ////If all passed
            if (what.Trim().Length > 2 && where.Trim().Length > 2)
            {
                string request = String.Format(
                    CultureInfo.CurrentCulture,
                    this._apiAdsEndpoint + "search?what={0}&where={1}{2}{3}{4}&key={5}&version=1.3&output=json",
                    what.Trim().ToLower(CultureInfo.CurrentCulture),
                    where.Trim().ToLower(CultureInfo.CurrentCulture),
                    page.HasValue ? "&page=" + page.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    pagesize.HasValue ? "&pagesize=" + pagesize.Value.ToString(CultureInfo.CurrentCulture) : String.Empty,
                    string.IsNullOrEmpty(format) ? string.Empty : "&format=" + format,
                    this._secretKey);
                var uri = new Uri(request);
                try
                {
                    ////Setting request type
                    this._requestType = 8;
                    ////Performing async open-read
                    if (!this._client.IsBusy)
                    {
                        this._client.OpenReadAsync(uri);
                    }
                }
                catch (WebException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds the ads by what and where parameters.
        /// </summary>
        /// <param name="what">The what field. Length > 2.</param>
        /// <param name="where">The where field. Length > 2.</param>
        /// <param name="callback">The callback.</param>
        public void FindAds(string what, string where, ApiCallbackType<FindAdsResponse> callback)
        {
            FindAds(what, where, null, null, null, callback);
        }

        /// <summary>
        /// Handles the findFirms event of the client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void clientFindFirms(object sender, OpenReadCompletedEventArgs e)
        {
            ////If no Error
            if (e.Error == null)
            {
                try
                {
                    ////Getting result stream
                    var s = e.Result;
                    JsonSerializer _jsonDeserializer = new JsonSerializer();
                    JsonTextReader _jsonReader = new JsonTextReader(new StreamReader(s));
                    ////Depending on the request type, deserializing JSON to Objects
                    if (this._requestType == 0)
                    {
                        //Product deserializedProduct = (FirmSearchResponse)JavaScriptConvert.DeserializeObject(s, typeof(FirmSearchResponse));
                        //var ser = new DataContractJsonSerializer(typeof(FirmSearchResponse));
                        var resp = (FirmSearchResponse)_jsonDeserializer.Deserialize(_jsonReader, typeof(FirmSearchResponse));
                        if (this._call != null)
                        {
                            this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }
                    }

                    if (this._requestType == 1)
                    {
                        //var ser = new DataContractJsonSerializer(typeof(FirmProfileResponse));
                        var resp = (FirmProfileResponse)_jsonDeserializer.Deserialize(_jsonReader, typeof(FirmProfileResponse));
                        if (this._call1 != null)
                        {
                            this._call1.Invoke(new ApiResponse<FirmProfileResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }
                    }

                    if (this._requestType == 2)
                    {
                        //var ser = new DataContractJsonSerializer(typeof(BranchesOfFirmResponse));
                        var resp = (BranchesOfFirmResponse)_jsonDeserializer.Deserialize(_jsonReader, typeof(BranchesOfFirmResponse));
                        if (this._call2 != null)
                        {
                            this._call2.Invoke(new ApiResponse<BranchesOfFirmResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }
                    }

                    if (this._requestType == 3)
                    {
                        //var ser = new DataContractJsonSerializer(typeof(GetRubricResponse));
                        var resp = (GetRubricResponse)_jsonDeserializer.Deserialize(_jsonReader, typeof(GetRubricResponse));
                        if (this._call3 != null)
                        {
                            this._call3.Invoke(new ApiResponse<GetRubricResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }
                    }

                    if (this._requestType == 4)
                    {
                        var resp = _jsonDeserializer.Deserialize<int>(_jsonReader);
                        if (this._call4 != null)
                        {
                            this._call4.Invoke(new ApiResponse<int> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }

                    }

                    if (this._requestType == 5)
                    {
                        var resp = _jsonDeserializer.Deserialize<GetProjectResponse>(_jsonReader);
                        if (this._call5 != null)
                        {
                            this._call5.Invoke(new ApiResponse<GetProjectResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }

                    }

                    if (this._requestType == 6)
                    {
                        var resp = _jsonDeserializer.Deserialize<GetCitiesResponse>(_jsonReader);
                        if (this._call6 != null)
                        {
                            this._call6.Invoke(new ApiResponse<GetCitiesResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }

                    }

                    if (this._requestType == 7)
                    {
                        var resp = _jsonDeserializer.Deserialize<GetGeoObjectResponse>(_jsonReader);
                        if (this._call7 != null)
                        {
                            this._call7.Invoke(new ApiResponse<GetGeoObjectResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }
                    }

                    if (this._requestType == 8)
                    {
                        var resp = _jsonDeserializer.Deserialize<FindAdsResponse>(_jsonReader);
                        if (this._call8 != null)
                        {
                            this._call8.Invoke(new ApiResponse<FindAdsResponse> { Error = e.Error, IsCanceled = e.Cancelled, Response = resp, InfoMessage = "OK" });
                        }
                    }
                }
                catch (SerializationException ex)
                {
                    ////Calling proper callback and setting error data.
                    this.invokeExceptionsForAllCalls(ex.InnerException, ex.Message);
                }
                finally
                {
                    if (e.Result != null)
                    {
                        e.Result.Close();
                    }
                }
            }
            else
            {
                ////Calling proper callback and setting error data.
                this.invokeExceptionsForAllCalls(e.Error, e.Error.Message);
            }
        }

        /// <summary>
        /// Invokes the exceptions for all calls.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <param name="message">The message of the exception.</param>
        private void invokeExceptionsForAllCalls(Exception e, string message)
        {
            if (e == null)
            {
                return;
            }

            if (this._call != null)
            {
                this._call.Invoke(new ApiResponse<FirmSearchResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call1 != null)
            {
                this._call1.Invoke(new ApiResponse<FirmProfileResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call2 != null)
            {
                this._call2.Invoke(new ApiResponse<BranchesOfFirmResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call3 != null)
            {
                this._call3.Invoke(new ApiResponse<GetRubricResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call4 != null)
            {
                this._call4.Invoke(new ApiResponse<int> { Error = e, IsCanceled = true, Response = -1, InfoMessage = message });
            }

            if (this._call5 != null)
            {
                this._call5.Invoke(new ApiResponse<GetProjectResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call6 != null)
            {
                this._call6.Invoke(new ApiResponse<GetCitiesResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call7 != null)
            {
                this._call7.Invoke(new ApiResponse<GetGeoObjectResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }

            if (this._call8 != null)
            {
                this._call8.Invoke(new ApiResponse<FindAdsResponse> { Error = e, IsCanceled = true, Response = null, InfoMessage = message });
            }
        }

        #endregion Methods
    }
}
