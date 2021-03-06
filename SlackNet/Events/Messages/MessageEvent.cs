﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SlackNet.Blocks;
using SlackNet.WebApi;

namespace SlackNet.Events
{
    [SlackType("message")]
    public class MessageEvent : Event
    {
        public string Subtype { get; set; }
        public string Channel { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public string Ts { get; set; }
        [JsonIgnore]
        public DateTime Timestamp => Ts.ToDateTime().GetValueOrDefault();
        public string ThreadTs { get; set; }
        [JsonIgnore]
        public DateTime? ThreadTimestamp => ThreadTs.ToDateTime();
        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
        public IList<Block> Blocks { get; set; } = new List<Block>();
        public Edit Edited { get; set; }
        /// <summary>
        /// Indicates message is part of the history of a channel but should not be displayed to users.
        /// </summary>
        public virtual bool Hidden => false;
        public int ReplyCount { get; set; }
        /// <summary>
        /// Deprecated, to be removed on October 18, 2019. Use <see cref="IConversationsApi.Replies"/> to get replies instead.
        /// </summary>
        [Obsolete]
        public IList<Reply> Replies { get; set; } = new List<Reply>();
        public IList<string> ReplyUsers { get; set; } = new List<string>();
        public int ReplyUsersCount { get; set; }
        public string LatestReply { get; set; }
        public bool IsStarred { get; set; }
        public IList<Reaction> Reactions { get; set; } = new List<Reaction>();
    }

    public class Edit
    {
        public string User { get; set; }
        public string Ts { get; set; }
        [JsonIgnore]
        public DateTime Timestamp => Ts.ToDateTime().GetValueOrDefault();
    }

    public class Reply
    {
        public string User { get; set; }
        public string Ts { get; set; }
        [JsonIgnore]
        public DateTime Timestamp => Ts.ToDateTime().GetValueOrDefault();
    }
}
