using _0_Framework.Application;
using Microsoft.AspNetCore.Components;
using UIService.Model;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Gallery
    {

        public Gallery()
        {
        }

        [Parameter]
        public string hostaddress { get; set; } = string.Empty;

        public List<FileModel> filesModelImage { get; set; } = new();
        public List<FileModel> filesModelVideo { get; set; } = new();


        protected override void OnInitialized()
        {
            if (string.IsNullOrEmpty(hostaddress))
                navmanager.NavigateTo("/admin/camera");

            string[] ImgPaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, $"{hostaddress}/pictures"));
            string[] VideoPaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, $"{hostaddress}/videos"));

            foreach (string filePath in ImgPaths)
            {
                filesModelImage.Add(new FileModel
                {
                    FileName = $"{hostaddress}/pictures/" + Path.GetFileName(filePath),
                    FileCreationTime = File.GetCreationTime(filePath).ToFarsi(),

                });
            }
            foreach (string filePath in VideoPaths)
            {
                filesModelVideo.Add(new FileModel { FileName = $"{hostaddress}/videos/" + Path.GetFileName(filePath) });
            }
            base.OnInitialized();
        }
    }
}
