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

namespace Tgm.Roborally.Server.Models
{ 
        /// <summary>
        /// Defines wether to turn left or right
        /// </summary>
        /// <value>Defines wether to turn left or right</value>
        [TypeConverter(typeof(CustomEnumConverter<Rotation>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum Rotation
        {
            
            /// <summary>
            /// Enum LeftEnum for left
            /// </summary>
            [EnumMember(Value = "left")]
            LeftEnum = 1,
            
            /// <summary>
            /// Enum RightEnum for right
            /// </summary>
            [EnumMember(Value = "right")]
            RightEnum = 2
        }
}