using Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
	/// <summary>
	/// Log document
	/// </summary>
	public class Log
	{
		/// <summary>
		/// Log ID
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		/// <summary>
		/// Log description
		/// </summary>
		[BsonElement("description")]
		public string? Description { get; set; }

		/// <summary>
		/// Log type
		/// </summary>
		[BsonElement("type")]
		public LogType Type { get; set; }

		/// <summary>
		/// Log creation date-time
		/// </summary>
		[BsonElement("creation_date")]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Instance log object
		/// </summary>
		public Log()
		{

		}
	}
}

