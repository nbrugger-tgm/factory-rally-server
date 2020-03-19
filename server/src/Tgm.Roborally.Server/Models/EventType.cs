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
        /// Describes what the event is about
        /// </summary>
        /// <value>Describes what the event is about</value>
        [TypeConverter(typeof(CustomEnumConverter<EventType>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum EventType
        {
            
            /// <summary>
            /// Enum Movement for movement
            /// </summary>
            [EnumMember(Value = "movement")]
            Movement = 1,
            
            /// <summary>
            /// Enum UpgradePurchase for upgrade purchase
            /// </summary>
            [EnumMember(Value = "upgrade purchase")]
            UpgradePurchase = 2,
            
            /// <summary>
            /// Enum FillRegister for fill register
            /// </summary>
            [EnumMember(Value = "fill register")]
            FillRegister = 3,
            
            /// <summary>
            /// Enum ActivateUpgrade for activate upgrade
            /// </summary>
            [EnumMember(Value = "activate upgrade")]
            ActivateUpgrade = 4,
            
            /// <summary>
            /// Enum LazerShot for lazer shot
            /// </summary>
            [EnumMember(Value = "lazer shot")]
            LazerShot = 5,
            
            /// <summary>
            /// Enum GameStart for game start
            /// </summary>
            [EnumMember(Value = "game start")]
            GameStart = 6,
            
            /// <summary>
            /// Enum ClearShop for clear shop
            /// </summary>
            [EnumMember(Value = "clear shop")]
            ClearShop = 7,
            
            /// <summary>
            /// Enum FillShop for fill shop
            /// </summary>
            [EnumMember(Value = "fill shop")]
            FillShop = 8,
            
            /// <summary>
            /// Enum RegisterClear for register clear
            /// </summary>
            [EnumMember(Value = "register clear")]
            RegisterClear = 9,
            
            /// <summary>
            /// Enum ProgrammingTimerStart for programming timer start
            /// </summary>
            [EnumMember(Value = "programming timer start")]
            ProgrammingTimerStart = 10,
            
            /// <summary>
            /// Enum ProgrammingTimerStop for programming timer stop
            /// </summary>
            [EnumMember(Value = "programming timer stop")]
            ProgrammingTimerStop = 11,
            
            /// <summary>
            /// Enum RandomCardDistribution for random card distribution
            /// </summary>
            [EnumMember(Value = "random card distribution")]
            RandomCardDistribution = 12,
            
            /// <summary>
            /// Enum TakeCardEvent for take card event
            /// </summary>
            [EnumMember(Value = "take card event")]
            TakeCardEvent = 13,
            
            /// <summary>
            /// Enum ActivateCheckpoint for activate checkpoint
            /// </summary>
            [EnumMember(Value = "activate checkpoint")]
            ActivateCheckpoint = 14
        }
}