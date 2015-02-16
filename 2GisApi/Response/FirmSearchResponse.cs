// <copyright file="FirmSearchResponse.cs" company="Mindfor">
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

using DoubleGisApiWrapper.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DoubleGisApiWrapper.Response
{
	/// <summary>
	/// Class that represents firm search response. 
	/// </summary>
	[DataContract(Name = "firmSearchResponse", Namespace = "")]
	public class FirmSearchResponse : GisResponseBase
	{
		/// <summary>
		/// Gets or sets the what are you searched. 
		/// </summary>
		/// <value> The what did you search. </value>
		[DataMember(Name = "what")]
		private string What { get; set; }

		/// <summary>
		/// Gets or sets the where did yo search. 
		/// </summary>
		/// <value> The where. </value>
		[DataMember(Name = "where")]
		private string Where { get; set; }

		/// <summary>
		/// Gets or sets the hint for your search. 
		/// </summary>
		/// <value> The hint for the search. </value>
		[DataMember(Name = "did_you_mean")]
		private DidYouMean DidYouMean { get; set; }

		/// <summary>
		/// Gets or sets the advertisings. 
		/// </summary>
		/// <value> The advertisings. </value>
		[DataMember(Name = "advertising")]
		private IEnumerable<Advertising> Advertising { get; set; }

		/// <summary>
		/// Gets or sets the results of the response. 
		/// </summary>
		/// <value> The results of the response. </value>
		[DataMember(Name = "result")]
		private IEnumerable<Result> Result { get; set; }
	}
}