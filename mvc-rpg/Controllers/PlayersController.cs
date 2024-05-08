using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_rpg.Data;
using mvc_rpg.Models;

namespace mvc_rpg.Controllers
{
    public class PlayersController : Controller
    {
        private readonly RPGContext _context;
        private static readonly ImageConverter _imageConverter = new ImageConverter();
        public PlayersController(RPGContext context)
        {
            _context = context;
        }


        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailModel = new mvc_rpg.ViewModel.PlayerDetail();
            var player = await _context.Players
                .Include(x => x.Items)
                    .ThenInclude(x => x.ItemType)
                .FirstOrDefaultAsync(m => m.ID == id);

            detailModel.Player = player;

            var graves = _context.Graves
                .Include(m => m.Player)
                .Include(m => m.Enemy)
                .Where(m => m.PlayerID == id)
                .ToList();

            detailModel.Graves = graves;

            if (player == null)
            {
                return NotFound();
            }

            return View(detailModel);
        }

        // GET: Players/Create
        public IActionResult Index()
        {
            return View();
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Bitmap GetImageFromByteArray(byte[] byteArray)
        {
            Bitmap bm = (Bitmap)_imageConverter.ConvertFrom(byteArray);

            if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                               bm.VerticalResolution != (int)bm.VerticalResolution))
            {
                // Correct a strange glitch that has been observed in the test program when converting 
                //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
                //  slightly away from the nominal integer value
                bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                 (int)(bm.VerticalResolution + 0.5f));
            }

            return bm;
        }

        private static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            // Get the image current width
            int sourceWidth = imgToResize.Width;
            // Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            // Calculate width and height with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);
            // New Width and Height
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Bitmap)b;
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Player player)
        {
            if (player.Name == "Admin")
            {
                HttpContext.Session.SetInt32("user_ID", -1);
                return Redirect("/Home/Admin");
            }

            if (player.ProfilePictureRaw != null)
            {
                player.ProfilePicture = GetByteArrayFromImage(player.ProfilePictureRaw);
            }
            player.isAlive = true;
            Bitmap bmp;
            using (var ms = new MemoryStream(player.ProfilePicture))
            {
                bmp = new Bitmap(ms);
            }

            bmp = ResizeImage(bmp, new Size(512, 512));


            if (player.ProfilePictureRaw != null)
            {
                player.ProfilePicture = ImageToByte(bmp);
            }

            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("user_ID", player.ID);
                return Redirect($"/Players/Details/{player.ID}");
            }
            return Redirect($"/Players/Details/{player.ID}");
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Health,SkinType,HairType,EyeType,ProfilePicture")] Player player)
        {
            if (id != player.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.ID == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.ID == id);
        }
    }
}
