// <copyright file="FirmProfileResponse.cs" company="Company">
//
//    Copyright (c) 2011, Anton Gubarenko
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
namespace GISApiWrapper.Types
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;    

    /// <summary>
    /// Class that represents Firm Profile Response.
    /// </summary>
    [DataContract]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "JSON deserialization properties must be identical to API names")]
    public class FirmProfileResponse
    {
        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        /// <value>
        /// The API version.
        /// </value>
        [DataMember]
        public string api_version { get; set; }

        /// <summary>
        /// Gets or sets the response code. 200 if ok, else - error. 
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        [DataMember]
        public string response_code { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [DataMember]
        public string error_code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        [DataMember]
        public string error_message { get; set; }

        /// <summary>
        /// Gets or sets the firm ID.
        /// </summary>
        /// <value>
        /// The firm ID.
        /// </value>
        [DataMember]
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the deletion date of the branch.
        /// </summary>
        /// <value>
        /// The deletion date.
        /// </value>
        [DataMember]
        public string deletion_date { get; set; }

        /// <summary>
        /// Gets or sets the firm group. If firm has some branches.
        /// </summary>
        /// <value>
        /// The firm group.
        /// </value>
        [DataMember]
        public FirmGroup firm_group { get; set; }

        /// <summary>
        /// Gets or sets the query parameters for API Tracker.
        /// </summary>
        /// <value>
        /// The query parameters for API Tracker.
        /// </value>
        [DataMember]
        public string register_bc_url { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinates on the map.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [DataMember]
        public string lon { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinates on the map.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [DataMember]
        public string lat { get; set; }

        /// <summary>
        /// Gets or sets the firms name.
        /// </summary>
        /// <value>
        /// The firms name.
        /// </value>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        /// <value>
        /// The city name.
        /// </value>
        [DataMember]
        public string city_name { get; set; }

        /// <summary>
        /// Gets or sets the address of the firm.
        /// </summary>
        /// <value>
        /// The address of the firm.
        /// </value>
        [DataMember]
        public string address { get; set; }

        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>
        /// The rubrics.
        /// </value>
        [DataMember]
        public string[] rubrics { get; set; }

        /// <summary>
        /// Gets or sets the contacts of the firm.
        /// </summary>
        /// <value>
        /// The contacts of the firm.
        /// </value>
        [DataMember]
        public Contact[] contacts { get; set; }

        /// <summary>
        /// Gets or sets the microcomment or advertising.
        /// </summary>
        /// <value>
        /// The  microcomment or advertising.
        /// </value>
        [DataMember]
        public string micro_comment { get; set; }

        /// <summary>
        /// Gets or sets the comment or advertising.
        /// </summary>
        /// <value>
        /// The  comment or advertising.
        /// </value>
        [DataMember]
        public string comment { get; set; }

        /// <summary>
        /// Gets or sets the article, 2000 chars or advertising.
        /// </summary>
        /// <value>
        /// The article.
        /// </value>
        [DataMember]
        public string article { get; set; }

        /// <summary>
        /// Gets or sets the advertising link or position.
        /// </summary>
        /// <value>
        /// The advertising link.
        /// </value>
        [DataMember]
        public Link link { get; set; }

        /// <summary>
        /// Gets or sets the FAS warning.
        /// </summary>
        /// <value>
        /// The FAS warning.
        /// </value>
        [DataMember]
        public string fas_warning { get; set; }

        /// <summary>
        /// Gets or sets the working schedule of the firm.
        /// </summary>
        /// <value>
        /// The working schedule.
        /// </value>
        [DataMember]
        public Schedule schedule { get; set; }

        /// <summary>
        /// Gets or sets the pay options.
        /// </summary>
        /// <value>
        /// The pay options.
        /// </value>
        [DataMember]
        public string[] payoptions { get; set; }
    }
}
