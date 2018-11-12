using ClientWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class NotesController : Controller
    {
        HttpClient client = new HttpClient();

        public NotesController()
        {
            client.BaseAddress = new Uri($"http://www.deveup.com.br/notas/api/notes");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Notes
        public ActionResult Index()
        {
            List<Note> notes = new List<Note>();
            HttpResponseMessage response = client.GetAsync($"/notas/api/notes").Result;
            if (response.IsSuccessStatusCode)
            {
                notes = response.Content.ReadAsAsync<List<Note>>().Result;
            }
            return View(notes);
        }


        // GET: Notes/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/notas/api/notes/{id}").Result;
            Note note = response.Content.ReadAsAsync<Note>().Result;
            if (note != null)
                return View(note);
            else
                return HttpNotFound();

        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        public ActionResult Create(Note note)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync<Note>($"/notas/api/notes", note).Result;
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error while creating note.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/notas/api/notes/{id}").Result;
            Note note = response.Content.ReadAsAsync<Note>().Result;
            if (note != null)
                return View(note);
            else
                return HttpNotFound();
        }

        // POST: Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Note note)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Note>($"/notas/api/notes/{id}", note).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error while editing note.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/notas/api/notes/{id}").Result;
            Note note = response.Content.ReadAsAsync<Note>().Result;
            if (note != null)
                return View(note);
            else
                return HttpNotFound();
        }

        // POST: Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Note note)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"/notas/api/notes/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error while deleting note.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
