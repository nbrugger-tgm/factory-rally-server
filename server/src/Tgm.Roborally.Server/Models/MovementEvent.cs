/*
 * Robot Rally Game logic engine
 *
 * This api controlls the flow of a game and provides it's data. It is desiged to be RESTfull so the structure works simmilar as file system. The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: nbrugger@student.tgm.ac.at
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Tgm.Roborally.Server.Converters;
using Tgm.Roborally.Server.Engine;

namespace Tgm.Roborally.Server.Models
{ 
    /// <summary>
    /// Describes the movement of a entity
    /// </summary>
    [DataContract]
    public partial class MovementEvent : IEquatable<MovementEvent>, Event {
        /// <summary>
        /// The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only
        /// </summary>
        /// <value>The unique identification of this entity. &lt;br&gt; *!!!* This is not the id of the player&lt;br&gt; This value will be autogenerated by the api and is read only</value>
        [DataMember(Name="entity", EmitDefaultValue=false)]
        public int Entity { get; set; }

        /// <summary>
        /// Gets or Sets Direction
        /// </summary>
        [DataMember(Name="direction", EmitDefaultValue=false)]
        public Direction Direction { get; set; }

        /// <summary>
        /// How far (in tiles) the movement was executed
        /// </summary>
        /// <value>How far (in tiles) the movement was executed</value>
        [Range(0, 50)]
        [DataMember(Name="movement-ammount", EmitDefaultValue=false)]
        public int MovementAmmount { get; set; }

        /// <summary>
        /// Gets or Sets Rotation
        /// </summary>
        [DataMember(Name="rotation", EmitDefaultValue=false)]
        public Rotation Rotation { get; set; }

        /// <summary>
        /// The number of times (90°) the robot turns
        /// </summary>
        /// <value>The number of times (90°) the robot turns</value>
        [Range(0, 2)]
        [DataMember(Name="rotation-times", EmitDefaultValue=false)]
        public int RotationTimes { get; set; }

        /// <summary>
        /// Gets or Sets From
        /// </summary>
        [DataMember(Name="from", EmitDefaultValue=false)]
        public Position From { get; set; }

        /// <summary>
        /// Gets or Sets To
        /// </summary>
        [DataMember(Name="to", EmitDefaultValue=false)]
        public Position To { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MovementEvent {\n");
            sb.Append("  Entity: ").Append(Entity).Append("\n");
            sb.Append("  Direction: ").Append(Direction).Append("\n");
            sb.Append("  MovementAmmount: ").Append(MovementAmmount).Append("\n");
            sb.Append("  Rotation: ").Append(Rotation).Append("\n");
            sb.Append("  RotationTimes: ").Append(RotationTimes).Append("\n");
            sb.Append("  From: ").Append(From).Append("\n");
            sb.Append("  To: ").Append(To).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public EventType GetEventType() => EventType.Movement;

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((MovementEvent)obj);
        }

        /// <summary>
        /// Returns true if MovementEvent instances are equal
        /// </summary>
        /// <param name="other">Instance of MovementEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MovementEvent other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Entity == other.Entity ||
                    
                    Entity.Equals(other.Entity)
                ) && 
                (
                    Direction == other.Direction ||
                    
                    Direction.Equals(other.Direction)
                ) && 
                (
                    MovementAmmount == other.MovementAmmount ||
                    
                    MovementAmmount.Equals(other.MovementAmmount)
                ) && 
                (
                    Rotation == other.Rotation ||
                    
                    Rotation.Equals(other.Rotation)
                ) && 
                (
                    RotationTimes == other.RotationTimes ||
                    
                    RotationTimes.Equals(other.RotationTimes)
                ) && 
                (
                    From == other.From ||
                    From != null &&
                    From.Equals(other.From)
                ) && 
                (
                    To == other.To ||
                    To != null &&
                    To.Equals(other.To)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    
                    hashCode = hashCode * 59 + Entity.GetHashCode();
                    
                    hashCode = hashCode * 59 + Direction.GetHashCode();
                    
                    hashCode = hashCode * 59 + MovementAmmount.GetHashCode();
                    
                    hashCode = hashCode * 59 + Rotation.GetHashCode();
                    
                    hashCode = hashCode * 59 + RotationTimes.GetHashCode();
                    if (From != null)
                    hashCode = hashCode * 59 + From.GetHashCode();
                    if (To != null)
                    hashCode = hashCode * 59 + To.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(MovementEvent left, MovementEvent right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MovementEvent left, MovementEvent right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
