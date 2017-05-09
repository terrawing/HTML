using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assign10
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // AutoMapper create map statements
            // Projects
            Mapper.CreateMap<Models.Project, Controllers.ProjectBase>();
            Mapper.CreateMap<Models.Project, Controllers.ProjectWithMediaInfo>();
            Mapper.CreateMap<Controllers.ProjectAdd, Models.Project>();
            Mapper.CreateMap<Controllers.ProjectBase, Controllers.ProjectWithLink>();
            Mapper.CreateMap<Controllers.ProjectWithMediaInfo, Controllers.ProjectWithLink>();

            Mapper.CreateMap<Controllers.ProjectWithSharerInfo, Controllers.ProjectWithLink>();

            Mapper.CreateMap<Controllers.ProjectWithAllInfo, Controllers.ProjectWithLink>();
            Mapper.CreateMap<Models.Project, Controllers.ProjectWithAllInfo>();

            Mapper.CreateMap<Models.Project, Controllers.ProjectWithSharerInfo>();

            // Media
            Mapper.CreateMap<Models.Media, Controllers.MediaBase>();
            Mapper.CreateMap<Models.Media, Controllers.ProjectWithMediaInfo>();
            Mapper.CreateMap<Models.Media, Controllers.MediaWithMediaItem>();
            Mapper.CreateMap<Controllers.MediaAdd, Models.Media>();
            Mapper.CreateMap<Controllers.MediaBase, Controllers.MediaWithLink>();

            // Sharer
            Mapper.CreateMap<Models.Sharer, Controllers.SharerBase>();
            Mapper.CreateMap<Models.Sharer, Controllers.ProjectWithSharerInfo>();
            Mapper.CreateMap<Controllers.SharerAdd, Models.Sharer>();
            Mapper.CreateMap<Controllers.SharerBase, Controllers.SharerWithLink>();



#pragma warning restore CS0618
        }
    }
}