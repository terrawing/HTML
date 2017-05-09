﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Assign10.Models
{
    // Attention - Project class

    /// <summary>
    /// Project object, represents a new hobby/craft project
    /// </summary>
    public class Project
    {
        public Project()
        {
            DateCreated = DateTime.Now;
            Likes = 0;
            Medias = new List<Media>();
            Sharers = new List<Sharer>();
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(2000)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [Required, StringLength(256)]
        public string Owner { get; set; }

        [Required, StringLength(100)]
        public string Visibility { get; set; } // Private, Public, Shared

        [StringLength(200)]
        public string Tags { get; set; }

        public int Likes { get; set; }

        // Collection of media items
        public ICollection<Media> Medias { get; set; }

        // Collection of sharer items

        public ICollection<Sharer> Sharers { get; set; }
    }

    // Attention - Media class

    /// <summary>
    /// Media object, represents a photo, video, etc. for a project object
    /// </summary>
    public class Media
    {
        public Media()
        {
            DateCreated = DateTime.Now;
            ItemMedia = new byte[] { };
            ItemContentType = "new";
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(2000)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [Required, StringLength(256)]
        public string Owner { get; set; }

        [Required, StringLength(100)]
        public string Contributor { get; set; }

        // Media item bytes and content type
        [Required]
        public byte[] ItemMedia { get; set; }
        [Required]
        public string ItemContentType { get; set; }

        [Required]
        public Project Project { get; set; }
    }

    // Attention - Sharer class

    /// <summary>
    /// Sharer object, stores the user name and access level for a project.
    /// </summary>
    public class Sharer
    {
        public Sharer()
        {
            DateUpdated = DateTime.Now;
        }

        public int Id { get; set; }

        [Required, StringLength(30)]
        public string AccessLevel { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        public DateTime DateUpdated { get; set; }

        [Required]
        public Project Project { get; set; }
    }

}
