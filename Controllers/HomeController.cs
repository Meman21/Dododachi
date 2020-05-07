using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        // Set pet into session
        private void SetSessionPet(Pet myPet = null)
        {
            if (myPet == null)
                myPet = new Pet();
            HttpContext.Session.SetInt32("fullness", myPet.Fullness);
            HttpContext.Session.SetInt32("happiness", myPet.Happiness);
            HttpContext.Session.SetInt32("meals", myPet.Meals);
            HttpContext.Session.SetInt32("energy", myPet.Energy);
        }

        // Get pet from session with updated stats
        private Pet GetSessionPet()
        {
            return new Pet
            (
                HttpContext.Session.GetInt32("fullness"),
                HttpContext.Session.GetInt32("happiness"),
                HttpContext.Session.GetInt32("meals"),
                HttpContext.Session.GetInt32("energy")
            );
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            // Checks if there's a pet in session
            if (GetSessionPet() == null)
                SetSessionPet();
            // Checks if dojodachi meets win conditions
            if (GetSessionPet().Won())
            {
                TempData["result"] = "Congratulations! You won!";
                TempData["image"] = "http://25.media.tumblr.com/tumblr_m3gtifCVU11r3mf3wo6_500.gif";
                ViewBag.Result = (string)TempData["result"];
                ViewBag.PetImage = (string)TempData["image"];
                return View("Won", GetSessionPet());
            }
            // Checks if dojodachi meets lose conditions
            if (GetSessionPet().Lost())
            {
                TempData["result"] = "Your Dojodachi has passed away :(";
                TempData["image"] = "https://i.ytimg.com/vi/aSQKlh2-S2A/maxresdefault.jpg";
                ViewBag.Result = (string)TempData["result"];
                ViewBag.PetImage = (string)TempData["image"];
                return View("Lost", GetSessionPet());
            }
            // Set TempData to defaults if nothing has been stored yet
            if (TempData["result"] == null)
                TempData["result"] = "Let's Get Started!";
            if (TempData["image"] == null)
                TempData["image"] = "http://static.pokemonpets.com/images/monsters-images-300-300/149-Dragonite.png";
            ViewBag.Result = (string)TempData["result"];
            ViewBag.PetImage = (string)TempData["image"];
            return View(GetSessionPet());
        }
        [HttpGet("feed")]
        public IActionResult Feed()
        {
            // Getting pet from session
            Pet myPet = GetSessionPet();
            // Using feed method/storing it in a variable
            TempData["result"] = myPet.Feed();
            // Updating session with the modified stats
            SetSessionPet(myPet);
            //Display different images based on dojodachi's response
            if ((string)TempData["result"] == "Your Dojodachi didn't like that. Fullness +0, Meals -1")
                TempData["image"] = "https://pa1.narvii.com/6190/54d7875d591c21a65800dcd2dbfc95633c2cddf7_hq.gif";
            else
                TempData["image"] = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/intermediary/f/9ab50781-9fe9-4c12-9515-e37ea09a1865/d4urcwx-956b29c5-cd9c-42f0-8a3d-1a2c16776496.png/v1/fill/w_350,h_350,strp/happy_dragonite_face_by_sjru_d4urcwx-350t.png";
            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            Pet myPet = GetSessionPet();
            TempData["result"] = myPet.Play();
            SetSessionPet(myPet);
            if ((string)TempData["result"] == "Your Dojodachi didn't feel like playing. Happiness +0, Energy -5")
                TempData["image"] = "https://i.imgur.com/YX0bCPZ.gif?1";
            else
                TempData["image"] = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/intermediary/f/9ab50781-9fe9-4c12-9515-e37ea09a1865/d4urcwx-956b29c5-cd9c-42f0-8a3d-1a2c16776496.png/v1/fill/w_350,h_350,strp/happy_dragonite_face_by_sjru_d4urcwx-350t.png";
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            Pet myPet = GetSessionPet();
            TempData["result"] = myPet.Work();
            SetSessionPet(myPet);
            TempData["image"] = "https://orig00.deviantart.net/ebd2/f/2017/129/2/6/untitled_by_elgusar_wolf-db8p6i8.jpg";
            return RedirectToAction("Index");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            Pet myPet = GetSessionPet();
            TempData["result"] = myPet.Sleep();
            SetSessionPet(myPet);
            TempData["image"] = "https://i.pinimg.com/originals/f7/a9/8d/f7a98d9cea0e38247d0bf53798ab867a.jpg";
            return RedirectToAction("Index");
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

