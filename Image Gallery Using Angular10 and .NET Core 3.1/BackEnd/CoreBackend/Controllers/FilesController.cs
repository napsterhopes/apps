using CoreBackend.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Controllers
{
    public class FilesController : Controller
    {
        #region Get All Images
        [HttpGet]
        [Route("api/files")]
        public List<WebImage> Get([FromQuery(Name = "query")] string query, [FromQuery(Name = "filterVal")] string filterVal)
        {
            try
            {
                SiemensTaskContext _serverDbContext = new SiemensTaskContext();
                if (string.Equals(query, "undefined") || string.IsNullOrEmpty(query))
                {
                    List<WebImage> webImages = _serverDbContext.WebImages.ToList();
                    if (string.Equals(filterVal, "Desc"))
                    {
                        return webImages.Select(c => { c.WebImgString = "data:image/jpg;base64, " + Convert.ToBase64String(c.Picture); return c; }).OrderByDescending(d => d.DateTimeAdded).ToList();
                    }
                    else
                    {
                        return webImages.Select(c => { c.WebImgString = "data:image/jpg;base64, " + Convert.ToBase64String(c.Picture); return c; }).OrderBy(d => d.DateTimeAdded).ToList();
                    }
                }
                else
                {
                    List<WebImage> webImages = _serverDbContext.WebImages.ToList();
                    if (string.Equals(filterVal, "Desc"))
                    {
                        return webImages.Select(c => { c.WebImgString = "data:image/jpg;base64, " + Convert.ToBase64String(c.Picture); return c; }).Where(x => x.WebImgName == query).OrderByDescending(d => d.DateTimeAdded).ToList();
                    }
                    else
                    {
                        return webImages.Select(c => { c.WebImgString = "data:image/jpg;base64, " + Convert.ToBase64String(c.Picture); return c; }).Where(x => x.WebImgName == query).OrderBy(d => d.DateTimeAdded).ToList();
                    }
                }
            }
            catch (Exception generatedEx)
            {
                _logger.Fatal(generatedEx);
                throw;
            }
        } 
        #endregion

        #region Upload File
        [HttpPost, DisableRequestSizeLimit]
        [Route("api/files")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                SiemensTaskContext _serverDbContext = new SiemensTaskContext();
                WebImage objWebImage = new WebImage();
                string message = string.Empty;
                if (file.Length > 0)
                {
                    if ((file.FileName.Count(s => s == '.')) > 1)
                    {
                        return BadRequest(message = "Sorry! File has multiple extensions");
                    }
                    else
                    {
                        #region Save In Repo
                        byte[] image = null;
                        using (var fs1 = file.OpenReadStream())
                        {
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                image = ms1.ToArray();
                            }
                        }
                        objWebImage.Picture = image;
                        objWebImage.WebImgName = Path.GetFileNameWithoutExtension(file.FileName);
                        objWebImage.DateTimeAdded = DateTime.UtcNow;
                        if (objWebImage.WebImgId == 0)
                        {
                            _serverDbContext.WebImages.Add(objWebImage);
                            _serverDbContext.SaveChanges();
                        }
                        #endregion
                        return StatusCode(200, message = "Uploaded Successfully");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception generatedEx)
            {
                _logger.Fatal(generatedEx);
                return StatusCode(500, $"Internal server error: {generatedEx}");
            }
        }
        #endregion
        private static readonly ILog _logger = Logger.GetLogger(typeof(Logger));

    }
}
