// <copyright file="GeoAttributes.cs" company="Company">
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
// <remarks>
// 2014.05.23 Update the class members to accord 2GIS API Reference: http://api.2gis.ru/doc/geo/search/.
// </remarks>
namespace GisApiWrapper.Types
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class thatrepresents GeoObject attributes.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "JSON deserialization properties must be identical to API names")]
    [CLSCompliant(true)]
    public class GeoAttributes
    {

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        public string abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        /// <value>
        /// The city name.
        /// </value>
        public string city { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        /// <value>
        /// The district name.
        /// </value>
        public string district { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        /// <value>
        /// The street name.
        /// </value>
        public string street { get; set; }

        /// <summary>
        /// Gets or sets the number of building.
        /// </summary>
        /// <value>
        /// The number of building.
        /// </value>
        public string number { get; set; }

        /// <summary>
        /// Gets or sets the 2 street name.
        /// </summary>
        /// <value>
        /// The 2 street name.
        /// </value>
        public string street2 { get; set; }

        /// <summary>
        /// Gets or sets the number of 2 building.
        /// </summary>
        /// <value>
        /// The number of 2 building.
        /// </value>
        public string number2 { get; set; }

        /// <summary>
        /// Gets or sets the building name.
        /// </summary>
        /// <value>
        /// The building name.
        /// </value>
        public string buildingname { get; set; }

        /// <summary>
        /// Gets or sets the purpose of the building.
        /// </summary>
        /// <value>
        /// The purpose of the building.
        /// </value>
        public string purpose { get; set; }

        /// <summary>
        /// Gets or sets the elevation.
        /// </summary>
        /// <value>
        /// The elevation.
        /// </value>
        public int? elevation { get; set; }

        /// <summary>
        /// Gets or sets the firm count.
        /// </summary>
        /// <value>
        /// The firm count.
        /// </value>
        public int? firmcount { get; set; }

        /// <summary>
        /// Gets or sets the postal index.
        /// </summary>
        /// <value>
        /// The postal index.
        /// </value>
        public string index { get; set; }

        /// <summary>
        /// Gets or sets the additional information about sight.
        /// </summary>
        /// <value>
        /// The additional information about sight.
        /// </value>
        public string info { get; set; }


        /// <summary>
        /// Gets or sets the map class.
        /// </summary>
        /// <value>
        /// The map class.
        /// </value>
        public string mapclass { get; set; }

        /// <summary>
        /// Gets or sets the synonym.
        /// </summary>
        /// <value>
        /// The synonym.
        /// </value>
        public string synonym { get; set; }
    }
}
