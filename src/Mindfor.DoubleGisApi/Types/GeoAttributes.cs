// <copyright file="GeoAttributes.cs" company="Mindfor">
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
// <remarks> 2014. 05.23 Update the class members to accord 2GIS API Reference: http://api.2gis.ru/doc/geo/search/. </remarks>

using System.Runtime.Serialization;

namespace Midnfor.DoubleGisApi.Types
{
	/// <summary>
	/// Class thatrepresents GeoObject attributes. 
	/// </summary>
	[DataContract(Name = "geoAttributes", Namespace = "")]
	public class GeoAttributes
	{
		/// <summary>
		/// Gets or sets the abbreviation. 
		/// </summary>
		/// <value> The abbreviation. </value>
		[DataMember(Name = "abbreviation")]
		public string Abbreviation { get; set; }

		/// <summary>
		/// Gets or sets the type. 
		/// </summary>
		/// <value> The type. </value>
		[DataMember(Name = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the city name. 
		/// </summary>
		/// <value> The city name. </value>
		[DataMember(Name = "city")]
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the district name. 
		/// </summary>
		/// <value> The district name. </value>
		[DataMember(Name = "district")]
		public string District { get; set; }

		/// <summary>
		/// Gets or sets the street name. 
		/// </summary>
		/// <value> The street name. </value>
		[DataMember(Name = "street")]
		public string Street { get; set; }

		/// <summary>
		/// Gets or sets the number of building. 
		/// </summary>
		/// <value> The number of building. </value>
		[DataMember(Name = "number")]
		public string Number { get; set; }

		/// <summary>
		/// Gets or sets the 2 street name. 
		/// </summary>
		/// <value> The 2 street name. </value>
		[DataMember(Name = "street2")]
		public string Street2 { get; set; }

		/// <summary>
		/// Gets or sets the number of 2 building. 
		/// </summary>
		/// <value> The number of 2 building. </value>
		[DataMember(Name = "number2")]
		public string Number2 { get; set; }

		/// <summary>
		/// Gets or sets the building name. 
		/// </summary>
		/// <value> The building name. </value>
		[DataMember(Name = "buildingname")]
		public string Buildingname { get; set; }

		/// <summary>
		/// Gets or sets the purpose of the building. 
		/// </summary>
		/// <value> The purpose of the building. </value>
		[DataMember(Name = "purpose")]
		public string Purpose { get; set; }

		/// <summary>
		/// Gets or sets the elevation. 
		/// </summary>
		/// <value> The elevation. </value>
		[DataMember(Name = "elevation")]
		public int? Elevation { get; set; }

		/// <summary>
		/// Gets or sets the firm count. 
		/// </summary>
		/// <value> The firm count. </value>
		[DataMember(Name = "firmcount")]
		public int? Firmcount { get; set; }

		/// <summary>
		/// Gets or sets the postal index. 
		/// </summary>
		/// <value> The postal index. </value>
		[DataMember(Name = "index")]
		public string Index { get; set; }

		/// <summary>
		/// Gets or sets the additional information about sight. 
		/// </summary>
		/// <value> The additional information about sight. </value>
		[DataMember(Name = "info")]
		public string Info { get; set; }

		/// <summary>
		/// Gets or sets the map class. 
		/// </summary>
		/// <value> The map class. </value>
		[DataMember(Name = "mapclass")]
		public string Mapclass { get; set; }

		/// <summary>
		/// Gets or sets the synonym. 
		/// </summary>
		/// <value> The synonym. </value>
		[DataMember(Name = "synonym")]
		public string Synonym { get; set; }
	}
}