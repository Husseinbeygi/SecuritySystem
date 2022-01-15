using _0_Framework.Application;
using Microsoft.AspNetCore.Components;
using UIService.Model;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Gallery
    {
        [Parameter] public string hostaddress { get; set; } = string.Empty;

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


        private void HostNullGuard()
        {
            if (string.IsNullOrEmpty(hostaddress))
                navmanager.NavigateTo("/admin/camera");
        }

        private void GetImageAndVideosAddress()
        {
            var imagePath = Path.Combine(Environment.WebRootPath, $"{hostaddress}/pictures");
            var videoPath = Path.Combine(Environment.WebRootPath, $"{hostaddress}/videos");
            CreateDirectoriesWhenIsNotExist(imagePath, videoPath);
            string[] imgPaths = Directory.GetFiles(imagePath);
            string[] videoPaths = Directory.GetFiles(videoPath);
            GetListOfPictureFiles(imgPaths);
            GetListOfVideoFiles(videoPaths);
            _countPageImg = CountPage(AllfilesModelImage);
            _countPageVideo = CountPage(AllfilesModelVideo);
        }

        private int CountPage(List<FileModel> filesModel)
        {
            return (filesModel.Count() % 8) > 0
                ? (filesModel.Count() / 8) + 1
                : (filesModel.Count() / 8);
        }


        private void CreateDirectoriesWhenIsNotExist(string imgPaths, string videoPaths)
        {
            if (!Directory.Exists(imgPaths))
            {
                Directory.CreateDirectory(imgPaths);
            }

            if (!Directory.Exists(videoPaths))
            {
                Directory.CreateDirectory(videoPaths);
            }
        }

        private void GetListOfPictureFiles(string[] imgPaths)
        {
            AllfilesModelImage.AddRange(from string filePath in imgPaths
                select new FileModel
                {
                    FileName = $"{hostaddress}/pictures/" + Path.GetFileName(filePath),
                    FasriFileCreationTime = File.GetCreationTime(filePath).ToFarsiFull(),
                    FileCreationTime = File.GetCreationTime(filePath),
                });
            AllfilesModelImage = AllfilesModelImage.OrderByDescending(x => x.FileCreationTime).ToList();
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