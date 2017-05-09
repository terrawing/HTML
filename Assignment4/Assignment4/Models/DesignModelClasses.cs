using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Models
{
    public class Instrument
    {
        public Instrument()
        {

        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Category { get; set; }

        [Required, StringLength(100)]
        public string InstrumentName { get; set; }

        [Required, StringLength(100)]
        public string ModelCode { get; set; }

        public int MSRP { get; set; }

        public string PhotoContentType { get; set; }

        public byte[] PhotoMedia { get; set; }

        public string SoundClipContentType { get; set; }

        public byte[] SoundClipMedia { get; set; }

    }
}