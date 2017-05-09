using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class InstrumentAdd
    {
        [Required, StringLength(100)]
        public string Category { get; set; }

        [Required, StringLength(100)]
        public string InstrumentName { get; set; }

        [Required, StringLength(100)]
        public string ModelCode { get; set; }

        public int MSRP { get; set; }
    }

    public class InstrumentBase : InstrumentAdd
    {
        public int Id { get; set; }
    }

    // Attention 2 - Resource view model class used to get both photo and sound information
    public class InstrumentWithPhotoAndSoundClipMediaInfo : InstrumentBase
    {
        public int PhotoMediaLength { get; set; }
        public int SoundClipMediaLength { get; set; }
        public string PhotoContentType { get; set; }
        public string SoundClipContentType { get; set; }
    }

    public class InstrumentWithPhotoAndSoundClipMedia : InstrumentWithPhotoAndSoundClipMediaInfo
    {
        public byte[] PhotoMedia { get; set; }
        public byte[] SoundClipMedia { get; set; }
    }

    public class InstrumentWithPhotoMediaInfo : InstrumentBase
    {
        public int PhotoMediaLength { get; set; }
        public string PhotoContentType { get; set; }
    }

    public class InstrumentWithPhotoMedia : InstrumentWithPhotoMediaInfo
    {
        public byte[] PhotoMedia { get; set; }
    }

    
    public class InstrumentWithSoundClipMediaInfo : InstrumentBase
    {
        public int SoundClipMediaLength { get; set; }
        public string SoundClipContentType { get; set; }
    }

    public class InstrumentWithSoundClipMedia : InstrumentWithSoundClipMediaInfo
    {
        public byte[] SoundClipMedia { get; set; }
    }
}