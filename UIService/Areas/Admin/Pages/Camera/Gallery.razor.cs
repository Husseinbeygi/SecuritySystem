using _0_Framework.Application;
using Microsoft.AspNetCore.Components;
using UIService.Model;
using System.Linq;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Gallery
    {

        [Parameter]
        public string hostaddress { get; set; } = string.Empty;

        public List<FileModel> AllfilesModelImage { get; set; } = new();
        public List<FileModel> AllfilesModelVideo { get; set; } = new();

        public List<FileModel> SelectedfilesModelImage { get; set; } = new();
        public List<FileModel> SelectedfilesModelVideo { get; set; } = new();


        private int _countPageImg;
        private int _countPageVideo;
        public Gallery()
        {
        }

        protected override void OnInitialized()
        {
            HostNullGuard();
            GetImageAndVideosAddress();
            GetPagedImageAddress(1);
            GetPagedVideoAddress(1);
            base.OnInitialized();
        }


        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }


        private void HostNullGuard()
        {
            if (string.IsNullOrEmpty(hostaddress))
                navmanager.NavigateTo("/admin/camera");
        }
        private void GetImageAndVideosAddress()
        {
            string[] ImgPaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, $"{hostaddress}/pictures"));
            string[] VideoPaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, $"{hostaddress}/videos"));
            GetListOfPictureFiles(ImgPaths);
            GetListOfVideoFiles(VideoPaths);
            _countPageImg = (AllfilesModelImage.Count() % 8) > 0 ? (AllfilesModelImage.Count() / 8) + 1 : (AllfilesModelImage.Count() / 8);
            _countPageVideo = (AllfilesModelVideo.Count() % 8) > 0 ? (AllfilesModelVideo.Count() / 8) + 1 : (AllfilesModelVideo.Count() / 8);

        }

        private void GetListOfPictureFiles(string[] ImgPaths)
        {
            AllfilesModelImage.AddRange(from string filePath in ImgPaths
                                        select new FileModel
                                        {
                                            FileName = $"{hostaddress}/pictures/" + Path.GetFileName(filePath),
                                            FasriFileCreationTime = File.GetCreationTime(filePath).ToFarsiFull(),
                                            FileCreationTime = File.GetCreationTime(filePath),


                                        });
            AllfilesModelImage =  AllfilesModelImage.OrderByDescending(x => x.FileCreationTime).ToList();
            
        }

        private void GetListOfVideoFiles(string[] VideoPaths)
        {
            AllfilesModelVideo.AddRange(from string filePath in VideoPaths
                                        select new FileModel
                                        {
                                            FileName = $"{hostaddress}/videos/" + Path.GetFileName(filePath),
                                            FasriFileCreationTime = File.GetCreationTime(filePath).ToFarsiFull(),
                                            FileCreationTime = File.GetCreationTime(filePath),


                                        });
            AllfilesModelVideo = AllfilesModelVideo.OrderByDescending(x => x.FileCreationTime).ToList();

        }

        private void GetPagedImageAddress(int page)
        {
            SelectedfilesModelImage = AllfilesModelImage.Skip((page - 1) * 8).Take(8).ToList();
        }

        private void GetPagedVideoAddress(int page)
        {
            SelectedfilesModelVideo = AllfilesModelVideo.Skip((page - 1) * 8).Take(8).ToList();
        }

    }
}
