﻿// <copyright file="Response.cs" company="Mindfor">
//     Copyright (c) 2015, George Evstigneev All rights reserved.
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

using System;

namespace DoubleGisApi.Response
{
	/// <summary>
	/// Class that represents the response from API. 
	/// </summary>
	/// <typeparam name="T"> Response type. </typeparam>
	public class ApiResponse<T>
	{
		/// <summary>
		/// Gets or sets a value indicating whether the response is canceled. 
		/// </summary>
		/// <value> <c> true </c> if this response is canceled; otherwise, <c> false </c>. </value>
		public bool IsCanceled { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the response is was successed. 
		/// </summary>
		/// <value> <c> true </c> if this response is successed; otherwise, <c> false </c>. </value>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Gets or sets the error. 
		/// </summary>
		/// <value> The error. </value>
		public Exception Error { get; set; }

		/// <summary>
		/// Gets or sets the response. 
		/// </summary>
		/// <value> The response. </value>
		public T Response { get; set; }

		/// <summary>
		/// Gets or sets the info message. 
		/// </summary>
		/// <value> The info message. </value>
		public string InfoMessage { get; set; }
	}
}