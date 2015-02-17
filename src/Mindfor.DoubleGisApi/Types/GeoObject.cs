// <copyright file="GeoObject.cs" company="Mindfor">
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

namespace Midnfor.DoubleGisApi.Types
{
	/// <summary>
	/// Class that represents the GeoObject container. 
	/// </summary>
	[DataContract(Name = "geoObject", Namespace = "")]
	public class GeoObject
	{
		/// <summary>
		/// Gets or sets the GeoObject ID. 
		/// </summary>
		/// <value> The GeoObject ID. </value>
		[DataMember(Name = "id")]
		private string Id { get; set; }

		/// <summary>
		/// Gets or sets the project ID. 
		/// </summary>
		/// <value> The project ID. </value>
		[DataMember(Name = "project_id")]
		private string ProjectId { get; set; }

		/// <summary>
		/// Gets or sets the GeoObject type. 
		/// </summary>
		/// <value> The GeoObject type. </value>
		[DataMember(Name = "type")]
		private string Type { get; set; }

		/// <summary>
		/// Gets or sets the name. 
		/// </summary>
		/// <value> The name. </value>
		[DataMember(Name = "name")]
		private string Name { get; set; }

		/// <summary>
		/// Gets or sets the short name. 
		/// </summary>
		/// <value> The short name. </value>
		[DataMember(Name = "short_name")]
		private string ShortName { get; set; }

		/// <summary>
		/// Gets or sets the selection. Point. 
		/// </summary>
		/// <value> The selection. </value>
		[DataMember(Name = "selection")]
		private string Selection { get; set; }

		/// <summary>
		/// Gets or sets the centroid. Point. 
		/// </summary>
		/// <value> The centroid. </value>
		[DataMember(Name = "centroid")]
		private string Centroid { get; set; }

		/// <summary>
		/// Gets or sets the attributes of the GeoObject. 
		/// </summary>
		/// <value> The attributes of the GeoObject. </value>
		[DataMember(Name = "attributes")]
		private GeoAttributes Attributes { get; set; }

		/// <summary>
		/// Gets or sets the distance in meters from the center of the search. 
		/// </summary>
		/// <value> The distance. </value>
		[DataMember(Name = "dist")]
		private string Dist { get; set; }
	}
}