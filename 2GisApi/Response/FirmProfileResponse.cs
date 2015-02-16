// <copyright file="FirmProfileResponse.cs" company="Mindfor">
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
	/// Class that represents Firm Profile Response. 
	/// </summary>
	[DataContract(Name = "firmProfileResponse", Namespace = "")]
	public class FirmProfileResponse : GisResponseBase
	{
		/// <summary>
		/// Gets or sets the firm ID. 
		/// </summary>
		/// <value> The firm ID. </value>
		[DataMember(Name = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the deletion date of the branch. 
		/// </summary>
		/// <value> The deletion date. </value>
		[DataMember(Name = "deletion_date")]
		public string DeletionDate { get; set; }

		/// <summary>
		/// Gets or sets the query parameters for API Tracker. 
		/// </summary>
		/// <value> The query parameters for API Tracker. </value>
		[DataMember(Name = "register_bc_url")]
		public string RegisterBcUrl { get; set; }

		/// <summary>
		/// Gets or sets the longitude coordinates on the map. 
		/// </summary>
		/// <value> The longitude. </value>
		[DataMember(Name = "lon")]
		public string Lon { get; set; }

		/// <summary>
		/// Gets or sets the latitude coordinates on the map. 
		/// </summary>
		/// <value> The latitude. </value>
		[DataMember(Name = "lat")]
		public string Lat { get; set; }

		/// <summary>
		/// Gets or sets the firms name. 
		/// </summary>
		/// <value> The firms name. </value>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the firm group. If firm has some branches. 
		/// </summary>
		/// <value> The firm group. </value>
		[DataMember(Name = "firm_group")]
		public FirmGroup FirmGroup { get; set; }

		/// <summary>
		/// Gets or sets the city name. 
		/// </summary>
		/// <value> The city name. </value>
		[DataMember(Name = "city_name")]
		public string CityName { get; set; }

		/// <summary>
		/// Gets or sets the address of the firm. 
		/// </summary>
		/// <value> The address of the firm. </value>
		[DataMember(Name = "address")]
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the additional info. 
		/// </summary>
		/// <value> The additional info. </value>
		[DataMember(Name = "additional_info")]
		public AdditionalInfo AdditionalInfo { get; set; }

		/// <summary>
		/// Gets or sets the rubrics. 
		/// </summary>
		/// <value> The rubrics. </value>
		[DataMember(Name = "rubrics")]
		public IEnumerable<string> Rubrics { get; set; }

		/// <summary>
		/// Gets or sets the article, 2000 chars or advertising. 
		/// </summary>
		/// <value> The article. </value>
		[DataMember(Name = "article")]
		public string Article { get; set; }

		/// <summary>
		/// Gets or sets the contacts of the firm. 
		/// </summary>
		/// <value> The contacts of the firm. </value>
		[DataMember(Name = "contacts")]
		public IEnumerable<Contact> Contacts { get; set; }

		/// <summary>
		/// Gets or sets the working schedule of the firm. 
		/// </summary>
		/// <value> The working schedule. </value>
		[DataMember(Name = "schedule")]
		public Schedule Schedule { get; set; }

		/// <summary>
		/// Gets or sets the pay options. 
		/// </summary>
		/// <value> The pay options. </value>
		[DataMember(Name = "payoptions")]
		public IEnumerable<string> Payoptions { get; set; }

		/// <summary>
		/// Gets or sets the reviews count on Flamp.ru. 
		/// </summary>
		/// <value> The reviews count. </value>
		[DataMember(Name = "reviews_count")]
		public string ReviewsCount { get; set; }

		/// <summary>
		/// Gets or sets the modification time. 
		/// </summary>
		/// <value> The modification time. </value>
		[DataMember(Name = "modification_time")]
		public string ModificationTime { get; set; }

		/// <summary>
		/// Gets or sets the creation time. 
		/// </summary>
		/// <value> The creation time. </value>
		[DataMember(Name = "create_time")]
		public string CreateTime { get; set; }
	}
}