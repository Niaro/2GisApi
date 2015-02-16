﻿// <copyright file="Geo.cs" company="Mindfor">
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

using System.Runtime.Serialization;

namespace DoubleGisApiWrapper.Types
{
	/// <summary>
	/// Class that represent the where field hint. 
	/// </summary>
	[DataContract(Name = "geo", Namespace = "")]
	public class Geo
	{
		/// <summary>
		/// Gets or sets the city ID. 
		/// </summary>
		/// <value> The city ID. </value>
		[DataMember(Name = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the city. 
		/// </summary>
		/// <value> The name of the city. </value>
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}