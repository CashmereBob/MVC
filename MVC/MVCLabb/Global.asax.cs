using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCLabb.Controllers;
using System.IO;
using MVCLabb.Models;
using MVCLabb.App_Start;
using System.Web.Helpers;
using System.Security.Claims;
using MVCLabb.Data.Repository;

namespace MVCLabb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            SeedDB();

        }

        protected void Application_Error()
        {
            Response.Redirect("~/Home/Error");
        }

        protected void SeedDB()
        {
            IUserRepository userRepo = new UserRepository();


            if (userRepo.GetAllUsers().Count() < 1) { 

            Guid userID = Guid.NewGuid();

            userRepo.AddUser(new Data.User {
                Admin = false,
                Country = "nn",
                Email = "user@user.com",
                Password = "user",
                Id = userID,
                Name = "TestUser",
            });

            IAlbumRepository albumRepo = new AlbumRepository();

            Guid album1 = Guid.NewGuid();

            albumRepo.AddAlbum(new Data.Album
            {
                Description = "+1 live-edge next level XOXO, ennui neutra vinyl hella. Scenester tofu selvage helvetica, fingerstache swag literally woke pinterest chicharrones jean shorts ennui kickstarter cronut. Salvia drinking vinegar hammock, helvetica lumbersexual retro franzen gochujang pabst. Deep v vice post-ironic umami, skateboard synth 8-bit mlkshk salvia street art neutra single-origin coffee food truck taxidermy. Coloring book listicle portland shabby chic prism, letterpress mumblecore.",
                Name = "Squid occupy celiac",
                Id = album1,
                UserID = userID
            });

            Guid album2 = Guid.NewGuid();

            albumRepo.AddAlbum(new Data.Album
            {
                Description = "Fingerstache swag literally woke pinterest chicharrones jean shorts ennui kickstarter cronut. Salvia drinking vinegar hammock, helvetica lumbersexual retro franzen gochujang pabst. Deep v vice post-ironic umami, skateboard synth 8-bit mlkshk salvia street art neutra single-origin coffee food truck taxidermy. Coloring book listicle portland shabby chic prism, letterpress mumblecore.",
                Name = "single-origin coffee",
                Id = album2,
                UserID = userID
            });

            Guid album3 = Guid.NewGuid();

            albumRepo.AddAlbum(new Data.Album
            {
                Description = "Kickstarter cronut. Salvia drinking vinegar hammock, helvetica lumbersexual retro franzen gochujang pabst. Deep v vice post-ironic umami, skateboard synth 8-bit mlkshk salvia street art neutra single-origin coffee food truck taxidermy. Coloring book listicle portland shabby chic prism, letterpress mumblecore.",
                Name = "Franzen jean shorts",
                Id = album3,
                UserID = userID
            });

            Guid album4 = Guid.NewGuid();

            albumRepo.AddAlbum(new Data.Album
            {
                Description = "Helvetica lumbersexual retro franzen gochujang pabst. Deep v vice post-ironic umami, skateboard synth 8-bit mlkshk salvia street art neutra single-origin coffee food truck taxidermy. Coloring book listicle portland shabby chic prism, letterpress mumblecore.",
                Name = "Bag biodisel",
                Id = album4,
                UserID = userID
            });

            IPhotoRepository photoRepo = new PhotoRepository();
                var albums = albumRepo.GettAllAlbums();

                var photo = new Data.Photo {
                    Path = " /Photos/marvel-ironman.png",
                    Name = "Freegan asymmetrical",
                    Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                    Date = DateTime.Now,
                    UserID = userID,
                    AlbumID = albums[0].Id
            };
                photoRepo.AddPhotoToDB(photo);

                photoRepo.AddPhotoToDB(new Data.Photo
                {
                    Path = " /Photos/img-54705803c114c-posts-9775.jpg",
                    Name = "Freegan asymmetrical",
                    Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                    Date = DateTime.Now,
                    UserID = userID,
                    AlbumID = albums[0].Id
                });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/URBAN.png",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[0].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/MTI4NjQ2ODIyMDIwMjg5ODEw.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[0].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/blogpost_Banner-img.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[1].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/monkton dkb grain 1.png",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[1].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/img-1.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[1].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/&NCS_modified=20160831204930&MaxW=960&imageVersion=default&AR-160839797.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[1].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/cnab.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[2].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/lobuche-approach1.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[2].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/multimedia-1436-img.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[2].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/Knut_IMG_8095.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[3].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/browmarker.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[3].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/4237641-images.jpg",
                Name = "Freegan asymmetrical",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[3].Id
            });

            photoRepo.AddPhotoToDB(new Data.Photo
            {
                Path = " /Photos/Bonsai_IMG_6404.jpg",
                Name = "Gentrify butcher selfies tacos",
                Description = "Freegan asymmetrical squid microdosing. Gentrify butcher selfies tacos, poke occupy mustache roof party brunch dreamcatcher glossier chicharrones green juice +1 semiotics. Kickstarter kinfolk sartorial narwhal, semiotics humblebrag brooklyn tumblr kale chips mumblecore.",
                Date = DateTime.Now,
                UserID = userID,
                AlbumID = albums[3].Id
            });

            }

        }


    }
}
