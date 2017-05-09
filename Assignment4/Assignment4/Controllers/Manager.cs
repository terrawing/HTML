using Assignment4.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class Manager
    {
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<InstrumentWithPhotoAndSoundClipMediaInfo> InstrumentGetAll()
        {
            // Call the base class method
            var o = ds.Instruments.OrderBy(i => i.InstrumentName);

            return Mapper.Map<IEnumerable<InstrumentWithPhotoAndSoundClipMediaInfo>>(o);
        }

        public InstrumentWithPhotoAndSoundClipMediaInfo InstrumentAdd(InstrumentAdd newInstrument)
        {
            if (newInstrument != null)
            {
                Instrument addInstrument = Mapper.Map<Instrument>(newInstrument);
                var addedItem = ds.Instruments.Add(addInstrument);
                ds.SaveChanges();

                // Return the object
                return (addedItem == null) ? null : Mapper.Map<InstrumentWithPhotoAndSoundClipMediaInfo>(addedItem);
            }
            else
            {
                return null;
            }
        }

        public InstrumentWithPhotoAndSoundClipMedia GetInstrumentByIdWithMedia(int id)
        {
            // Attempt to fetch the object
            var fetchedObject = ds.Instruments.Find(id);

            // Return the result, or null if not found
            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                return Mapper.Map<InstrumentWithPhotoAndSoundClipMedia>(fetchedObject);
            }
        }

        public bool SetInstrumentPhoto(int id, string contentType, byte[] newPhoto)
        {
            if(string.IsNullOrEmpty(contentType) | newPhoto == null)
            {
                return false;
            }

            var storeItem = ds.Instruments.Find(id);

            if(storeItem == null)
            {
                return false;
            }

            //Save the photo
            storeItem.PhotoContentType = contentType;
            storeItem.PhotoMedia = newPhoto;

            return (ds.SaveChanges() > 0) ? true : false;
        }

        public bool SetInstrumentSound(int id, string contentType, byte[] newSound)
        {
            if (string.IsNullOrEmpty(contentType) | newSound == null)
            {
                return false;
            }

            var storeItem = ds.Instruments.Find(id);

            if (storeItem == null)
            {
                return false;
            }

            //Save the sound
            storeItem.SoundClipContentType = contentType;
            storeItem.SoundClipMedia = newSound;

            return (ds.SaveChanges() > 0) ? true : false;
        }

        public void DeleteInstrumentById(int id)
        {
            var fetchedObject = ds.Instruments.Find(id);

            if (fetchedObject == null)
            {
                throw new Exception("No instrument to delete from that id");
            }
            else
            {
                try
                {
                    ds.Instruments.Remove(fetchedObject);
                    ds.SaveChanges();
                }
                catch
                {
                    throw new Exception("Error trying to delete");
                }
            }
        }
    }
}