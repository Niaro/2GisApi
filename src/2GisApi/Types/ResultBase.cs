using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DoubleGisApi.Types
{
	[DataContract(Name = "resultBase", Namespace = "")]
	public class ResultBase
	{
		/// <summary>
		/// Gets or sets the unique firm ID. 
		/// </summary>
		/// <value> The firm ID. </value>
		[DataMember(Name = "id", Order = 1)]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the longitude coordinates on the map. 
		/// </summary>
		/// <value> The longitude. </value>
		[DataMember(Name = "lon", Order = 2)]
		public string Lon { get; set; }

		/// <summary>
		/// Gets or sets the latitude coordinates on the map. 
		/// </summary>
		/// <value> The latitude. </value>
		[DataMember(Name = "lat", Order = 3)]
		public string Lat { get; set; }

		/// <summary>
		/// Gets or sets the name of the firm. 
		/// </summary>
		/// <value> The firms name. </value>
		[DataMember(Name = "name", Order = 4)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the unique hash. 
		/// </summary>
		/// <value> The firm hash. </value>
		[DataMember(Name = "hash", Order = 5)]
		public string Hash { get; set; }

		/// <summary>
		/// Gets or sets the city name. 
		/// </summary>
		/// <value> The city name. </value>
		[DataMember(Name = "city_name", Order = 6)]
		public string CityName { get; set; }

		/// <summary>
		/// Gets or sets the address of the firm. 
		/// </summary>
		/// <value> The address the address. </value>
		[DataMember(Name = "address", Order = 7)]
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the firm group. IF firm has branches. 
		/// </summary>
		/// <value> The firm group. </value>
		[DataMember(Name = "firm_group", Order = 8)]
		public FirmGroup FirmGroup { get; set; }

		/// <summary>
		/// Gets or sets the microcomment or advertising. 
		/// </summary>
		/// <value> The microcomment or advertising. </value>
		[DataMember(Name = "microcomment", Order = 9)]
		public string Microcomment { get; set; }

		/// <summary>
		/// Gets or sets the FAS warning. 
		/// </summary>
		/// <value> The FAS warning. </value>
		[DataMember(Name = "fas_warning", Order = 10)]
		public string FasWarning { get; set; }

		/// <summary>
		/// Gets or sets the rubrics. 
		/// </summary>
		/// <value> The rubrics. </value>
		[DataMember(Name = "rubrics", Order = 12)]
		public IEnumerable<string> Rubrics { get; set; }
	}
}