using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment4.Controllers
{
    public class InstrumentsController : ApiController
    {
        private Manager m = new Manager();

        // Attention 3 - Get all instruments with photo data and sound data
        // GET: api/Instruments
        public IHttpActionResult Get()
        {
            return Ok(m.InstrumentGetAll());
        }

        // Attention 4 - Get an instrument by id, but check for an Accept header for either an image or audio. If either of those 2 are found it will return the media item. Otherwise return the instrument data only.
        // GET: api/Instruments/5
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var fetchedObject = m.GetInstrumentByIdWithMedia(id.Value);

            if(fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                // Attention 5 - Content negotiation code

                // Look for an Accept header that starts with "image"
                var imageHeader = Request.Headers.Accept.SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));
                // Look for an Accept header that starts with "audio"
                var audioHeader = Request.Headers.Accept.SingleOrDefault(a => a.MediaType.ToLower().StartsWith("audio/"));

                if (imageHeader == null && audioHeader == null)
                {
                    // If there is no image or audio header, just do regular normal processing for a JSON result
                    return Ok(Mapper.Map<InstrumentWithPhotoAndSoundClipMediaInfo>(fetchedObject));
                }
                else
                {
                    if(imageHeader != null)
                    {
                        if (fetchedObject.PhotoMediaLength > 0) //Check if the photo exists by getting the photo file size
                        {
                            return Ok(fetchedObject.PhotoMedia);
                        }
                        else
                        {
                            return NotFound(); //404
                        }
                    }
                    else
                    {
                        if (fetchedObject.SoundClipMediaLength > 0) //Check if the sound exists by getting the sound file size
                        {
                            return Ok(fetchedObject.SoundClipMedia);
                        }
                        else
                        {
                            return NotFound(); //404
                        }
                    }
                }
            }
        }

        // Attention 6 - Add (POST) a new instrument to the datastore
        // POST: api/Instruments
        public IHttpActionResult Post([FromBody]InstrumentAdd newInstrument)
        {
            // Ensure that a "newItem" is in the entity body
            if (newInstrument == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Attempt to add the new object
            var addedItem = m.InstrumentAdd(newInstrument);

            // Continue?
            if (addedItem == null)
            {
                return BadRequest("Cannot add the object");
            }

            var uri = Url.Link("DefaultApi", new { id = addedItem.Id }); //201

            return Created(uri, addedItem);
        }

        // Attention 7 - COMMAND to set or configure an instrument photo
        // PUT: api/Instruments/5/SetPhoto
        [Route("api/Instruments/{id}/setphoto")]
        public IHttpActionResult PutSetPhoto(int id, [FromBody]byte[] newPhoto)
        {
            int byteLength = newPhoto.Length; //200000 bytes max (200kb)

            if (byteLength < 200000)
            {
                // GET the Content-Type header from the request
                var contentType = Request.Content.Headers.ContentType.MediaType;

                // Attemp to save
                if (m.SetInstrumentPhoto(id, contentType, newPhoto))
                {
                    return StatusCode(HttpStatusCode.NoContent); //Return code 204
                }
                else
                {
                    return BadRequest("Cannot set the photo");
                }
            }
            else
            {
                return BadRequest("Image is bigger than 200kb");
            }    
        }

        // Attention 8 - COMMAND to set or configure an instrument sound
        // PUT: api/Instruments/5/SetSound
        [Route("api/instruments/{id}/setsound")]
        public IHttpActionResult PutSetSound(int id, [FromBody]byte[] newSound)
        {
            int byteLength = newSound.Length; //200000 bytes max (200kb)

            if (byteLength < 200000)
            {
                // GET the Content-Type header from the request
                var contentType = Request.Content.Headers.ContentType.MediaType;

                // Attemp to save
                if (m.SetInstrumentSound(id, contentType, newSound))
                {
                    return StatusCode(HttpStatusCode.NoContent); //Return code 204
                }
                else
                {
                    return BadRequest("Cannot set the sound");
                }
            }
            else
            {
                return BadRequest("Sound is bigger than 200kb");
            }
        }

        // Attention 9 - Delete an instrument by id from the data store 
        // DELETE: api/Instruments/5
        public void Delete(int id)
        {
            m.DeleteInstrumentById(id);
        }
    }
}
