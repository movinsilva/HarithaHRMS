﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using HarithaHRMS.DTOs;
//
//    var leaveListDto = LeaveListDto.FromJson(jsonString);

namespace HarithaHRMS.DTOs
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LeaveListDto
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("leaves")]
        public List<Leaf> Leaves { get; set; }
    }

    public partial class Leaf
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("date")]
        public String Date { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("isApproved")]
        public int IsApproved { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("approvedId")]
        public Guid? ApprovedId { get; set; }

        [JsonProperty("user")]
        public Approved User { get; set; }

        [JsonProperty("approved")]
        public Approved Approved { get; set; }
    }

    public partial class Approved
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("normalizedUserName")]
        public string NormalizedUserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("normalizedEmail")]
        public string NormalizedEmail { get; set; }

        [JsonProperty("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonProperty("securityStamp")]
        public string SecurityStamp { get; set; }

        [JsonProperty("concurrencyStamp")]
        public Guid ConcurrencyStamp { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("phoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonProperty("twoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonProperty("lockoutEnd")]
        public object LockoutEnd { get; set; }

        [JsonProperty("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [JsonProperty("accessFailedCount")]
        public long AccessFailedCount { get; set; }
    }

    public partial class LeaveListDto
    {
        public static LeaveListDto FromJson(string json) => JsonConvert.DeserializeObject<LeaveListDto>(json, HarithaHRMS.DTOs.Converter.Settings);
    }

    public static class SerializeLeave
    {
        public static string ToJson(this LeaveListDto self) => JsonConvert.SerializeObject(self, HarithaHRMS.DTOs.Converter.Settings);
    }

    internal static class ConverterLeave
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
