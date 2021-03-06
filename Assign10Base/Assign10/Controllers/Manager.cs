﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assign10.Models;
//using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity;

namespace Assign10.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

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

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()




        // ############################################################
        // Project

        public IEnumerable<ProjectWithSharerInfo> ProjectGetAll() //ProjectBase before
        {
            // Fetch the objects with a matching user name
            var c = ds.Projects.Include("Medias").Include("Sharers").Where(i => i.Owner == User.Name);

            return Mapper.Map<IEnumerable<ProjectWithSharerInfo>>(c); //ProjectBase before
        }

        public ProjectWithAllInfo ProjectGetById(int id)
        {
            // Fetch the object with a matching user name
            var o = ds.Projects
                .Include("Medias").Include("Sharers")
                .SingleOrDefault(i => i.Id == id && i.Owner == User.Name);

            // Do additional checks for sharer being able to access
            // if the User.Name is not the Owner, check if the User.Name is a Sharer instead
            if (o == null)
            {
                var sharedObj = ds.Sharers.SingleOrDefault(s => s.Project.Id == id && s.Username == User.Name);

                if (sharedObj == null)
                {
                    return null;
                }
                else
                {
                    var o2 = ds.Projects.Include("Medias").Include("Sharers").SingleOrDefault(p => p.Id == id);

                    if (o2 == null)
                    {
                        return null;
                    }
                    else
                    {
                        return Mapper.Map<ProjectWithAllInfo>(o2);
                    }
                }
            }
            else
            {
                return Mapper.Map<ProjectWithAllInfo>(o);
            }
            /*
            return (o == null)
                    ? null
                    : Mapper.Map<ProjectWithMediaInfo>(o);*/
        }

        public ProjectBase ProjectAdd(ProjectAdd newItem)
        {
            // Create a new object
            var addedItem = Mapper.Map<Project>(newItem);
            // Add the user name
            addedItem.Owner = User.Name;

            // Save
            ds.Projects.Add(addedItem);
            ds.SaveChanges();

            // Return the object
            return Mapper.Map<ProjectWithMediaInfo>(addedItem);
        }

        public ProjectWithSharerInfo ProjectShare(SharerAdd newItem)
        {
            // Get the project by id with it's associated collection for edit
            var fetchedObject = ds.Projects.Include("Sharers").SingleOrDefault(p => p.Id == newItem.ProjectId && p.Owner == User.Name);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                string newAccessLevel = newItem.AccessLevel.ToLower();
                string newUsername = newItem.Username.ToLower();

                foreach (var sharer in fetchedObject.Sharers)
                {
                    string sharedAccessLevel = sharer.AccessLevel.ToLower();
                    string sharedUsername = sharer.Username.ToLower();

                    // check if the specific project already has the same sharer username and sharer access level
                    if (string.Equals(sharedAccessLevel, newAccessLevel) && string.Equals(sharedUsername, newUsername))
                    {
                        // Access level is the same and username is the same
                        return null;
                    }
                }

                // When there is no duplicate

                // Add a sharer data to the datastore
                Sharer addSharer = Mapper.Map<Sharer>(newItem);
                addSharer.Project = ds.Projects.SingleOrDefault(p => p.Id == newItem.ProjectId);
                ds.Sharers.Add(addSharer);

                //  After that, add it to the collection of the Project
                //fetchedObject.Sharers.Add(addSharer);

                ds.SaveChanges();

                return Mapper.Map<ProjectWithSharerInfo>(fetchedObject);
            }
        }

        // ############################################################
        // Media

        public IEnumerable<MediaBase> MediaGetAll()
        {
            // Fetch the objects with a matching user name
            var c = ds.Medias
                .Include("Project")
                .Where(i => i.Owner == User.Name);

            return Mapper.Map<IEnumerable<MediaBase>>(c);
        }

        public MediaWithMediaItem MediaGetById(int id)
        {
            // Fetch the object with a matching user name
            var fetchedObject =
                ds.Medias
                .Include("Project")
                .SingleOrDefault(i => i.Id == id && i.Owner == User.Name);

            // Fetch the project object bases on matching the media identifier and matching owner name
            //var projectObject = ds.Projects.SingleOrDefault(i => i.Medias.Any(m => m.Id == id) && i.Owner == User.Name);

            
            if (fetchedObject == null) // User.Name is not the Owner check if User.Name is a Sharer instead
            {
                // Attempt to get the media object and it's associated project object by id without matching user name
                // because if the fetchedObject is null there is no fetchedObject.Project.id
                //var test = fetchedObject.Project.Id;

                var fetchedObject2 = ds.Medias.Include("Project").SingleOrDefault(i => i.Id == id);

                if (fetchedObject2 == null) // if id does not match to anything in the data store
                {
                    return null;
                }
                else
                {
                    var sharedObj = ds.Sharers.SingleOrDefault(s => s.Project.Id == fetchedObject2.Project.Id && s.Username == User.Name);

                    if(sharedObj == null)
                    {
                        return null;
                    }
                    else
                    {
                        var fetchedObject3 = ds.Medias.Include("Project").SingleOrDefault(i => i.Id == id); // Don't need to check for null, we did that check already earlier

                        return Mapper.Map<MediaWithMediaItem>(fetchedObject3);
                    }
                }
            }
            else
            {
                return Mapper.Map<MediaWithMediaItem>(fetchedObject);
            }

            /*
            return (fetchedObject == null)
                ? null
                : Mapper.Map<MediaWithMediaItem>(fetchedObject);*/
        }

        public MediaBase MediaAdd(MediaAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null) { return null; }

            // Must validate the associated object
            var associatedItem = ds.Projects
                .SingleOrDefault(i => i.Id == newItem.ProjectId && i.Owner == User.Name);

            if (associatedItem == null)
            {
                var sharedObj = ds.Sharers.SingleOrDefault(s => s.Project.Id == newItem.ProjectId && s.Username == User.Name);

                if (sharedObj == null)
                {
                    return null;
                }
                else
                {
                    var projectobj = ds.Projects.SingleOrDefault(i => i.Id == newItem.ProjectId);

                    if (projectobj == null)
                    {
                        return null;
                    }
                    else
                    {
                        // Create a new object
                        var addedItem = Mapper.Map<Media>(newItem);
                        // Configure the association
                        addedItem.Project = projectobj;
                        // Add the user name
                        addedItem.Owner = projectobj.Owner;
                        addedItem.Contributor = User.Name;

                        // Save
                        ds.Medias.Add(addedItem);
                        ds.SaveChanges();

                        // Return the object
                        return Mapper.Map<MediaBase>(addedItem);
                    }
                }
            }
            else
            {
                // Create a new object
                var addedItem = Mapper.Map<Media>(newItem);
                // Configure the association
                addedItem.Project = associatedItem;
                // Add the user name
                addedItem.Owner = User.Name;
                addedItem.Contributor = User.Name;

                // Save
                ds.Medias.Add(addedItem);
                ds.SaveChanges();

                // Return the object
                return Mapper.Map<MediaBase>(addedItem);
            }
        }

        public bool MediaItemAdd(int id, string contentType, byte[] media)
        {
            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | media == null) { return false; }

            // Attempt to find the matching object
            // Must fetch the associated object to enable "save" to work properly
            var storedItem = ds.Medias.Include("Project").SingleOrDefault(m => m.Id == id);

            // Ensure that we can continue
            // The following is probably the easiest way to do this
            // Add another condition to match the user name

            // check if the meta data is added first before adding the media item
            if (storedItem == null)
            {
                return false;
            }

            if (storedItem.Owner != User.Name)
            {
                // Check if the sharer is part of the project's contribution
                var sharedObj = ds.Sharers.SingleOrDefault(s => s.Project.Id == storedItem.Project.Id && s.Username == User.Name);

                if (sharedObj == null)
                {
                    return false;
                }
                else
                {
                    var projectobj = ds.Projects.SingleOrDefault(i => i.Id == sharedObj.Project.Id);

                    if (projectobj == null)
                    {
                        return false;
                    }
                    else
                    {
                        // Save the media item
                        storedItem.Owner = projectobj.Owner;
                        storedItem.Contributor = User.Name;
                        storedItem.ItemContentType = contentType;
                        storedItem.ItemMedia = media;

                        // Attempt to save changes
                        return (ds.SaveChanges() > 0) ? true : false;
                    }
                }
            }
            else
            {
                // Save the media item
                storedItem.Owner = User.Name;
                storedItem.Contributor = User.Name;
                storedItem.ItemContentType = contentType;
                storedItem.ItemMedia = media;

                // Attempt to save changes
                return (ds.SaveChanges() > 0) ? true : false;
            }
        }

        /*
        public IEnumerable<NoteBase> NoteGetAll()
        {
            LoadData();

            // Attention - 3 - Get all, for the authenticated user only
            var c = ds.Notes.Where(n => n.Owner == User.Name);

            return Mapper.Map<IEnumerable<NoteBase>>(c.OrderByDescending(n => n.DateCreated));
        }

        public NoteBase NoteGetById(int id)
        {
            // Attention - 4 - Get one, for the authenticated user only
            var o = ds.Notes.SingleOrDefault
                (n => n.Id == id && n.Owner == User.Name);

            return (o == null) ? null : Mapper.Map<NoteBase>(o);
        }

        public IEnumerable<NoteBase> NoteGetAllByTitle(string text)
        {
            // Search for partial match in title property, case-insensitive
            // Future
            throw new NotImplementedException();
        }

        public IEnumerable<NoteBase> NoteGetAllByContent(string text)
        {
            // Search for partial match in content property, case-insensitive
            // Future
            throw new NotImplementedException();
        }

        public NoteBase NoteAdd(NoteAdd newItem)
        {
            // Attention - 5 - Add new is NOT restricted
            var addedItem = ds.Notes.Add(Mapper.Map<Note>(newItem));
            // Assign the owner
            addedItem.Owner = User.Name;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<NoteBase>(addedItem);
        }

        public NoteBase NoteEdit(NoteEdit newItem)
        {
            // Attention - 6 - Edit existing, for the authenticated user only

            // Attempt to fetch the object
            // Can either do a two-condition fetch, or test it in the "if" statement
            // In this method, we'll do it in the following statement
            var o = ds.Notes.SingleOrDefault
                (n => n.Id == newItem.Id && n.Owner == User.Name);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<NoteBase>(o);
            }
        }

        public bool NoteDelete(int id)
        {
            // Attention - 7 - Delete item, for the authenticated user only

            // Attempt to fetch the object to be deleted
            // Can either do a two-condition fetch, or test it in the "if" statement
            // In this method, we'll do it in the "if" statement
            var itemToDelete = ds.Notes.Find(id);

            if (itemToDelete == null || itemToDelete.Owner != User.Name)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Notes.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }
        */

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // Return if there's existing data

            if (ds.Projects.Count() > 0) { return false; }

            // what about medias too?






            // Otherwise...
            // Create and add objects
            // Save changes

            /*
            var note1 = new Note
            {
                Owner = User.Name,
                Title = "Microsoft Word tips part 1",
                Content = "<p>Video provides a powerful way to help you prove your point. When you click Online Video, you can paste in the embed code for the video you want to add. You can also type a keyword to search online for the video that best fits your document.<br>To make your document look professionally produced, Word provides header, footer, cover page, and text box designs that complement each other. For example, you can add a matching cover page, header, and sidebar. Click Insert and then choose the elements you want from the different galleries.</p>"
            };
            var note2 = new Note
            {
                Owner = User.Name,
                Title = "Microsoft Word tips part 2",
                Content = "<p>Themes and styles also help keep your document coordinated. When you click Design and choose a new Theme, the pictures, charts, and SmartArt graphics change to match your new theme. When you apply styles, your headings change to match the new theme.<br>Save time in Word with new buttons that show up where you need them. To change the way a picture fits in your document, click it and a button for layout options appears next to it. When you work on a table, click where you want to add a row or a column, and then click the plus sign.<br>Reading is easier, too, in the new Reading view. You can collapse parts of the document and focus on the text you want. If you need to stop reading before you reach the end, Word remembers where you left off - even on another device.</p>"
            };
            ds.Notes.Add(note1);
            ds.Notes.Add(note2);
            */
            ds.SaveChanges();

            return true;
        }

        public bool RemoveData()
        {
            try
            {
                //foreach (var e in ds.Your_Entity_Set)
                //{
                //    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                //}
                //ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}