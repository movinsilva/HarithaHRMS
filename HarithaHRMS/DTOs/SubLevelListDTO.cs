﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using HarithaHRMS;
//
//    var subLevelListDto = SubLevelListDto.FromJson(jsonString);

namespace HarithaHRMS.DTOs
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SubLevelListDto
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("sublevels")]
        public List<Sublevel> Sublevels { get; set; }
    }

    public partial class Sublevel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("userID")]
        public object UserId { get; set; }

        [JsonProperty("projectID")]
        public long ProjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("manHours")]
        public long ManHours { get; set; }

        [JsonProperty("progressFraction")]
        public long ProgressFraction { get; set; }

        [JsonProperty("priorityLevel")]
        public long PriorityLevel { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }
    }

    public partial class Project
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public object Code { get; set; }

        [JsonProperty("assignedDateTime")]
        public DateTime AssignedDateTime { get; set; }

        [JsonProperty("userId")]
        public object UserId { get; set; }

        [JsonProperty("customer")]
        public object Customer { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("remarks")]
        public object Remarks { get; set; }

        [JsonProperty("progress")]
        public long Progress { get; set; }

        [JsonProperty("isFinished")]
        public bool IsFinished { get; set; }

        [JsonProperty("finishedDate")]
        public DateTimeOffset FinishedDate { get; set; }

        [JsonProperty("subLevels")]
        public object SubLevels { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public partial class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class SubLevelListDto
    {
        public static SubLevelListDto FromJson(string json) => JsonConvert.DeserializeObject<SubLevelListDto>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SubLevelListDto self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
